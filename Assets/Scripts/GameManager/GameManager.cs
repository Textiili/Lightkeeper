using System.Persistence;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : PersistentSingleton<GameManager> {
    GameState currentState;
    public BootstrapState BootstrapState = new BootstrapState();
    public GameRunningState RunningState = new GameRunningState();
    public GameLoadState LoadState = new GameLoadState();

    [HideInInspector] public SceneEnums Scene = SceneEnums.LightkeepBay;//TODO: vaihda/poista?
    [HideInInspector] public Vector2 spawnPoint = Vector2.zero;
    [HideInInspector] public bool isBuilding = false;
    [HideInInspector] public bool Paused { get; private set; } = false;
    [HideInInspector] public bool curtainOn = false;
    [HideInInspector] public SaveLoadSystem saveLoadSystem;


    private void Update() {//TODO: Bootstrapper script
        if (SceneManager.GetActiveScene().name == "Bootstrap" && currentState != BootstrapState) {
            saveLoadSystem = FindFirstObjectByType<SaveLoadSystem>();
            currentState = BootstrapState;
            currentState.EnterState(this);
        }
        currentState.UpdateState(this);

        if (Input.GetKeyDown(KeyCode.Escape)) {
            var saveLoadSystem = FindFirstObjectByType<SaveLoadSystem>();
            saveLoadSystem.SaveGame();
            SceneManager.LoadSceneAsync(SceneEnums.MainMenu.ToString(), LoadSceneMode.Single);
        }

        if (SceneManager.GetSceneByName("MainMenu").isLoaded) {
            Destroy(GameObject.Find("Player"));
            Destroy(gameObject);
        }
    }
    public void SwitchGameState(GameState state) {
        currentState = state;
        state.EnterState(this);
    }
    public void ChangeScene(SceneEnums changeTo, Vector2 spawnPoint, bool isBuilding) {
        Scene = changeTo;
        this.spawnPoint = spawnPoint;
        this.isBuilding = isBuilding;
        currentState = LoadState;
        currentState.EnterState(this);
    }

    public void Curtain() {
        curtainOn = true;
    }
}
