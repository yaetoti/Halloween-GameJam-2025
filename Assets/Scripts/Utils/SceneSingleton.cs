using UnityEngine;

public abstract class SceneSingleton<T> : MonoBehaviour where T : SceneSingleton<T> {
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
    OnInitializeSingleton();
  }

  protected virtual void OnInitializeSingleton() { }
}