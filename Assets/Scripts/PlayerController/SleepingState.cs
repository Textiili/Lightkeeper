using Unity.VisualScripting;
using UnityEngine;

public class SleepingState : PlayerState {
    private SpriteRenderer character;
    private GameObject lantern;

    public override void EnterState(PlayerController state) {
        if (!GameObject.Find("Bed")) return;
        var bedLocation = GameObject.Find("Bed");
        state.transform.position = bedLocation.transform.position;
        character = state.GetComponent<SpriteRenderer>();
        lantern = state.transform.Find("Lantern").GameObject();
        character.enabled = false;
        lantern.SetActive(false);
    }
    public override void UpdateState(PlayerController state) {
        //==================ACTIONS=====================================
        if (Input.GetKeyDown(KeyCode.E) && state.interaction != null ||
            Input.GetKeyDown(KeyCode.Return) && state.interaction != null) {
            state.interaction.Interact();
        }
    }
    public override void FixedUpdateState(PlayerController state) {
    }
}
