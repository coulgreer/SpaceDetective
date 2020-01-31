public class ConversingPlayerState : IPlayerState {
    public void HandleInput(PlayerCharacterController controller) {
        if (ConversationController.Instance.IsFinishedConversing) {
            controller.PlayerState = new ExploringPlayerState();
        }
    }
}
