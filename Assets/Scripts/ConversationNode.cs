using System.Collections.Generic;

public class ConversationNode : IConversationNode {

    public ConversationNodeId Id { get; }
    public Speaker Speaker { get; }
    public string Prompt { get; }
    public IList<IConversationOption> Options { get; }
    public bool IsHidden { get; }

    private ConversationNode(ConversationNodeId id, Speaker speaker, string prompt) {
        Id = id;
        Speaker = speaker;
        Prompt = prompt;
        IsHidden = false;
    }

    public ConversationNode(
        ConversationNodeId id,
        Speaker speaker,
        string prompt,
        IList<IConversationOption> options)
        : this(id, speaker, prompt) {

        Options = options;
    }

    public ConversationNode(
        ConversationNodeId id,
        Speaker speaker,
        string prompt,
        ConversationNodeId destinationId)
        : this(id, speaker, prompt) {

        IConversationOption option = new ConversationOption("[continue...]", destinationId);

        List<IConversationOption> options = new List<IConversationOption>();
        options.Add(option);

        Options = options;
    }

    public ConversationNode(
        ConversationNodeId id,
        Speaker speaker,
        string prompt,
        ConversationNodeId defaultDestinationId,
        ConversationNodeId hiddenDestinationId)
        : this(id, speaker, prompt) {

        IConversationOption option = new ConversationOption(
            "[continue...]",
            defaultDestinationId,
            hiddenDestinationId);

        List<IConversationOption> options = new List<IConversationOption>();
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
