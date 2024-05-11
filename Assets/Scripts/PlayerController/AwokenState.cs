using Unity.VisualScripting;
using UnityEngine;

public class AwokenState : PlayerState {
    private SpriteRenderer character;
    private GameObject lantern;

    public override void EnterState(PlayerController state) {
        character = state.GetComponent<SpriteRenderer>();
        lantern = state.transform.Find("Lantern").GameObject();
        character.enabled = true;
        lantern.SetActive(true);
        state.SwitchPlayerState(state.playerMove);
    }
    public override void UpdateState(PlayerController state) {
    }
    public override void FixedUpdateState(PlayerController state) {
    }
}
