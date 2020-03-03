public class KeyConversationOption : IConversationOption {

    public string Response { get { return Option.Response; } }
    public ConversationNodeId DefaultDestinationId { get { return Option.DefaultDestinationId; } }
    public ConversationNodeId HiddenDestinationId { get { return Option.HiddenDestinationId; } }
    public bool IsHidden { get { return Option.IsHidden; } }

    public KeyId KeyId { get; }

    protected IConversationOption Option { get; }

    public KeyConversationOption(IConversationOption option, KeyId keyId) {
        this.Option = option;
        this.KeyId = keyId;
    }

    public override bool Equals(object obj) {
        if (obj == null) { return false; }

        KeyConversationOption option = obj as KeyConversationOption;
        if (option == null) {
            return false;
        } else {
            return this.Option.Equals(option.Option) && this.KeyId.Equals(option.KeyId);
        }
    }

    public override int GetHashCode() {
        return Option.GetHashCode() ^ KeyId.GetHashCode();
    }
}
