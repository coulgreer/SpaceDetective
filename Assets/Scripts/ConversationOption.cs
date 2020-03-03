public class ConversationOption : IConversationOption {
    public string Response { get; }
    public ConversationNodeId DefaultDestinationId { get; }
    public ConversationNodeId HiddenDestinationId { get; }
    public bool IsHidden { get { return false; } }

    public KeyId KeyId { get { return KeyId.EmptyKeyId; } }

    public ConversationOption(
        string response,
        ConversationNodeId defaultDestinationId,
        ConversationNodeId hiddenDestinationId) {

        this.Response = response;

        this.DefaultDestinationId = defaultDestinationId;
        this.HiddenDestinationId = hiddenDestinationId;
    }

    public ConversationOption(
        string response,
        ConversationNodeId defaultDestinationId)
        : this(response, defaultDestinationId, ConversationNodeId.EmptyId) { }

    public override bool Equals(System.Object obj) {
        if (obj == null) { return false; }

        ConversationOption option = obj as ConversationOption;
        if (option == null) {
            return false;
        } else {
            return this.Response.Equals(option.Response, System.StringComparison.InvariantCultureIgnoreCase)
                && this.DefaultDestinationId.Equals(option.DefaultDestinationId)
                && this.HiddenDestinationId.Equals(option.HiddenDestinationId);
        }
    }

    public override int GetHashCode() {
        return Response.GetHashCode() ^ DefaultDestinationId.GetHashCode() ^ HiddenDestinationId.GetHashCode();
    }
}
