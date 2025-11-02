using UnityEngine;

public class Ghost : MonoBehaviour {
  private HealthSystem m_healthSystem;

  private void Awake() {
    m_healthSystem = GetComponent<HealthSystem>();
    m_healthSystem.health.AddListener(OnHealthChange);
  }

  private void OnDestroy() {
    m_healthSystem.health.RemoveListener(OnHealthChange);
  }

  private void Start() {
    
  }

  private void Update() {
  }

  private void OnHealthChange(int health) {
    // DIE MOTHERFUCKER DIE MOTHERFUCKER DIEEEEEE
    if (health <= 0) {
      Destroy(gameObject);
    }
  }
}