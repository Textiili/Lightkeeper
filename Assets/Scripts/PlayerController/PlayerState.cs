public abstract class PlayerState {
    public abstract void EnterState(PlayerController state);
    public abstract void UpdateState(PlayerController state);
    public abstract void FixedUpdateState(PlayerController state);
}
