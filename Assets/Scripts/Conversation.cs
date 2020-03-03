using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class Conversation : MonoBehaviour {

    public IConversationNode ActiveNode { get; private set; }
    public IConversationNode GreetingNode { get; private set; }

    [SerializeField]
    private string ManifestPath;

    private IDictionary<ConversationNodeId, IConversationNode> DialogueNodesByID;

    // Start is called before the first frame update
    void Start() {
        LoadConversationScript();
    }

    private void LoadConversationScript() {
        using (StreamReader r = new StreamReader(ManifestPath)) {
            string json = r.ReadToEnd();
            Dictionary<string, IList<IConversationNode>> dialogueNodesByName =
                JsonConvert.DeserializeObject<Dictionary<string, IList<IConversationNode>>>(json);

            SetIntroductionNodeStart(dialogueNodesByName);
            SetGrettingNodeStart(dialogueNodesByName);
            DialogueNodesByID = ParseToDictionaryById(dialogueNodesByName);
        }
    }

    private void SetIntroductionNodeStart(Dictionary<string, IList<IConversationNode>> dictionary) {
        IList<IConversationNode> introductionNodes = dictionary["introduction-nodes"];

        ActiveNode = introductionNodes[0];
    }

    private void SetGrettingNodeStart(Dictionary<string, IList<IConversationNode>> dictionary) {
        IList<IConversationNode> greetingNodes = dictionary["greeting-nodes"];

        GreetingNode = greetingNodes[0];
    }

    private IDictionary<ConversationNodeId, IConversationNode> ParseToDictionaryById(
        Dictionary<string, IList<IConversationNode>> dictionary) {

        IDictionary<ConversationNodeId, IConversationNode> tempDictionary =
            new Dictionary<ConversationNodeId, IConversationNode>();

        foreach (IConversationNode n in dictionary["introduction-nodes"]) {
            tempDictionary.Add(n.Id, n);
        }

        foreach (IConversationNode n in dictionary["greeting-nodes"]) {
            tempDictionary.Add(n.Id, n);
        }

        foreach (IConversationNode n in dictionary["body-nodes"]) {
            tempDictionary.Add(n.Id, n);
        }

        foreach (IConversationNode n in dictionary["conclusion-nodes"]) {
            tempDictionary.Add(n.Id, n);
        }

        return tempDictionary;
    }

    public void SetActiveNode(ConversationNodeId id) {
        if (!ConversationNodeId.ExitId.Equals(id) && !ConversationNodeId.EmptyId.Equals(id)) {
            ActiveNode = DialogueNodesByID[id];
        }
    }

    public string GetTitle() {
        return ActiveNode.Speaker.Name;
    }

    public string GetPortraitPath() {
        return ActiveNode.Speaker.PortraitPath;
    }

    public string GetPrompt() {
        return ActiveNode.Prompt;
    }

    public bool IsNodeHidden(ConversationNodeId id) {
        if (!DialogueNodesByID.ContainsKey(id)) { return true; }

        IConversationNode targetNode = DialogueNodesByID[id];

        return targetNode.IsHidden;
    }
}
