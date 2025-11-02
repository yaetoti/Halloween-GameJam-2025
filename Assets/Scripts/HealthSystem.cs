using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour {
  public ObservableValue<int> health = new(1);
  public ObservableValue<int> maxHealth = new(1);

  public int Health {
    get => health.Value;
    set {
      var newValue = Math.Clamp(value, 0, MaxHealth);
      if (newValue != health.Value) {
        health.Value = newValue;
      }
    }
  }

  public int MaxHealth {
    get => maxHealth.Value;
    set {
      if (value != maxHealth.Value) {
        maxHealth.Value = value;
      }
    }
  }
}