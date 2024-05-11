using UnityEngine;

public class GameRunningState : GameState
{
    public override void EnterState(GameManager state) {
        Time.timeScale = 1;
    }

    public override void UpdateState(GameManager state) {
    }
}
