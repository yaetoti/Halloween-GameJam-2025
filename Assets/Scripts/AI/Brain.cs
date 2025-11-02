using System;
using System.Collections.Generic;
using UnityEngine;

// I HEV NO BREIN LEFT. I VIL MEIK A NEV ONE
public class Brain : MonoBehaviour {
  [SerializeField]
  private List<BrainStrategyEntry> m_initialStrategies;
  // TODO porne to typos
  [SerializeField]
  private string m_defaultStrategyName;
  
  private Dictionary<string, BrainStrategy> m_strategies = new();
  private BrainStrategy m_activeStrategy;

  private void Awake() {
    foreach (var entry in m_initialStrategies) {
      AddStrategy(entry.name, entry.strategy);
    }

    if (!string.IsNullOrEmpty(m_defaultStrategyName)) {
      SetStrategy(m_defaultStrategyName);
    }
  }

  public void AddStrategy(string name, BrainStrategy strategy) {
    if (m_strategies.ContainsKey(name)) {
      Debug.LogError($"Strategy '{name}' already exists", this);
      return;
    }

    var strategyInstance = Instantiate(strategy);
    m_strategies.Add(name, strategyInstance);
    strategyInstance.Initialize(gameObject, this);
  }

  public void SetStrategy(string name) {
    if (!m_strategies.ContainsKey(name)) {
      Debug.LogError($"Strategy '{name}' not found", this);
      return;
    }
    
    m_activeStrategy?.OnStop();
    m_activeStrategy = m_strategies[name];
    m_activeStrategy?.OnStart();
  }

  public void Update() {
    m_activeStrategy?.OnUpdate();
  }
  
  public void FixedUpdate() {
    m_activeStrategy?.OnFixedUpdate();
  }

  public void OnDestroy() {
    m_activeStrategy?.OnStop();
    m_activeStrategy = null;

    foreach (var strategy in m_strategies.Values) {
      Destroy(strategy);
    }
  }
}