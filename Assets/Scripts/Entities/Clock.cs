using System;
using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(Clock))]
public class ClockEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        
        Clock clock = (Clock)target;
        EditorGUI.BeginChangeCheck();
        float newProgress = EditorGUILayout.Slider("Time Progress", clock.m_timeProgress, 0f, 1f);
        if (EditorGUI.EndChangeCheck()) {
            clock.m_timeProgress = newProgress;
        }
    }
}
#endif

public class Clock : MonoBehaviour {
  public class ClockData {
    public int hours;
    public int minutes;
    public float hoursRotationZ;
    public float minutesRotationZ;
  }
  
  // Params
  public Camera mainCamera;
  public Collider2D clockCollider;
  public Transform hourArrowTransform;
  public Transform minuteArrowTransform;
  // Unit per seconds (0-1)
  public float secondsToWind;
  public float secondsToUnwind;
  public int startTimeMinutes;
  public int endTimeMinutes;
  
  // State
  private ClockState m_clockState;
  // [0-1]
  private float m_unwindingProgress;
  // [0-1] Game time progress. From 2:00 to 3:00
  [SerializeField]
  // TODO private
  public float m_timeProgress;
  
  // [0-1] -> 2:30 3:40

  public void Awake() {
    m_clockState = ClockState.UNWINDING;
  }

  public void Update() {
    // Debug.Log($"Clock Unwinding Progress: {m_unwindingProgress}");
    // Debug.Log($"Mouse position: {Input.mousePosition}");
    // if (clockCollider.OverlapPoint(mainCamera.ScreenToWorldPoint(Input.mousePosition))) {
    //   Debug.Log("Mouse is over clock");
    // }
    
    // === Update state ===
    // Just in case
    if (m_clockState == ClockState.BROKEN) {
      return;
    }

    // TODO Here we should limit motions
    // TODO maybe clicking would be better?
    // TODO how to track state change?
    
    // If mouse inside collider and button pressed
    Vector2 mouseWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    if (Input.GetKey(KeyCode.Mouse0) && clockCollider.OverlapPoint(mouseWorld)) {
      m_clockState = ClockState.WINDING;
      //m_unwindingProgress -= windingSpeed * Time.deltaTime;
      m_unwindingProgress -= Time.deltaTime / secondsToWind;
      if (m_unwindingProgress <= 0.0f) {
        m_unwindingProgress = 0.0f;
      }
    }
    else {
      m_clockState = ClockState.UNWINDING;
      //m_unwindingProgress += unwindingSpeed * Time.deltaTime;
      m_unwindingProgress += Time.deltaTime / secondsToUnwind;
      if (m_unwindingProgress >= 1.0f) {
        m_unwindingProgress = 1.0f;
        m_clockState = ClockState.BROKEN;
        
        // TODO broken
        // TODO call gamemanager
        // TODO call your mommy
        Debug.Log("This clock is ffocking broken!");
      }
    }
    
    // === Set arrows position ===
    var clockData = GetClockData();
    hourArrowTransform.rotation = Quaternion.Euler(0.0f, 0.0f, clockData.hoursRotationZ);
    minuteArrowTransform.rotation = Quaternion.Euler(0.0f, 0.0f, clockData.minutesRotationZ);
  }

  public ClockData GetClockData() {
    int duration = endTimeMinutes - startTimeMinutes;
    int currentTime = startTimeMinutes + (int)(duration * m_timeProgress);
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