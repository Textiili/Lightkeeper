using UnityEngine;
using UnityEngine.Events;

public class EnterLevel : MonoBehaviour, IInteractable
{
    [Header("Leveli, joka ladataan:")]
    [SerializeField] private SceneEnums levelToEnter = SceneEnums.LightkeepBay;
    [Header("Rakennus?")]
    [SerializeField] private bool isBuilding = true;
    [Header("Spawnpoint")]
    [SerializeField] private Vector2 spawnPoint;
    [Header("Ladataanko, kun pelaaja on hitboxissa?")]
    public bool autoExecuteLoad = false;
    private GameManager gameManager;

    private void Awake() {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") && autoExecuteLoad) {
            Interact();
        }
    }

    public void Interact() {
        if (Application.CanStreamedLevelBeLoaded(levelToEnter.ToString())) {
            gameManager.ChangeScene(levelToEnter, spawnPoint, isBuilding);
        }
    }
}
