using Unity.VisualScripting;
using UnityEngine;

// Add UI to my scene

// Some ones are not moving
// Give random speed
// Increase default speed


public class Ghost : MonoBehaviour {
  public enum State {
    IDLE,
    PURSUING,
  }
  
  // Params
  [SerializeField]
  private Transform rendererTransform;
  [SerializeField]
  private float moveSpeedMin = 1.0f;
  [SerializeField]
  private float moveSpeedMax = 3.0f;
  [SerializeField]
  private float stopDistance = 0.01f;
  [SerializeField]
  private float standingTimeSecondsMin = 0.3f;
  [SerializeField]
  private float standingTimeSecondsMax = 3.0f;

  public float targetOffset = 0.1f;

  // State
  private Rigidbody2D m_rigidbody;
  private HealthSystem m_healthSystem;

  [DoNotSerialize]
  public ObservableValue<State> state;
  private Vector3? m_targetPosition;
  private float m_standingTimeSeconds;
  private float m_standingProgress;
  private float m_moveSpeed;

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
    
    m_moveSpeed = Random.Range(moveSpeedMin, moveSpeedMax);
    state = new(State.IDLE);
    UpdateWaitingTime();
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
      // TODO remove THE FUCK OUT OF HERE
      float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
      rendererTransform.rotation = Quaternion.Euler(0, 0, angle);
      
      // Check stopping condition
      if (distance <= stopDistance) {
        CurrentState = State.IDLE;
        UpdateWaitingTime();
        // TODO inertia
        m_rigidbody.linearVelocity = new();
      }
    }

    // Deal damage once in a while
    if (CurrentState == State.IDLE) {
      m_standingProgress += Time.deltaTime / m_standingTimeSeconds;
      
      // Pursue the new target
      if (m_standingProgress >= 1.0f) {
        m_standingProgress = 0.0f;
        UpdatePosition();
        CurrentState = State.PURSUING;
      }
    }
    
    // Idle handling. Do not touch
  }

  private void FixedUpdate() {
    // Move if not close enough
    if (CurrentState != State.PURSUING) {
      m_rigidbody.linearVelocity = new();
    }
    
    if (CurrentState == State.PURSUING) {
      Vector2 direction = m_targetPosition!.Value - transform.position;
      float distance = direction.magnitude;

      // If close enough, stop moving, wait a bit, go to the new position
      if (distance < stopDistance) {
        CurrentState = State.IDLE;
        m_rigidbody.linearVelocity = Vector2.zero;
        UpdateWaitingTime();
        return;
      }

      direction.Normalize();

      // Steering behaviour
      Vector2 desiredVelocity = direction * m_moveSpeed;
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

  private void UpdateWaitingTime() {
    m_standingProgress = 0.0f;
    m_standingTimeSeconds = Random.Range(standingTimeSecondsMin, standingTimeSecondsMax);
  }

  private void UpdatePosition() {
    m_targetPosition = GhostSpawnManager.Instance.GetRandomPosition();

    // Random offset
    float randomX = Random.Range(-targetOffset, targetOffset);
    float randomY = Random.Range(-targetOffset, targetOffset);
    m_targetPosition = m_targetPosition.Value + new Vector3(randomX, randomY, 0);
  }
}