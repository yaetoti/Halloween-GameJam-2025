using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour {
  // public Spawn place: x, y1 y2 (random between): some marker
  // zombie prefab
  // enter marker: transform
  
  // StartSpawnTime
  // FinishSpawnTime

  [SerializeField] private Transform markerSpawnXY1;
  [SerializeField] private Transform markerSpawnY2;
  
  [SerializeField] private GameObject zombiePrefab;
  [SerializeField] private Transform enterMarker;
    
  [SerializeField] private float startSpawnTimeSeconds = 2.0f;
  [SerializeField] private float finishSpawnTimeSeconds = 0.3f;

  [SerializeField] private AnimationCurve spawnDelayCurve;
  
  // State
  private float m_spawnProgress = 0.0f;

  private void Update() {
    if (GameManager.Instance.GetGameState() != GameState.GAME) {
      return;
    }
    
    // Increase spawn progress
    float spawnDelay = Mathf.Lerp(startSpawnTimeSeconds, finishSpawnTimeSeconds, spawnDelayCurve.Evaluate(GameManager.Instance.GetClock().TimeProgress));
    m_spawnProgress += Time.deltaTime / spawnDelay;
    
    // Perform spawn
    if (m_spawnProgress >= 1.0f) {
      m_spawnProgress = 0.0f;
      Spawn();
    }
  }
  
  private void Spawn() {
    var spawnPosition = GetRandomSpawnPosition();
    var zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
    var zombieMemory = zombie.GetComponent<ZombieMemoryPlay>();
    zombieMemory.enterMarkerX = enterMarker;
  }
  
  private Vector3 GetRandomSpawnPosition() {
    float randomY = Random.Range(markerSpawnXY1.position.y, markerSpawnY2.position.y);
    return new Vector3(markerSpawnXY1.position.x, randomY, markerSpawnXY1.position.z);
  }
}

