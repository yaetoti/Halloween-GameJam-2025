using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : SceneSingleton<SpawnManager> {
  public List<GameObject> LureObjects { get; } = new();
  public List<GameObject> ZombieObjects { get; } = new();
  public List<GameObject> GhostObjects { get; } = new();

  public void RegisterLure(GameObject obj) {
    LureObjects.Add(obj);
  }
  
  public void RegisterZombie(GameObject obj) {
    ZombieObjects.Add(obj);
  }
  
  public void RegisterGhost(GameObject obj) {
    GhostObjects.Add(obj);
  }

  public void UnregisterLure(GameObject obj) {
    LureObjects.Remove(obj);
  }
  
  public void UnregisterZombie(GameObject obj) {
    ZombieObjects.Remove(obj);
  }
  
  public void UnregisterGhost(GameObject obj) {
    GhostObjects.Remove(obj);
  }
}