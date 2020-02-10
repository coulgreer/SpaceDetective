using System.Collections.Generic;
using Newtonsoft.Json;

[JsonConverter(typeof(ConversationNodeConverter))]
public class ConversationNode {
    public ConversationNodeId Id { get; }
    public Speaker Speaker { get; }
    public string Prompt { get; }
    public IList<ConversationOption> Options { get; }

    private ConversationNode(ConversationNodeId id, Speaker speaker, string prompt) {
        Id = id;
        Speaker = speaker;
        Prompt = prompt;
    }

    public ConversationNode(
        ConversationNodeId id,
        Speaker speaker,
        string prompt,
        IList<ConversationOption> options) : this(id, speaker, prompt) {

        Options = options;
    }

    public ConversationNode(
        ConversationNodeId id,
        Speaker speaker,
        string prompt,
        ConversationNodeId destinationId) : this(id, speaker, prompt) {

        ConversationOption option = new ConversationOption("[continue...]", destinationId);

        List<ConversationOption> options = new List<ConversationOption>();
        options.Add(option);

        Options = options;
    }

    public override bool Equals(System.Object obj) {
        if (obj == null) { return false; }

        ConversationNode node = obj as ConversationNode;
        if (node == null) {
            return false;
        } else {
            return this.Id.Equals(node.Id);
        }
    }

    public override int GetHashCode() {
        return Id.GetHashCode();
    }
}
