using System;
using System.Collections.Generic;
using System.Persistence;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : PersistentSingleton<PlayerController>, IBind<PlayerData> {
    private Dictionary<string, PlayerState> stateMap = new();
    public PlayerState currentState;
    public PlayerMoveState playerMove = new PlayerMoveState();
    public EnterBuildingState enterBuilding = new EnterBuildingState();
    public SleepingState sleepingState = new SleepingState();
    public AwokenState awokenState = new AwokenState();
    
    [SerializeField] PlayerData data;
    public void Bind(PlayerData data) {
        this.data = data;
    }
    
    public IInteractable interaction;
    protected override void Awake() {//T‰‰ liittyy instanssiin?
        base.Awake();
        AddState("PlayerMoveState", playerMove);
        AddState("EnterBuildingState", enterBuilding);
        AddState("SleepingState", sleepingState);
        AddState("AwokenState", awokenState);
    }

    private void Start() {
        transform.position = data.position;
        SetStateWithString(data.playerStateString);
    }

    private void Update() {//INPUT
        data.position = transform.position;
        data.playerStateString = currentState.ToString();
        currentState.UpdateState(this);
    }

    private void FixedUpdate() {
        currentState.FixedUpdateState(this);
    } 

    private void OnTriggerEnter2D(Collider2D collision) {
        IInteractable test = collision.gameObject.GetComponent<IInteractable>();
        if (test != null) {
            interaction = collision.gameObject.GetComponent<IInteractable>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        IInteractable test = collision.gameObject.GetComponent<IInteractable>();
        if (test != null && interaction != null) {
            interaction = null;
        }
    }

    public void SwitchPlayerState(PlayerState state) {
        currentState = state;
        state.EnterState(this);
    }
    private void AddState(string stateName, PlayerState state) {
        stateMap.Add(stateName, state);
    }
    public void SetStateWithString(string stateName) {
        if (stateMap.ContainsKey(stateName)) {
            currentState = stateMap[stateName];
            currentState.EnterState(this);
        } else {
            Debug.LogWarning($"Could not get '{stateName}'! Defaulting to PlayerMoveState");
            currentState = playerMove;
            currentState.EnterState(this);
        }
    }
}
[Serializable]
public class PlayerData : ISaveable {
    [SerializeField] public string Id { get; set; }
    public string playerStateString;
    public Vector2 position;
}

