using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Clock : MonoBehaviour {
  // Params
  public Camera mainCamera;
  public Collider2D clockCollider;
  // Unit per seconds (0-1)
  public float secondsToWind;
  public float secondsToUnwind;
  
  // State
  private ClockState m_clockState;
  // [0-1]
  private float m_unwindingProgress;
  // [0-1] Game time progress. From 2:00 to 3:00
  private float m_timeProgress;
  
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
  }
}