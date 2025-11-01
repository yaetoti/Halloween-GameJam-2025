using System;
using UnityEngine;

[Serializable]
public class ObservableValue<T> {
  // Properties
  public T Value {
    get => value;
    set {
      this.value = value;
      Notify();
    }
  }

  // Events
  public event Action<T> OnValueChanged;

  // State
  [SerializeField]
  private T value;

  public ObservableValue() {
  }

  public ObservableValue(T value) {
    // TODO no listeners here, it's a constructor, am I right?
    //Value = value;
    this.value = value;
  }

  public void Notify() {
    OnValueChanged?.Invoke(value);
  }

  public void AddListener(Action<T> listener) {
    OnValueChanged += listener;
  }

  public void RemoveListener(Action<T> listener) {
    OnValueChanged -= listener;
  }
}