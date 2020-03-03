public class HiddenConversationOption : IConversationOption {

    private Lock ALock;

    public string Response { get { return Option.Response; } }
    public ConversationNodeId DefaultDestinationId { get { return Option.DefaultDestinationId; } }
    public ConversationNodeId HiddenDestinationId { get { return Option.HiddenDestinationId; } }
    public bool IsHidden { get { return Option.IsHidden || ALock.IsLocked; } }
    public KeyId KeyId { get { return Option.KeyId; } }

    protected IConversationOption Option { get; }

    public HiddenConversationOption(IConversationOption option, Lock aLock) {
        this.Option = option;
        this.ALock = aLock;
    }

    public override bool Equals(object obj) {
        if (obj == null) { return false; }

        HiddenConversationOption option = obj as HiddenConversationOption;
        if (option == null) {
            return false;
        } else {
            return this.Option.Equals(option.Option)
                && this.ALock.Equals(option.ALock);
        }
    }

    public override int GetHashCode() {
        return Option.GetHashCode() ^ ALock.GetHashCode();
    }
}
