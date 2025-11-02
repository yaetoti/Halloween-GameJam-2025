using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameManager : SceneSingleton<GameManager> {
  // Params
  public float lureRadius;
  public float hammerRadius;
  // TODO: Will we need initial value? If so, create zombieSpawnRateInitial and change getter
  public float zombieSpawnRate; // TODO spawner is controlling it

  [SerializeField]
  private Clock clockObject;
  [SerializeField]
  private GameObject doorObject;

  // State (initialization is in Initialize())
  public ObservableValue<int> CandyCount { get; private set; }
  private HealthSystem m_doorHealthSystem;
  private GameState m_gameState;
  private RoomType m_currentRoom;
  private ToolType m_activeTool;

  protected override void OnInitializeSingleton() {
    m_gameState = GameState.GAME;
    m_currentRoom = RoomType.LIVING_ROOM;
    m_activeTool = ToolType.HAMMER;
    CandyCount = new();
    m_doorHealthSystem = doorObject.GetComponent<HealthSystem>();
    
    Debug.Log("GameManager initialized");
  }

  private void Start() {
    clockObject.timeProgress.AddListener(OnTimeProgressChange);
    clockObject.unwindingProgress.AddListener(OnClockUnwindingChange);
    m_doorHealthSystem.health.AddListener(OnDoorHealthChange);
  }

  protected override void OnDestroy() {
    if (clockObject) {
      clockObject.timeProgress.RemoveListener(OnTimeProgressChange);
      clockObject.unwindingProgress.RemoveListener(OnClockUnwindingChange);
    }
  }

  private void OnTimeProgressChange(float progress) {
    if (progress >= 1.0f) {
      Debug.Log("[END]: Time is out!");
      // TODO Add ApplicationManager data that will appear in win/lose
      
      // TODO win
      // TODO change state
      // TODO activate cutscene
    }
  }

  private void OnClockUnwindingChange(float progress) {
    // The clock is broken
    if (progress >= 1.0f) {
      Debug.Log("[END]: The clock is broken!");
      
      // TODO Add ApplicationManager data that will appear in win/lose
      // TODO lose
      // TODO change state
      // TODO activate cutscene
    }
  }

  private void OnDoorHealthChange(int health) {
    if (health <= 0) {
      Debug.Log("[END]: The door is broken!");
      
      // TODO Add ApplicationManager data that will appear in win/lose
      // TODO lose
      // TODO change state
      // TODO activate cutscene
    }
  }

  private void Update() {
    // No item changing if not in the game
    if (m_gameState != GameState.GAME) {
      return;
    }

    if (Input.GetKeyDown(KeyCode.Alpha1)) {
      m_activeTool = ToolType.HAMMER;
    }
    if (Input.GetKeyDown(KeyCode.Alpha2)) {
      m_activeTool = ToolType.LURE_SMALL;
    }
    if (Input.GetKeyDown(KeyCode.Alpha3)) {
      m_activeTool = ToolType.LURE_BIG;
    }
  }

  
  // === Scene management ===
  
  
  public Clock GetClock() {
    return clockObject;
  }

  public GameObject GetDoor() {
    return doorObject;
  }
  
  public HealthSystem GetDoorHealth() {
    return m_doorHealthSystem;
  }
  
  
  // === State management ===

  public GameState GetGameState() {
    return m_gameState;
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
    // TODO transition (?)
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
