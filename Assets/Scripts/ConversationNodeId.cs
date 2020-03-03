using Newtonsoft.Json;

public struct ConversationNodeId {
    public static readonly ConversationNodeId EmptyId = new ConversationNodeId("");
    public static readonly ConversationNodeId ExitId = new ConversationNodeId("EXIT");

    public string Id { get; }

    [JsonConstructor]
    public ConversationNodeId(string id) {
        Id = id;
    }

    public override bool Equals(System.Object obj) {
        if (obj == null) { return false; }

        if (!(obj is ConversationNodeId)) {
            return false;
        } else {
            ConversationNodeId nodeId = (ConversationNodeId)obj;
            return this.Id.Equals(nodeId.Id);
        }
    }

    public override int GetHashCode() {
        return Id.GetHashCode();
    }
}
