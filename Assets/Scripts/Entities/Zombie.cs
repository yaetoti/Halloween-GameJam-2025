using System;
using Unity.VisualScripting;
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

  [DoNotSerialize]
  public ObservableValue<State> state;
  private Vector3? m_targetPosition;
  private HealthSystem m_targetHealth;
  private float m_breakingProgress;

  public State CurrentState {
    get => state.Value;
    set {
      if (value != state.Value) {
        state.Value = value;
      }
    }
  }

  private void Awake() {
    m_rigidbody = GetComponent<Rigidbody2D>();
    m_healthSystem = GetComponent<HealthSystem>();
    m_healthSystem.health.AddListener(OnHealthChange);
    
    state = new(State.IDLE);
  }

  private void OnDestroy() {
    m_healthSystem.health.RemoveListener(OnHealthChange);
  }

  private void Update() {
    // Come close, start breaking
    if (CurrentState == State.PURSUING) {
      Vector2 direction = m_targetPosition!.Value - transform.position;
      float distance = direction.magnitude;
      
      // Rotate towards the target
      float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
      rendererTransform.rotation = Quaternion.Euler(0, 0, angle);
      
      // Check breaking condition
      if (distance <= stopDistance) {
        CurrentState = State.BREAKING;
        m_breakingProgress = 0.0f;
        // TODO inertia
        m_rigidbody.linearVelocity = new();
      }
    }

    // Deal damage once in a while
    if (CurrentState == State.BREAKING) {
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
    // if (m_state == State.PURSUING) {
    //   Vector2 direction = m_targetPosition!.Value - transform.position;
    //   float distance = direction.magnitude;
    //   // Do not overshoot
    //   float desiredSpeed = Mathf.Min(distance, moveSpeed);
    //   
    //   // Preform movement
    //   var damping = m_rigidbody.linearVelocity;
    //   var impulse = direction.normalized * desiredSpeed - damping;
    //   m_rigidbody.AddForce(impulse, ForceMode2D.Impulse);
    // }
    
    // Move if not close enough
    if (CurrentState != State.PURSUING) {
      m_rigidbody.linearVelocity = new();
    }
    
    if (CurrentState == State.PURSUING) {
      Vector2 direction = m_targetPosition!.Value - transform.position;
      float distance = direction.magnitude;

      // If close enough, stop moving
      if (distance < stopDistance) {
        m_rigidbody.linearVelocity = Vector2.zero;
        return;
      }

      direction.Normalize();

      // Steering behaviour
      Vector2 desiredVelocity = direction * moveSpeed;
      Vector2 steering = desiredVelocity - m_rigidbody.linearVelocity;

      m_rigidbody.AddForce(steering, ForceMode2D.Force);
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
      CurrentState = State.IDLE;
      return;
    }
    
    CurrentState = State.PURSUING;
    m_targetPosition = targetPosition;
    m_targetHealth = targetHealth;
  }
}