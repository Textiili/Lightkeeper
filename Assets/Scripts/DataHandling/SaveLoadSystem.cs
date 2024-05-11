using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace System.Persistence
{
    [Serializable]
    public class GameData {
        public string Name;
        public string CurrentLevelName;
        public PlayerData playerData;
        public PostboxData postboxData;
        public BedData bedData;
    }

    public interface ISaveable {
         public string Id { get; set; }
    }

    public interface IBind<TData> where TData : ISaveable {//TODO: ID
        void Bind(TData data);
    }

    public class SaveLoadSystem : PersistentSingleton<SaveLoadSystem>
    {
        [SerializeField] public GameData gameData;
        IDataService dataService;

        protected override void Awake() {
            base.Awake();
            dataService = new FileDataService(new JsonSerializer());
        }

        void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
        void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            if (scene.name == "Bootstrap" || scene.name == "MainMenu") return;
            gameData.CurrentLevelName = scene.name;
            Bind<Postbox, PostboxData>(gameData.postboxData);
            Bind<PlayerController, PlayerData>(gameData.playerData);
            Bind<Bed, BedData>(gameData.bedData);
        }

        void Bind<T, TData>(TData data) where T : MonoBehaviour, IBind<TData> where TData : ISaveable, new() {
            var entity = FindObjectsByType<T>(FindObjectsSortMode.None).FirstOrDefault();
            if (entity != null) {
                if (data == null) {
                    data = new TData();
                }
                entity.Bind(data);
            }
        }

        public void NewGame() {
            gameData = new GameData {
                Name = "Data",
                CurrentLevelName = "LightkeepersHouse",
                playerData = new PlayerData {
                    playerStateString = "SleepingState",
                    position = new Vector2(-0.5f, -0.5f)
                },
                postboxData = new PostboxData {
                hasPost = true
                },
                bedData = new BedData {
                inUse = true
                }
            };
            dataService.Save(gameData, true);
            SceneManager.LoadScene(SceneEnums.Bootstrap.ToString());
        }

        public void SaveGame() {
            dataService.Save(gameData); 
        }

        public void LoadGame(string gameName) {
            gameData = dataService.Load(gameName);
            SceneManager.LoadScene(SceneEnums.Bootstrap.ToString());
        }

        public void DeleteGame(string gameName) => dataService.Delete(gameName);
    }
}