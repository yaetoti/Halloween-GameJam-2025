using System;
using UnityEngine;

public class HealthBar : MonoBehaviour {
  private static readonly int HEALTH = Shader.PropertyToID("_Health");
  
  public HealthSystem healthSystem;
    
  private Material m_healthBarMat;

  private void Awake() {
    healthSystem.health.AddListener(OnHealthChanged);
    healthSystem.maxHealth.AddListener(OnMaxHealthChanged);
  }

  private void OnDestroy() {
    healthSystem.health.RemoveListener(OnHealthChanged);
    healthSystem.maxHealth.RemoveListener(OnMaxHealthChanged);
  }

  private void OnEnable() {
    m_healthBarMat = transform.GetChild(0).GetComponent<SpriteRenderer>().material;
  }

  private void OnHealthChanged(int health) {
    UpdateBar();
  }
  
  private void OnMaxHealthChanged(int maxHealth) {
    UpdateBar();
  }
  
  private void UpdateBar() {
    float healthValue = Mathf.Clamp(healthSystem.health.Value, 0, healthSystem.maxHealth.Value);
    m_healthBarMat.SetFloat(HEALTH, healthValue / healthSystem.maxHealth.Value);
  }
}
