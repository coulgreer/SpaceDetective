using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class Conversation : MonoBehaviour {
    public ConversationNode ActiveNode { get; private set; }
    public ConversationNode GreetingNode { get; private set; }

    [SerializeField]
    private string ManifestPath;

    private IDictionary<ConversationNodeId, ConversationNode> DialogueNodesByID;

    // Start is called before the first frame update
    void Start() {
        LoadDialogueScript();
    }

    private void LoadDialogueScript() {
        using (StreamReader r = new StreamReader(ManifestPath)) {
            string json = r.ReadToEnd();
            Dictionary<string, IList<ConversationNode>> dialogueNodesByName = JsonConvert.DeserializeObject<Dictionary<string, IList<ConversationNode>>>(json);

            SetIntroductionNodeStart(dialogueNodesByName);
            SetGrettingNodeStart(dialogueNodesByName);
            DialogueNodesByID = ParseToDictionaryById(dialogueNodesByName);
        }
    }

    private void SetIntroductionNodeStart(Dictionary<string, IList<ConversationNode>> dictionary) {
        IList<ConversationNode> introductionNodes = dictionary["introduction-nodes"];

        ActiveNode = introductionNodes[0];
    }

    private void SetGrettingNodeStart(Dictionary<string, IList<ConversationNode>> dictionary) {
        IList<ConversationNode> greetingNodes = dictionary["greeting-nodes"];

        GreetingNode = greetingNodes[0];
    }

    private IDictionary<ConversationNodeId, ConversationNode> ParseToDictionaryById(
        Dictionary<string, IList<ConversationNode>> dictionary) {

        IDictionary<ConversationNodeId, ConversationNode> tempDictionary =
            new Dictionary<ConversationNodeId, ConversationNode>();

        foreach (ConversationNode n in dictionary["introduction-nodes"]) {
            tempDictionary.Add(n.Id, n);
        }

        foreach (ConversationNode n in dictionary["greeting-nodes"]) {
            tempDictionary.Add(n.Id, n);
        }

        foreach (ConversationNode n in dictionary["body-nodes"]) {
            tempDictionary.Add(n.Id, n);
        }

        foreach (ConversationNode n in dictionary["conclusion-nodes"]) {
            tempDictionary.Add(n.Id, n);
        }

        return tempDictionary;
    }

    public void SetActiveNode(ConversationNodeId id) {
        if (id != null) {
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
}
