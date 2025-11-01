using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour {
  public static GameManager Instance { get; private set; }
  
  // Params
  public float lureRadius;
  public float hammerRadius;
  // TODO: Will we need initial value? If so, create zombieSpawnRateInitial and change getter
  public float zombieSpawnRate;
  public float initialDoorHealth;
  public float gameLengthSeconds;
  
  // State (initialization is in Initialize())
  private RoomType m_currentRoom;
  private ToolType m_activeTool;
  private int m_candyCount;
  private float m_doorHealth;
  private float m_clockState;
  private float gameTimeSeconds;
  
  
  private void Awake() {
    Initialize();
  }

  private void OnEnable() {
    // Anti-crash for domain reloading
#if UNITY_EDITOR
    Initialize();
#endif
  }
  
  private void Initialize() {
    // Singleton init
    if (Instance == this) {  
      return;  
    }  
      
    if (Instance is not null) {
      Destroy(this);
      return;
    }  
    
    Instance = this;
    //DontDestroyOnLoad(this);
    
    // Real initialization

    m_currentRoom = RoomType.GARAGE;
    m_activeTool = ToolType.HAMMER;
    m_candyCount = 0;
    m_clockState = 0.0f;
    m_doorHealth = initialDoorHealth;
    gameTimeSeconds = 0.0f;
    
    Debug.Log("GameManager initialized");
  }

  public RoomType GetCurrentRoom() {
    return m_currentRoom;
  }
  
  public void SetCurrentRoom(RoomType type) {
    m_currentRoom = type;
    CameraManager.Instance.MoveToLocation(type);
  }

  public void ToggleRoom() {
    SetCurrentRoom(m_currentRoom == RoomType.GARAGE ? RoomType.LIVING_ROOM : RoomType.GARAGE);
  }
  
  public ToolType GetActiveTool() {
    return m_activeTool;
  }

  public void SetActiveTool(ToolType type) {
    m_activeTool = type;
    // TODO transition
  }
  
  public float GetDoorHealth() {
    return m_doorHealth;
  }

  public float GetClockState() {
    return m_clockState;
  }

  public void SetClockState(float state) {
    m_clockState = state;
  }
  
  public float GetTimeTillVictory() {
    return gameLengthSeconds - gameTimeSeconds;
  }

  public int GetGhostCountLivingRoom() {
    // TODO implement. array of gameobjects? where will we add them?
    return 0;
  }
  
  public int GetGhostCountGarage() {
    // TODO implement. array of gameobjects?
    return 0;
  }

  public int GetCandyCount() {
    return m_candyCount;
  }

  public float GetLureRadius() {
    return lureRadius;
  }
  
  public float GetHammerRadius() {
    return hammerRadius;
  }

  public float GetZombieSpawnRate() {
    return zombieSpawnRate;
  }
}
