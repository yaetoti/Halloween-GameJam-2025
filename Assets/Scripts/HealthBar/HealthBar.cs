using System;
using UnityEngine;

public class HealthBar : MonoBehaviour {
  private static readonly int HEALTH = Shader.PropertyToID("_Health");
  
  public Health health;
    
  private Material m_healthBarMat;

  private void Awake() {
    health.health.AddListener(OnHealthChanged);
    health.maxHealth.AddListener(OnMaxHealthChanged);
  }

  private void OnDestroy() {
    health.health.RemoveListener(OnHealthChanged);
    health.maxHealth.RemoveListener(OnMaxHealthChanged);
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
    float healthValue = Mathf.Clamp(health.health.Value, 0, health.maxHealth.Value);
    m_healthBarMat.SetFloat(HEALTH, healthValue / health.maxHealth.Value);
  }
}
