using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingHandler : MonoBehaviour {
  public void ReturnToMenu() {
    SceneManager.LoadScene("MainMenu");
  }
}