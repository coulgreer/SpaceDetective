using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConversationController : MonoBehaviour {

    public delegate void ConversingEventHandler();
    public static event ConversingEventHandler Conversing;

    public delegate void ConversedEventHandler();
    public static event ConversedEventHandler Conversed;

    public static ConversationController Instance;

    public bool IsFinishedConversing { get; private set; }

    [SerializeField]
    private GameObject DialogueOverlay;
    [SerializeField]
    private TextMeshProUGUI Title;
    [SerializeField]
    private TextMeshProUGUI Prompt;
    [SerializeField]
    private Image Portrait;
    [SerializeField]
    private Transform ButtonPanel;
    [SerializeField]
    private GameObject PrefabButton;

    private Conversation Conversation;
    private GameObject Player;

    void OnEnable() {
        Conversed += delegate { SetDialogueVisibility(false); };
    }

    void OnDisable() {
        Conversed -= delegate { SetDialogueVisibility(false); };
    }

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(this);
        }
    }

    void Start() {
        SetDialogueVisibility(false);
    }

    public void SetDialogueVisibility(bool isDialogueVisible) {
        if (isDialogueVisible) {
            Conversing?.Invoke();
        }

        DialogueOverlay.SetActive(isDialogueVisible);
    }

    public void SetConversation(Conversation conversation) {
        if (conversation != null) {
            this.Conversation = conversation;
            UpdatePrompt(conversation.ActiveNode.Id);
        }
    }

    private void UpdatePrompt(ConversationNodeId id) {
        if (ConversationNodeId.ExitId.Equals(id)) {
            Conversation.SetActiveNode(Conversation.GreetingNode.Id);
            Conversed?.Invoke();
        } else {
            Conversation.SetActiveNode(id);
        }

        UpdateConversation();
        UpdateOptions();
    }

    private void UpdateConversation() {
        string titleText = Conversation.GetTitle();
        string portraitPath = Conversation.GetPortraitPath();
        string promptText = Conversation.GetPrompt();

        Title.text = titleText;
        Prompt.text = promptText;
        if (portraitPath != null) {
            Portrait.sprite = Resources.Load<Sprite>(PrunePath(portraitPath));
        }
    }

    private string PrunePath(string path) {
        string pattern = "(?<=Resources\\/).*(?=\\.(png|jpg|jpeg))";
        return Regex.Match(path, pattern).Value;
    }

    private void UpdateOptions() {
        foreach (Transform child in ButtonPanel) {
            Destroy(child.gameObject);
        }

        IConversationNode node = Conversation.ActiveNode;
        foreach (IConversationOption option in node.Options) {
            if (!option.IsHidden) {
                CreateButton(option).transform.SetParent(ButtonPanel, false);
            }
        }
    }

    private GameObject CreateButton(IConversationOption option) {
        GameObject buttonObject = Instantiate(PrefabButton);

        Button buttonComponent = buttonObject.GetComponent<Button>();
        buttonComponent.GetComponentInChildren<TMP_Text>().text = option.Response;
        buttonComponent.onClick.AddListener(delegate {
            GrantKey(option);

            if (!Conversation.IsNodeHidden(option.HiddenDestinationId)) {
                SelectOption(option.HiddenDestinationId);
            } else {
                SelectOption(option.DefaultDestinationId);
            }
        });

        return buttonObject;
    }

    private void SelectOption(ConversationNodeId id) {
        Conversation.SetActiveNode(id);
        UpdatePrompt(id);
    }

    private void GrantKey(IKey key) {
        Inventory inventory = Player.GetComponent<Inventory>();

        inventory.AddKey(key);
    }

    public void SetPlayer(GameObject player) {
        if (player != null) {
            this.Player = player;
        }
    }
}
