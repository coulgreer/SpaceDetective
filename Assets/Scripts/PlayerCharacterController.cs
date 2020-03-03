using UnityEngine;

public class PlayerCharacterController : MonoBehaviour {
    public Vector2 FacingDirection { get; set; }
    public IPlayerState PlayerState { private get; set; }
    public Rigidbody2D Rigidbody2D { get; private set; }

    void OnEnable() {
        ConversationController.Conversed += delegate { PlayerState = new ExploringPlayerState(gameObject); };
        ConversationController.Conversing += delegate { PlayerState = new ConversingPlayerState(gameObject); };
    }

    void OnDisable() {
        ConversationController.Conversed -= delegate { PlayerState = new ExploringPlayerState(gameObject); };
        ConversationController.Conversing -= delegate { PlayerState = new ConversingPlayerState(gameObject); };
    }

    // Start is called before the first frame update
    void Start() {
        FacingDirection = Vector2.left;
        PlayerState = new ExploringPlayerState(gameObject);
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        PlayerState.HandleInput();
    }
}
