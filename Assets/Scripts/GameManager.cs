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
  public float initialDoorHealth; // TODO door is controlling it
  public float gameLengthSeconds; // TODO clock is controlling it

  [SerializeField]
  private Clock clockObject;
  [SerializeField]
  private GameObject doorObject;

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

  protected override void OnInitializeSingleton() {
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

    if (Input.GetKeyDown(KeyCode.Alpha1)) {
      m_activeTool = ToolType.HAMMER;
    }
    if (Input.GetKeyDown(KeyCode.Alpha2)) {
      m_activeTool = ToolType.LURE_SMALL;
    }
    if (Input.GetKeyDown(KeyCode.Alpha3)) {
      m_activeTool = ToolType.LURE_BIG;
    }

    Debug.Log("Active tool: " + m_activeTool);
    
    // TODO REMOVE
    
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
