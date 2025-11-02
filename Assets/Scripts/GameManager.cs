using System;
using System.Collections.Generic;
using UnityEngine.UI;
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

  [Space(10)]

  [SerializeField]
  private Image bat;

  [SerializeField]
  private Image smallLure;

  [SerializeField]
  private Image bigLure;

  [Space(10)]

  [SerializeField]
  private Image uiClock;

  [Space(10)]
  private TMPro.TextMeshProUGUI candyCount;

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
    CandyCount.AddListener(SetCandyCountText);
  }

  private void SetCandyCountText(int count)
  {
    candyCount.text = count.ToString();
  }

  protected override void OnDestroy() {
    if (clockObject) {
      clockObject.timeProgress.RemoveListener(OnTimeProgressChange);
      clockObject.unwindingProgress.RemoveListener(OnClockUnwindingChange);
    }
  }

  private void OnTimeProgressChange(float progress) {

    uiClock.material.SetFloat("_TimeTillEnd", progress);

    Shader.SetGlobalFloat("_LightLerp", progress);

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

  private void Update()
  {
    // No item changing if not in the game
    if (m_gameState != GameState.GAME)
    {
      return;
    }

    if (Input.GetKeyDown(KeyCode.Alpha1))
    {
      SetActiveTool(ToolType.HAMMER);
    }
    if (Input.GetKeyDown(KeyCode.Alpha2))
    {
      SetActiveTool(ToolType.LURE_SMALL);
    }
    if (Input.GetKeyDown(KeyCode.Alpha3))
    {
      SetActiveTool(ToolType.LURE_BIG);
    }
  }

  private void SetActiveUIButton(Image image, float state = 1)
  {
    image.material.SetFloat("_IsChecked", state);
  }

  private void ResetActiveUIButton()
  {
    bat.material.SetFloat("_IsChecked", 0);
    smallLure.material.SetFloat("_IsChecked", 0);
    bigLure.material.SetFloat("_IsChecked", 0);
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

  public ToolType GetActiveTool()
  {
    return m_activeTool;
  }

  public void SetActiveTool(string type)
    {
      switch (type)
      {
        case "HAMMER":
          SetActiveTool(ToolType.HAMMER);
          break;
        case "LURE_SMALL":
          SetActiveTool(ToolType.LURE_SMALL);
          break;
        case "LURE_BIG":
          SetActiveTool(ToolType.LURE_BIG);
          break;
      }
    }

  public void SetActiveTool(ToolType type) {
    m_activeTool = type;
    ResetActiveUIButton();
    switch (type)
    {
      case ToolType.HAMMER:
        SetActiveUIButton(bat);
        break;
      case ToolType.LURE_SMALL:
        SetActiveUIButton(smallLure);
        break;
      case ToolType.LURE_BIG:
        SetActiveUIButton(bigLure);
        break;    
    }
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
