using System;
using UnityEngine;

public class TestObservableValue : MonoBehaviour {
  public ObservableValue<int> value = new();
  private float time = 0.0f;
  
  private void Awake() {
    value.AddListener(OnValueChanged);
  }

  private void Update() {
    time += Time.deltaTime;
    if (time >= 1.0f) {
      time -= 1.0f;
      value.Value += 1;
    }
  }

  private void OnValueChanged(int value) {
    Debug.Log($"Value changed: {value}");
  }
}
