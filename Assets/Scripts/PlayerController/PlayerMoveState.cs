using UnityEngine;

public class PlayerMoveState : PlayerState {
    private Rigidbody2D rb2d;
    private Vector2 movement;
    private float speed = 2.5f;

    private float lastHorizontal;
    private float lastVertical;

    private Animator animator;
    private string currentState;
    public override void EnterState(PlayerController state)
    {   
        animator = state.GetComponent<Animator>();
        rb2d = state.GetComponent<Rigidbody2D>();
        //v Jotta pelaaja aloittaa Player_Idle_Forward-animaatiolla
        lastHorizontal = 0f; lastVertical = -1f;
    }
    public override void UpdateState(PlayerController state)
    {
        //==================ACTIONS=====================================
        if (Input.GetKeyDown(KeyCode.E) && state.interaction != null ||
            Input.GetKeyDown(KeyCode.Return) && state.interaction != null) {
            state.interaction.Interact();
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        //==================ANIMATION===================================
        if (movement.x != 0 || movement.y != 0)
        {
            lastHorizontal = movement.x;
            lastVertical = movement.y;

            if (movement.x < 0)
            {
                ChangeAnimationState(PlayerAnimations.Player_Move_Left.ToString());
            } 
            else if (movement.x > 0)
            {
                ChangeAnimationState(PlayerAnimations.Player_Move_Right.ToString());
            } 
            else if (movement.y > 0)
            {
                ChangeAnimationState(PlayerAnimations.Player_Move_Up.ToString());
            }
            else if (movement.y < 0)
            {
                ChangeAnimationState(PlayerAnimations.Player_Move_Down.ToString());
            }
        } 
        else if (lastVertical < 0)
        {
            ChangeAnimationState(PlayerAnimations.Player_Idle_Forward.ToString());
        } 
        else if (lastVertical > 0) 
        {
            ChangeAnimationState(PlayerAnimations.Player_Idle_Backward.ToString());
        }
        else if (lastHorizontal < 0)
        {
            ChangeAnimationState(PlayerAnimations.Player_Idle_Left.ToString());
        } 
        else if (lastHorizontal > 0)
        {
            ChangeAnimationState(PlayerAnimations.Player_Idle_Right.ToString());
        }

        //Sprintti ja sprintin animointi
        if (movement.x != 0 || movement.y != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                animator.speed = 2;
                speed = 3.5f;
            }
            else
            {
                animator.speed = 1;
                speed = 2.5f;
            }
        } else
        {
            animator.speed = 1;
        }
    }
    public override void FixedUpdateState(PlayerController state)
    {
        rb2d.MovePosition(rb2d.position + movement * speed * Time.fixedDeltaTime);
    }

    private void ChangeAnimationState(string newState)
    {   //==================ANIMATION===================================
        //If-lause, jotta animaatiolooppi ei ylikirjoita itse‰‰n.
        if (currentState == newState) return;
        animator.Play(newState, 0);
    }
}
