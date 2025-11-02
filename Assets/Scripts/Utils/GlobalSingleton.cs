using UnityEngine;

public abstract class GlobalSingleton<T> : MonoBehaviour where T : GlobalSingleton<T> {
  public static T Instance { get; private set; }

  protected virtual void Awake() {
    Initialize();
  }

  protected virtual void OnEnable() {
#if UNITY_EDITOR
    Initialize();
#endif
  }

  protected virtual void OnDestroy() {
    if (Instance == this) {
      Instance = null;
    }
  }

  private void Initialize() {
    if (Instance == this) {
      return;
    }

    if (Instance is not null) {
      Destroy(this);
      return;
    }

    Instance = (T)this;
    DontDestroyOnLoad(this);
    OnInitializeSingleton();
  }

  protected virtual void OnInitializeSingleton() {
  }
}