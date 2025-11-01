using System;

[Serializable]
public class ObservableValue<T> {
  // Properties
  public T Value {
    get => m_value;
    set {
      m_value = value;
      Notify();
    }
  }

  // Events
  private event Action<T> OnValueChanged;

  // State
  private T m_value;

  public ObservableValue() {
  }

  public ObservableValue(T value) {
    // TODO no listeners here, it's a constructor, am I right?
    //Value = value;
    m_value = value;
  }

  public void Notify() {
    OnValueChanged?.Invoke(m_value);
  }

  public void AddListener(Action<T> listener) {
    OnValueChanged += listener;
  }

  public void RemoveListener(Action<T> listener) {
    OnValueChanged -= listener;
  }
}