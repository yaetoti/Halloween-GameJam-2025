using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour {
  public static GameManager Instance { get; private set; }
  
  // Params
  public float lureRadius;
  public float hammerRadius;
  // TODO: Will we need initial value? If so, create zombieSpawnRateInitial and change getter
  public float zombieSpawnRate; // TODO spawner is controlling it
  public float initialDoorHealth; // TODO door is controlling it
  public float gameLengthSeconds; // TODO clock is controlling it

  [SerializeField]
  private Clock clockObject;
  [SerializeField]
  private GameObject doorObject;
  [SerializeField]
  private List<GameObject> lureObjects = new();
  [SerializeField]
  private List<GameObject> zombieObjects = new();
  [SerializeField]
  private List<GameObject> ghostObjects = new();

  // State (initialization is in Initialize())
  private RoomType m_currentRoom;
  private ToolType m_activeTool;
  private int m_candyCount;
  private float m_doorHealth; // TODO remove. We have a door gameObject
  private float m_clockState; // TODO remove. We have clock gameObject
  private float gameTimeSeconds;
  
  // Entities
  
  
  // Door is just.. HealthBar, Health
  // +entities arrays
  // TODO Add entities: ghosts, zombies
  
  // Place LMB: in trigger, no overlaps, enough candies -> Add Lure at this position + Register
  
  // TODO InteractionManager (or player)
  // TODO SpawnManager
  
  
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

    m_currentRoom = RoomType.LIVING_ROOM;
    m_activeTool = ToolType.HAMMER;
    m_candyCount = 0;
    m_clockState = 0.0f;
    m_doorHealth = initialDoorHealth;
    gameTimeSeconds = 0.0f;
    
    Debug.Log("GameManager initialized");
  }

  private void Update() {
    // TODO state check (return if not playing)
    
    gameTimeSeconds += Time.deltaTime;

    if (gameTimeSeconds >= gameLengthSeconds) {
      // TODO check win/lose conditions
      // TODO change state
      // TODO activate cutscene
    }
  }

  
  // === Scene management ===
  
  
  public Clock GetClock() {
    return clockObject;
  }

  public GameObject GetDoor() {
    return doorObject;
  }
  
  public List<GameObject> GetLureObjects() {
    return lureObjects;
  }
  
  public List<GameObject> GetZombieObjects() {
    return zombieObjects;
  }
  
  public List<GameObject> GetGhostObjects() {
    return ghostObjects;
  }

  public void RegisterLure(GameObject obj) {
    lureObjects.Add(obj);
  }
  
  public void RegisterZombie(GameObject obj) {
    zombieObjects.Add(obj);
  }
  
  public void RegisterGhost(GameObject obj) {
    ghostObjects.Add(obj);
  }

  public void UnregisterLure(GameObject obj) {
    lureObjects.Remove(obj);
  }
  
  public void UnregisterZombie(GameObject obj) {
    zombieObjects.Remove(obj);
  }
  
  public void UnregisterGhost(GameObject obj) {
    ghostObjects.Remove(obj);
  }
  
  
  // === State management ===
  
  
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
    // TODO transition (?)
  }
  
  public float GetDoorHealth() {
    return m_doorHealth;
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
