using System;
using UnityEngine;

public class TestZombieAI : MonoBehaviour {
  public Transform targetTransform;
  public HealthSystem targetHealth;
  
  private Zombie m_zombie;

  private void Awake() {
    m_zombie = GetComponent<Zombie>();
  }

  private void Start() {
    m_zombie.SetTarget(targetTransform?.position, targetHealth);
  }
}