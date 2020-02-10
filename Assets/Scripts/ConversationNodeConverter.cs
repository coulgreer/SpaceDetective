using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

public class ConversationNodeConverter : JsonConverter {
    public override bool CanConvert(Type objectType) {
        return typeof(ConversationNode).IsAssignableFrom(objectType);
    }

    public override object ReadJson(
        JsonReader reader,
        Type objectType,
        object existingValue,
        JsonSerializer serializer) {

        ConversationNode node = null;
        JObject jObject = JObject.Load(reader);

        string type = jObject["Type"].Value<string>();
        if ("Linear".Equals(type, StringComparison.InvariantCultureIgnoreCase)) {
            node = CreateLinearNode(jObject);
        } else if ("Branching".Equals(type, StringComparison.InvariantCultureIgnoreCase)) {
            node = CreateBranchingNode(jObject);
        }

        return node;
    }

    private ConversationNode CreateLinearNode(JObject jObject) {
        ConversationNodeId id = jObject["Id"].ToObject<ConversationNodeId>();
        Speaker speaker = jObject["Speaker"].ToObject<Speaker>();
        string prompt = jObject["Prompt"].Value<string>();
        ConversationNodeId destinationId = jObject["DestinationId"].ToObject<ConversationNodeId>();

        return new ConversationNode(id, speaker, prompt, destinationId);
    }

    private ConversationNode CreateBranchingNode(JObject jObject) {
        ConversationNodeId id = jObject["Id"].ToObject<ConversationNodeId>();
        Speaker speaker = jObject["Speaker"].ToObject<Speaker>();
        string prompt = jObject["Prompt"].Value<string>();
        List<ConversationOption> options = jObject["Options"].ToObject<List<ConversationOption>>();

        return new ConversationNode(id, speaker, prompt, options);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
        throw new NotImplementedException();
    }
}
