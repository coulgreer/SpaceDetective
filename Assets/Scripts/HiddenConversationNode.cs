using System.Collections.Generic;

public class HiddenConversationNode : IConversationNode {

    private Lock ALock;

    public ConversationNodeId Id { get { return Node.Id; } }
    public Speaker Speaker { get { return Node.Speaker; } }
    public string Prompt { get { return Node.Prompt; } }
    public IList<IConversationOption> Options { get { return Node.Options; } }
    public bool IsHidden { get { return Node.IsHidden || ALock.IsLocked; } }

    private IConversationNode Node { get; }
    private KeyId TargetKeyId { get; }

    public HiddenConversationNode(IConversationNode node, Lock aLock) {
        this.Node = node;
        this.ALock = aLock;
    }

    public override bool Equals(object obj) {
        if (obj == null) { return false; }

        HiddenConversationNode node = obj as HiddenConversationNode;
        if (node == null) {
            return false;
        }

        return this.Node.Equals(node.Node)
            && this.ALock.Equals(node.ALock);
    }

    public override int GetHashCode() {
        return Node.GetHashCode() ^ ALock.GetHashCode();
    }
}
