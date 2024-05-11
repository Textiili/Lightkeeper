using Unity.VisualScripting;
using UnityEngine;

public class EnterBuildingState : PlayerState {
    private Rigidbody2D rb2d;
    private float stateDuration = 0.7f;
    private float elapsedTime = 0f;
    private float speed = 2f;

    private Animator animator;
    private string currentState;
    public override void EnterState(PlayerController state) {
        
        animator = state.GetComponent<Animator>();
        rb2d = state.GetComponent<Rigidbody2D>();
    }
    public override void UpdateState(PlayerController state) {
        ChangeAnimationState(PlayerAnimations.Player_Move_Up.ToString());
    }
    public override void FixedUpdateState(PlayerController state) {
        if (elapsedTime < stateDuration) {
            rb2d.MovePosition(rb2d.position + (Vector2.up * speed * Time.fixedDeltaTime));
            elapsedTime += Time.deltaTime;
        } else {
            elapsedTime = 0f;
            state.SwitchPlayerState(state.playerMove);
        }
    }

    private void ChangeAnimationState(string newState) {
        //If-lause, jotta animaatiolooppi ei ylikirjoita itseään.
        if (currentState == newState) return;
        animator.Play(newState, 0);
    }
}
