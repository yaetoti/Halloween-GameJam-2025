using System;
using UnityEngine;

public class TestHealth : MonoBehaviour {
  public Health health;
  public float timer;

  private void OnEnable() {
    health.health.AddListener(OnHealthChanged);
  }

  private void OnDisable() {
    health.health.RemoveListener(OnHealthChanged);
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
      health.health.Value -= 2;
    }
  }
}
