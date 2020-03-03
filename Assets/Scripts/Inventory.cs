using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public delegate void KeyAddedEventHandler(IKey key);
    public static event KeyAddedEventHandler KeyAdded;


    private IList<IKey> keyRing;

    public IList<IKey> KeyRing { get { return new List<IKey>(keyRing); } }

    public Inventory() {
        keyRing = new List<IKey>();
    }

    public void AddKey(IKey key) {
        if (key != null) {
            keyRing.Add(key);
            KeyAdded(key);
        }
    }
}
