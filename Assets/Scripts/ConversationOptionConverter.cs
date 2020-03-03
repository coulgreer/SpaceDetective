using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

public class ConversationOptionConverter : JsonConverter {
    public override bool CanConvert(Type objectType) {
        return typeof(IConversationOption).IsAssignableFrom(objectType);
    }

    public override object ReadJson(
        JsonReader reader,
        Type objectType,
        object existingValue,
        JsonSerializer serializer) {

        JObject jObject = JObject.Load(reader);

        return ParseConversationOption(jObject);
    }

    private IConversationOption ParseConversationOption(JObject jObject) {
        IConversationOption option = CreateBaseConversationOption(jObject);
        if (option != null) {
            option = AddLocksToOption(option, jObject);
            option = AddKeyToOption(option, jObject);
        }

        return option;
    }

    private ConversationOption CreateBaseConversationOption(JObject jObject) {
        string response = jObject["Response"].Value<string>();
        ConversationNodeId destinationNodeId = jObject["DestinationId"].ToObject<ConversationNodeId>();
        ConversationNodeId hiddenDestinationNodeId = jObject["HiddenDestinationId"] != null
            ? jObject["HiddenDestinationId"].ToObject<ConversationNodeId>()
            : ConversationNodeId.EmptyId;

        return new ConversationOption(response, destinationNodeId, hiddenDestinationNodeId);
    }

    private IConversationOption AddLocksToOption(IConversationOption option, JObject jObject) {
        if (option == null) { return option; }

        JArray locks = jObject["Locks"] as JArray;

        if (locks != null) {
            foreach (JToken jToken in locks) {
                option = CreateOptionWithLock(option, jToken);
            }
        }

        return option;
    }

    private HiddenConversationOption CreateOptionWithLock(IConversationOption option, JToken jToken) {
        KeyId targetKeyId = jToken.ToObject<KeyId>();
        Lock aLock = new Lock(targetKeyId);
        HiddenConversationOption hiddenOption = new HiddenConversationOption(option, aLock);

        Inventory.KeyAdded += aLock.UseKey;

        return hiddenOption;
    }

    private IConversationOption AddKeyToOption(IConversationOption option, JObject jObject) {
        JToken jToken = jObject["Key"];

        if (jToken != null) {
            KeyId keyId = jToken.ToObject<KeyId>();

            option = new KeyConversationOption(option, keyId);
        }

        return option;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
        throw new NotImplementedException();
    }
}
