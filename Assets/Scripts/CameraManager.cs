using UnityEngine;

[System.Serializable]
public class RoomCameraData {
  public Vector2 position;
}

// Per-level singleton
public class CameraManager : MonoBehaviour {
  private enum State {
    IDLE,
    TRANSFER
  }

  public static CameraManager Instance { get; private set; }

  // Params
  public RoomCameraData livingRoomData;
  public RoomCameraData garageData;
  public Camera activeCamera;
  public float transferTimeSeconds = 1.0f;
  
  // State
  private Vector2 m_lastPosition;
  private Vector2 m_targetPosition;
  private float m_transferProgress = 0.0f;
  private State m_state = State.IDLE;
  
  private void Awake() {
    Initialize();
  }
  
  private void OnEnable() {
#if UNITY_EDITOR
    Initialize();
#endif
  }
  
  private void OnDestroy() {
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
  
    Instance = this;
  }
  
  private void LateUpdate() {
    if (m_state == State.TRANSFER) {
      // Finish
      if (m_transferProgress >= 1.0f) {
        m_state = State.IDLE;
        var cameraPos1 = activeCamera.transform.position;
        activeCamera.transform.position = new (m_targetPosition.x, m_targetPosition.y, cameraPos1.z);
      }

      m_transferProgress += Time.deltaTime / transferTimeSeconds;

      // TODO easing functions
      
      // Move to the desired location
      Vector2 position = Vector2.Lerp(m_lastPosition, m_targetPosition, m_transferProgress);

      // Save z coordinate
      var cameraPos = activeCamera.transform.position;
      activeCamera.transform.position = new (position.x, position.y, cameraPos.z);
    }
  }
  
  public void MoveToLocation(RoomType type) {
    switch (type) {
    case RoomType.GARAGE:
      m_targetPosition = garageData.position;
      break;
    case RoomType.LIVING_ROOM:
      m_targetPosition = livingRoomData.position;
      break;
    default:
      Debug.LogError("Unknown room type");
      break;
    }

    m_lastPosition = activeCamera.transform.position;
    Debug.Log("Last position: " + m_lastPosition);
    Debug.Log("Target position: " + m_targetPosition);
    Debug.Log("Transfer time: " + transferTimeSeconds);
    m_transferProgress = 0.0f;
    m_state = State.TRANSFER;
  }
}
