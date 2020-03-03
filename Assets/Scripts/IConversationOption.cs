using Newtonsoft.Json;

[JsonConverter(typeof(ConversationOptionConverter))]
public interface IConversationOption : IKey {

    string Response { get; }
    ConversationNodeId DefaultDestinationId { get; }
    ConversationNodeId HiddenDestinationId { get; }
    bool IsHidden { get; }
}
