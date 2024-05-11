using System.Persistence;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour {
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        if (!EventSystem.current.currentSelectedGameObject) {
            EventSystem.current.SetSelectedGameObject(GameObject.Find("Menu").transform.Find("New Game").gameObject);
        }
    }

    public void NewGameButton() {
        var saveLoadSystem = FindFirstObjectByType<SaveLoadSystem>();
        saveLoadSystem.NewGame();
    }

    public void LoadGameButton() {
        var saveLoadSystem = FindFirstObjectByType<SaveLoadSystem>();
        saveLoadSystem.LoadGame("Data"); //TODO: Peli nimellä!
    }

    public void QuitGameButton() {
        Debug.Log("Peli lopetettu tallentamatta.");
        Application.Quit();
    }
}
