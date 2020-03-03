using UnityEngine;

public class ConversingPlayerState : IPlayerState {
    public GameObject Player { get; }

    public ConversingPlayerState(GameObject player) {
        this.Player = player;
    }

    public void HandleInput() {
        /* Do Nothing until menu buttons are add */
    }
}
