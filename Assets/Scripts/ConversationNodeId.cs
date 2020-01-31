using Newtonsoft.Json;

public class ConversationNodeId {
    public string Id { get; }

    [JsonConstructor]
    public ConversationNodeId(string id) {
        Id = id;
    }

    public override bool Equals(System.Object obj) {
        if (obj == null) { return false; }

        ConversationNodeId nodeId = obj as ConversationNodeId;
        if (nodeId == null) {
            return false;
        }
        else {
            return this.Id.Equals(nodeId.Id);
        }
    }

    public override int GetHashCode() {
        return Id.GetHashCode();
    }
}
