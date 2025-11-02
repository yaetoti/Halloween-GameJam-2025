using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;

public class Clock : MonoBehaviour {
#if UNITY_EDITOR

  [CustomEditor(typeof(Clock))]
  public class ClockEditor : Editor {
    public override void OnInspectorGUI() {
      base.OnInspectorGUI();
        
      Clock clock = (Clock)target;
      EditorGUI.BeginChangeCheck();
      float newProgress = EditorGUILayout.Slider("Time Progress", clock.TimeProgress, 0f, 1f);
      if (EditorGUI.EndChangeCheck()) {
        clock.TimeProgress = newProgress;
      }
    }
  }
#endif
  
  public class ClockData {
    public int hours;
    public int minutes;
    public float hoursRotationZ;
    public float minutesRotationZ;
  }
  
  // Params
  [SerializeField]
  private Camera mainCamera;
  [SerializeField]
  private Collider2D clockCollider;
  [SerializeField]
  private Transform hourArrowTransform;
  [SerializeField]
  private Transform minuteArrowTransform;
  // Unit per seconds (0-1)
  [SerializeField]
  private float secondsToWind;
  [SerializeField]
  private float secondsToUnwind;
  [SerializeField]
  private int startTimeMinutes;
  [SerializeField]
  private int endTimeMinutes;
  [SerializeField]
  private float gameTimeSeconds;
  
  // State
  // [0-1]
  public ObservableValue<float> unwindingProgress = new();
  // [0-1] Game time progress. From 2:00 to 5:00
  public ObservableValue<float> timeProgress = new();

  [Space(10)]

  [SerializeField]
  private SpriteRenderer sprite;
  [Space(10)]

  [SerializeField]
  private Sprite fullClock;
  [SerializeField]
  private Sprite damagedClock;
  [SerializeField]
  private Sprite destroyedClock;

  [Space(10)]
  [SerializeField]
  private Animator m_anim;
  

  public float UnwindingProgress {
    get => unwindingProgress.Value;
    private set {
      float newValue = Mathf.Clamp01(value);
      // ReSharper disable once CompareOfFloatsByEqualityOperator
      if (newValue != unwindingProgress.Value) {
        unwindingProgress.Value = newValue;
      }
    }
  }

  public float TimeProgress {
    get => timeProgress.Value;
    private set {
      float newValue = Mathf.Clamp01(value);
      // ReSharper disable once CompareOfFloatsByEqualityOperator
      if (newValue != timeProgress.Value) {
        timeProgress.Value = newValue;
      }
    }
  }

  public ObservableValue<ClockState> CurrentClockState { get; private set; } = new(ClockState.UNWINDING);

  private void Awake() {
    m_anim = GetComponent<Animator>();
  }

  public void Update() {
    // Debug.Log($"Clock Unwinding Progress: {m_unwindingProgress}");
    // Debug.Log($"Mouse position: {Input.mousePosition}");
    // if (clockCollider.OverlapPoint(mainCamera.ScreenToWorldPoint(Input.mousePosition))) {
    //   Debug.Log("Mouse is over clock");
    // }
    
    // === Update state ===
    // Just in case
    // if (CurrentClockState.Value == ClockState.BROKEN) {
    //   return;
    // }

    // --- Update time ---
    TimeProgress += Time.deltaTime / gameTimeSeconds;
    
    // --- Update winding ---
    // TODO Here we should limit motions
    // TODO maybe clicking would be better?
    // TODO how to track state change?
    
    Vector2 mouseWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    float progress = UnwindingProgress;
    
    // If the cursor is inside the collider and LMB is pressed - wind up
    if (Input.GetKey(KeyCode.Mouse0) && clockCollider.OverlapPoint(mouseWorld)) {
      CurrentClockState.Value = ClockState.WINDING;
      progress -= Time.deltaTime / secondsToWind;

      if (progress <= 0.0f) {
        progress = 0.0f;
      }
    }
    // Otherwise - unwind
    else {
      CurrentClockState.Value = ClockState.UNWINDING;
      progress += Time.deltaTime / secondsToUnwind;
      
      if (progress >= 1.0f) {
        progress = 1.0f;
        CurrentClockState.Value = ClockState.BROKEN;
      }
    }
    
    UnwindingProgress = progress;
    
    // === Set arrows position ===
    var clockData = GetClockData();
    hourArrowTransform.rotation = Quaternion.Euler(0.0f, 0.0f, clockData.hoursRotationZ);
    minuteArrowTransform.rotation = Quaternion.Euler(0.0f, 0.0f, clockData.minutesRotationZ);
  }

  public ClockData GetClockData()
  {
    int duration = endTimeMinutes - startTimeMinutes;
    int currentTime = startTimeMinutes + (int)(duration * TimeProgress);
    int hours = currentTime / 60;
    int displayedHours = hours % 12;
    int minutes = currentTime - hours * 60;

    // Clockwise - negative Z
    float hoursRotationZ = displayedHours / 12.0f * -360.0f;
    float minutesRotationZ = minutes / 60.0f * -360.0f;

    return new()
    {
      hours = displayedHours,
      minutes = minutes,
      hoursRotationZ = hoursRotationZ,
      minutesRotationZ = minutesRotationZ
    };
  }

    void FixedUpdate()
  {
    m_anim.speed = UnwindingProgress * 2;
    switch (UnwindingProgress)
    {
      case > .75f:
        sprite.sprite = destroyedClock;
        break;
      case > .4f:
        sprite.sprite = damagedClock;
        break;
      case > 0:
        sprite.sprite = fullClock;
        break;
      }
    }
}