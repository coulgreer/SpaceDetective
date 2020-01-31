using UnityEngine;

public class PlayerCharacterController : MonoBehaviour {
    public Vector2 FacingDirection { get; set; }
    public IPlayerState PlayerState { private get; set; }
    public Rigidbody2D Rigidbody2D { get; private set; }

    // Start is called before the first frame update
    void Start() {
        FacingDirection = Vector2.left;
        PlayerState = new ExploringPlayerState();
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        PlayerState.HandleInput(this);
    }
}
