using UnityEngine;

public class ExploringPlayerState : IPlayerState {
    private const float Speed = 3.0f;

    public GameObject Player { get; }

    public ExploringPlayerState(GameObject player) {
        this.Player = player;
    }

    public void HandleInput() {
        PlayerCharacterController controller = Player.GetComponent<PlayerCharacterController>();

        UpdatePosition(controller);

        if (Input.GetKeyDown(KeyCode.X)) {
            TriggerConversation(controller);
        }
    }

    private void UpdatePosition(PlayerCharacterController controller) {
        float horizontalDelta = Input.GetAxis("Horizontal");
        Vector2 move = new Vector2(horizontalDelta, 0.0f);

        bool isMovingHorizontal = !Mathf.Approximately(move.x, 0.0f);
        if (isMovingHorizontal) {
            controller.FacingDirection.Set(move.x, 0.0f);
            controller.FacingDirection.Normalize();
        }

        Vector2 position = controller.Rigidbody2D.position;
        position += move * Speed * Time.deltaTime;
        controller.Rigidbody2D.MovePosition(position);
    }

    private void TriggerConversation(PlayerCharacterController controller) {
        RaycastHit2D hit = Physics2D.Raycast(
            controller.Rigidbody2D.position,
            controller.FacingDirection, 1.5f,
            LayerMask.GetMask("NPC"));

        if (hit.collider != null) {
            Conversation npcConversation = hit.collider.GetComponent<Conversation>();
            if (npcConversation != null) {
                controller.PlayerState = new ConversingPlayerState(Player);
                ConversationController.Instance.SetPlayer(Player);
                ConversationController.Instance.SetConversation(npcConversation);
                ConversationController.Instance.SetDialogueVisibility(true);
            }
        }
    }
}
