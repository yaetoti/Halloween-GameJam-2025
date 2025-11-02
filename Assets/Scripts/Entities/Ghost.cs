using UnityEngine;

public class Ghost : MonoBehaviour {
  private Health m_health;

  private void Awake() {
    m_health = GetComponent<Health>();
    m_health.health.AddListener(OnHealthChange);
  }

  private void OnDestroy() {
    m_health.health.RemoveListener(OnHealthChange);
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