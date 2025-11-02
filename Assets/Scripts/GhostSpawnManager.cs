using System.Collections.Generic;
using UnityEngine;

public class GhostSpawnManager : SceneSingleton<GhostSpawnManager> {
  public List<Transform> spawnPositions;
  public GameObject ghostPrefab;
  
  public void SpawnGhost() {
    var ghostInstance = Instantiate(ghostPrefab, GetRandomPosition(), Quaternion.identity);
    // TODO i pohuy? i pohuy.
  }
  
  public Vector3 GetRandomPosition() {
    if (spawnPositions.Count == 0) {
      Debug.LogError("You need to add transforms to GhostSpawnManager", this);
      return Vector3.zero;
    }

    return spawnPositions[ Random.Range(0, spawnPositions.Count) ].position;
  }
}