using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapState : GameState {
    private Animator curtain;

    public override void EnterState(GameManager state) {
        Debug.Log("Bootstrapping...");
        Time.timeScale = 0;
        curtain = state.GetComponent<Animator>();
        state.curtainOn = true;
        var sceneName = state.saveLoadSystem.gameData.CurrentLevelName;

        //Async lataus, jotta koodin toiminta ei katkea!
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single).completed += (AsyncOperation operation) => {
            if (SceneManager.GetSceneByName(sceneName).isLoaded) {
                Debug.Log($"{sceneName} is ready!");

                state.curtainOn = false;
                curtain.Play("FadeIn");
                state.SwitchGameState(state.RunningState);
            }
        };
    }

    public override void UpdateState(GameManager state) {
    }
}
