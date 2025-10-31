using UnityEngine;

public class GameManagerTest : MonoBehaviour {
  void Start() {
    Debug.Log($"Active tool is: {GameManager.Instance.GetActiveTool()}");
  }

  void Update() {
    
  }
}
