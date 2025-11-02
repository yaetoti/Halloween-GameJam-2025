using System;
using System.Collections.Generic;
using UnityEngine;

public class TestZombieAI : MonoBehaviour {
  public List<Transform> lures; 
  
  public Transform targetTransform;
  public HealthSystem targetHealth;
  
  private Zombie m_zombie;

  private void Awake() {
    m_zombie = GetComponent<Zombie>();
  }

  private void Start() {
    //m_zombie.SetTarget(targetTransform?.position, targetHealth);
    
    // Add test lures
    foreach (var lure in lures) {
      //SpawnManager.Instance.RegisterLure(lure.gameObject);
    }
  }
}