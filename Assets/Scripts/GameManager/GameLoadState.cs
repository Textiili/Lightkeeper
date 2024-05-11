using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoadState : GameState
{
    private Animator curtain;
    
    public override void EnterState(GameManager state)
    {
        curtain = state.GetComponent<Animator>();
        curtain.Play("FadeOut");
    }

    public override void UpdateState(GameManager state)
    {   //v Aloitetaan lataus kun esirippu on laskettu!
        if (state.curtainOn) {
            var sceneName = state.Scene.ToString();
            SceneManager.LoadScene(sceneName);
            if (SceneManager.GetSceneByName(sceneName).isLoaded) {
                Debug.Log($"{sceneName} is ready!");

                var player = GameObject.Find("Player");
                player.transform.position = state.spawnPoint;
                if (state.isBuilding) {
                    var playerController = player.GetComponent<PlayerController>();
                    playerController.SwitchPlayerState(playerController.enterBuilding);
                }
                state.curtainOn = false;
                curtain.Play("FadeIn");
            }
        }
    }
}
