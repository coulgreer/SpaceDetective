using UnityEngine;

public class ExploringPlayerState : IPlayerState {
    private const float Speed = 3.0f;

    public void HandleInput(PlayerCharacterController controller) {
        UpdatePosition(controller);

        if (Input.GetKeyDown(KeyCode.X)) {
            TriggerDialogue(controller);
        }
    }

    private void TriggerDialogue(PlayerCharacterController controller) {

        RaycastHit2D hit = Physics2D.Raycast(controller.Rigidbody2D.position, controller.FacingDirection, 1.5f, LayerMask.GetMask("NPC"));
        if (hit.collider != null) {
            Conversation manager = hit.collider.GetComponent<Conversation>();
            if (manager != null) {
                controller.PlayerState = new ConversingPlayerState();
                ConversationController.Instance.SetDialogueManager(manager);
                ConversationController.Instance.SetDialogueVisibility(true);
            }
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
}
