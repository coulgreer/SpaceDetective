using System.Collections.Generic;
using Newtonsoft.Json;

[JsonConverter(typeof(ConversationNodeConverter))]
public interface IConversationNode {
    ConversationNodeId Id { get; }
    Speaker Speaker { get; }
    string Prompt { get; }
    IList<IConversationOption> Options { get; }
    bool IsHidden { get; }
}
