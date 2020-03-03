using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

public class ConversationNodeConverter : JsonConverter {
    public override bool CanConvert(Type objectType) {
        return typeof(IConversationNode).IsAssignableFrom(objectType);
    }

    public override object ReadJson(
        JsonReader reader,
        Type objectType,
        object existingValue,
        JsonSerializer serializer) {

        JObject jObject = JObject.Load(reader);

        return ParseConversationNode(jObject);
    }

    private IConversationNode ParseConversationNode(JObject jObject) {
        IConversationNode node = CreateBaseConversationNode(jObject);
        node = AddLocksToNode(node, jObject);

        return node;
    }

    private ConversationNode CreateBaseConversationNode(JObject jObject) {
        ConversationNode node = null;

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
        ConversationNodeId defaultDestinationId = jObject["DestinationId"].ToObject<ConversationNodeId>();
        ConversationNodeId hiddenDestinationId = jObject["HiddenDestinationId"] != null
            ? jObject["HiddenDestinationId"].ToObject<ConversationNodeId>()
            : ConversationNodeId.EmptyId;

        return new ConversationNode(id, speaker, prompt, defaultDestinationId, hiddenDestinationId);
    }

    private ConversationNode CreateBranchingNode(JObject jObject) {
        ConversationNodeId id = jObject["Id"].ToObject<ConversationNodeId>();
        Speaker speaker = jObject["Speaker"].ToObject<Speaker>();
        string prompt = jObject["Prompt"].Value<string>();
        List<IConversationOption> options = jObject["Options"].ToObject<List<IConversationOption>>();

        return new ConversationNode(id, speaker, prompt, options);
    }

    private IConversationNode AddLocksToNode(IConversationNode node, JObject jObject) {
        if (node == null) { return node; }

        JArray locks = jObject["Locks"] as JArray;

        if (locks != null) {
            foreach (JToken jToken in locks) {
                node = CreateNodeWithLock(node, jToken);
            }
        }

        return node;
    }

    private HiddenConversationNode CreateNodeWithLock(IConversationNode node, JToken jToken) {
        KeyId targetKeyId = jToken.ToObject<KeyId>();
        Lock aLock = new Lock(targetKeyId);
        HiddenConversationNode hiddenNode = new HiddenConversationNode(node, aLock);

        Inventory.KeyAdded += aLock.UseKey;

        return hiddenNode;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
        throw new NotImplementedException();
    }
}
