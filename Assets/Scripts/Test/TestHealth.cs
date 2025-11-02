using System;
using UnityEngine;

public class TestHealth : MonoBehaviour {
  public HealthSystem healthSystem;
  public float timer;

  private void OnEnable() {
    healthSystem.health.AddListener(OnHealthChanged);
  }

  private void OnDisable() {
    healthSystem.health.RemoveListener(OnHealthChanged);
  }
  
  private void OnHealthChanged(int value) {
    if (value == 0) {
      Debug.Log("Patient is dead!");
    }
    else {
      Debug.Log($"New health: {value}");
    }
  }

  private void Update() {
    timer += Time.deltaTime;
    if (timer >= 2.0f) {
      Debug.Log("HIT!");
      
      timer -= 2.0f;
      healthSystem.Health -= 2;
    }
  }
}
