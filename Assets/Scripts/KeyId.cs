using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public struct KeyId {
    public static readonly KeyId EmptyKeyId = new KeyId("");

    public string Id { get; }

    [JsonConstructor]
    public KeyId(string id) {
        Id = id;
    }

    public override bool Equals(System.Object obj) {
        if (obj == null) { return false; }

        if (!(obj is KeyId)) {
            return false;
        } else {
            KeyId keyId = (KeyId)obj;
            return this.Id.Equals(keyId.Id);
        }
    }

    public override int GetHashCode() {
        return Id.GetHashCode();
    }
}
