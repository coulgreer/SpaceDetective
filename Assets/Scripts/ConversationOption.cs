public class ConversationOption {
    public string Response { get; }
    public ConversationNodeId DestinationId { get; }

    public ConversationOption(string response, ConversationNodeId destinationId) {
        Response = response;
        DestinationId = destinationId;
    }

    public override bool Equals(System.Object obj) {
        if (obj == null) { return false; }

        ConversationOption option = obj as ConversationOption;
        if (option == null) {
            return false;
        }
        else {
            return (this.Response.Equals(option.Response, System.StringComparison.InvariantCultureIgnoreCase)) && (this.DestinationId.Equals(option.DestinationId));
        }
    }

    public override int GetHashCode() {
        return Response.GetHashCode() ^ DestinationId.GetHashCode();
    }
}
