using System;
using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
using UnityEditor;

public class Clock : MonoBehaviour {
  
  [CustomEditor(typeof(Clock))]
  public class ClockEditor : Editor {
    public override void OnInspectorGUI() {
      base.OnInspectorGUI();
        
      Clock clock = (Clock)target;
      EditorGUI.BeginChangeCheck();
      float newProgress = EditorGUILayout.Slider("Time Progress", clock.TimeProgress.Value, 0f, 1f);
      if (EditorGUI.EndChangeCheck()) {
        clock.TimeProgress.Value = newProgress;
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
  public ObservableValue<float> UnwindingProgress { get; private set; } = new();

  // [0-1] Game time progress. From 2:00 to 5:00
  public ObservableValue<float> TimeProgress { get; private set; } = new();

  public ObservableValue<ClockState> CurrentClockState { get; private set; } = new(ClockState.UNWINDING);

  private void Awake() {
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
    TimeProgress.Value += Time.deltaTime / gameTimeSeconds;
    
    // --- Update winding ---
    // TODO Here we should limit motions
    // TODO maybe clicking would be better?
    // TODO how to track state change?
    
    Vector2 mouseWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    float progress = UnwindingProgress.Value;
    
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
    
    UnwindingProgress.Value = progress;
    
    // === Set arrows position ===
    var clockData = GetClockData();
    hourArrowTransform.rotation = Quaternion.Euler(0.0f, 0.0f, clockData.hoursRotationZ);
    minuteArrowTransform.rotation = Quaternion.Euler(0.0f, 0.0f, clockData.minutesRotationZ);
  }
  
  public ClockData GetClockData() {
    int duration = endTimeMinutes - startTimeMinutes;
    int currentTime = startTimeMinutes + (int)(duration * TimeProgress.Value);
    int hours = currentTime / 60;
    int displayedHours = hours % 12;
    int minutes = currentTime - hours * 60;

    // Clockwise - negative Z
    float hoursRotationZ = displayedHours / 12.0f * -360.0f;
    float minutesRotationZ = minutes / 60.0f * -360.0f;
    
    return new() {
      hours = displayedHours,
      minutes = minutes,
      hoursRotationZ = hoursRotationZ,
      minutesRotationZ = minutesRotationZ
    };
  }
}