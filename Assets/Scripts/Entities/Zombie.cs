using System;
using UnityEngine;

public class Zombie : MonoBehaviour {
  public enum State {
    IDLE,
    PURSUING,
    BREAKING,
  }
  
  // What data?
  // - Get target location
  
  // How to get door location? AHH, we need a gamemanager
  // How to get lure location? AHH WE NEED A SPAWNER AHAHAHHAH "ahh" ahh energy
  
  
  
  // Params
  [SerializeField]
  private Transform rendererTransform;
  [SerializeField]
  private float moveSpeed = 1.0f;
  [SerializeField]
  private float stopDistance = 0.01f;
  [SerializeField]
  private float secondsToBreak = 1.0f;
  [SerializeField]
  private int damage = 1;

  // State
  private Rigidbody2D m_rigidbody;
  private HealthSystem m_healthSystem;

  private Vector3? m_targetPosition;
  private HealthSystem m_targetHealth;
  private State m_state;
  private float m_breakingProgress;

  private void Awake() {
    m_rigidbody = GetComponent<Rigidbody2D>();
    m_healthSystem = GetComponent<HealthSystem>();
    m_healthSystem.health.AddListener(OnHealthChange);
    
    m_state = State.IDLE;
  }

  private void OnDestroy() {
    m_healthSystem.health.RemoveListener(OnHealthChange);
  }

  private void Update() {
    // Come close, start breaking
    if (m_state == State.PURSUING) {
      Vector2 direction = m_targetPosition!.Value - transform.position;
      float distance = direction.magnitude;
      
      // Rotate towards the target
      float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
      rendererTransform.rotation = Quaternion.Euler(0, 0, angle);
      
      // Check breaking condition
      if (distance <= stopDistance) {
        m_state = State.BREAKING;
        m_breakingProgress = 0.0f;
      }
    }

    // Deal damage once in a while
    if (m_state == State.BREAKING) {
      m_breakingProgress += Time.deltaTime / secondsToBreak;
      
      // Deal damage
      if (m_breakingProgress >= 1.0f) {
        m_breakingProgress = 0.0f;

        if (m_targetHealth is not null) {
          m_targetHealth.Health -= damage;
        }
      }
    }
    
    // Idle handling. Do not touch
  }

  private void FixedUpdate() {
    // Move if not close enough
    if (m_state == State.PURSUING) {
      Vector2 direction = m_targetPosition!.Value - transform.position;
      float distance = direction.magnitude;
      // Do not overshoot
      float desiredSpeed = Mathf.Min(distance, moveSpeed);
      
      // Preform movement
      var damping = m_rigidbody.linearVelocity;
      var impulse = direction * desiredSpeed - damping;
      m_rigidbody.AddForce(impulse, ForceMode2D.Impulse);
    }
  }

  private void OnHealthChange(int health) {
    // DIE MOTHERFUCKER DIE MOTHERFUCKER DIEEEEEE
    if (health <= 0) {
      Destroy(gameObject);
    }
  }
  
  
  // --- State Management ---


  public void SetTarget(Vector3? targetPosition = null, HealthSystem targetHealth = null) {
    if (targetPosition is null) {
      m_state = State.IDLE;
      return;
    }
    
    m_state = State.PURSUING;
    m_targetPosition = targetPosition;
    m_targetHealth = targetHealth;
  }
}