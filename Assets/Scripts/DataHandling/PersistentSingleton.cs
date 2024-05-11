using UnityEngine;

public class PersistentSingleton<T> : MonoBehaviour where T : Component {
    public bool AutoUnparentOnAwake = true;
    protected static T instance;

    public static bool hasInstance => instance != null;
    public static T TryGetInstance() => hasInstance ? instance : null;

    public static T Instance {
        get {
            if (instance == null) {
                var go = new GameObject(typeof(T).Name + "Auto-Generated");
                instance = go.AddComponent<T>();
            }
            return instance;
        }
    }

    protected virtual void Awake() {
        InitializeSingleton();
    }

    protected virtual void InitializeSingleton() {
        if (!Application.isPlaying) return;

        if (AutoUnparentOnAwake) {
            transform.SetParent(null);
        }

        if (instance == null) {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        } else {
            if (instance != this) {
                Destroy(gameObject);
            }
        }
    }
}

