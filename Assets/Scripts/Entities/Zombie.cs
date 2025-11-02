using System;
using UnityEngine;

public class Zombie : MonoBehaviour {
  public static readonly string STRATEGY_ENTER = "enter";
  public static readonly string STRATEGY_NAVIGATE = "navigate";
  
  // What data?
  // - Get target location
  
  // How to get door location? AHH, we need a gamemanager
  // How to get lure location?
  
  // Params
  public Transform enterPoint;
  
  // State
  private Brain m_brain = new();
  private HealthSystem m_healthSystem;

  private void Awake() {
    m_healthSystem = GetComponent<HealthSystem>();
    m_healthSystem.health.AddListener(OnHealthChange);

    //m_brain.AddStrategy(STRATEGY_ENTER, );
    
    // Enter (move horizontally until in the room)
    // Navigate (look for candies - look for the door - stay. go - break)
  }

  private void OnDestroy() {
    m_healthSystem.health.RemoveListener(OnHealthChange);
  }

  private void Start() {
    
  }

  // Update is called once per frame
  private void Update() {
  }

  private void OnHealthChange(int health) {
    // DIE MOTHERFUCKER DIE MOTHERFUCKER DIEEEEEE
    if (health <= 0) {
      Destroy(gameObject);
    }
  }
}