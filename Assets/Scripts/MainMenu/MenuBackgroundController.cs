using System.Collections.Generic;
using UnityEngine;

public class MenuBackgroundController : MonoBehaviour {
  // Params
  public GameObject lurePrefab;
  public List<Transform> lurePositions;
  
  // State
  private GameObject m_currentLure;
  private int m_lastElementIndex = -1;
  
  private void Start() {
    SpawnLure();
    Shader.SetGlobalFloat("_LightLerp", 1.0f);
  }

  private void Update() {
  }

  private void OnLureHealthChanged(int health) {
    if (health == 0) {
      m_currentLure.transform.GetChild(0).GetComponent<HealthSystem>().health.RemoveListener(OnLureHealthChanged);
      SpawnLure();
    }
  }
  
  public void SpawnLure() {
    int index = GetUniqueId();
    m_currentLure = Instantiate(lurePrefab, lurePositions[index].position, Quaternion.identity);
    m_currentLure.transform.GetChild(0).GetComponent<HealthSystem>().health.AddListener(OnLureHealthChanged);
  }

  public int GetUniqueId() {
    int newIndex;
    do {
      newIndex = Random.Range(0, lurePositions.Count);
    } while (newIndex == m_lastElementIndex);
    
    m_lastElementIndex = newIndex;
    return newIndex;
  }
}