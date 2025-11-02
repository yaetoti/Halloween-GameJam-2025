using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityHitController : MonoBehaviour {
  public Camera mainCamera;

  public List<AudioSource> bonks;

  public AudioSource ghost;
  
  private void Update() {
    // Debug.Log($"AppState: {ApplicationManager.Instance.GetAppState()}");
    // Debug.Log($"GameState: {GameManager.Instance.GetGameState()}");
    // Debug.Log($"ActiveTool: {GameManager.Instance.GetActiveTool()}");
    // Debug.Log($"CurrentRoom: {GameManager.Instance.GetCurrentRoom()}");
    
    // In game - in actual game - use hammer - in garage - click LMB
    if (ApplicationManager.Instance.GetAppState() != AppState.GAME
        || GameManager.Instance.GetGameState() != GameState.GAME
        || GameManager.Instance.GetActiveTool() != ToolType.HAMMER
        || !Input.GetKeyDown(KeyCode.Mouse0)) {
      return;
    }

    Vector2 mouseWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    RaycastHit2D[] hits = Physics2D.RaycastAll(mouseWorld, Vector2.zero);
    //Debug.Log($"Colliders: {hits.Length}");
    
    foreach (var hit in hits) {
      if (!hit.collider)
      {
        continue;
      }

      Zombie zombieComponent = hit.collider.GetComponent<Zombie>();
      if (zombieComponent) {
        HealthSystem healthSystemComponent = hit.collider.GetComponent<HealthSystem>();
        healthSystemComponent.health.Value -= 1;
        
        int index = UnityEngine.Random.Range(0, 3);
        bonks[index].Play();

        // GIVE US A SIGN
        if (healthSystemComponent.Health <= 0) {
          GhostSpawnManager.Instance.SpawnGhost();
        }
        
        break;
      }
      
      Ghost ghostComponent = hit.collider.GetComponent<Ghost>();
      if (ghostComponent) {
        HealthSystem healthSystemComponent = hit.collider.GetComponent<HealthSystem>();
        healthSystemComponent.health.Value -= 1;

        ghost.Play();

        if (healthSystemComponent.Health <= 0) {
          // Give a candy =)
          GameManager.Instance.CandyCount.Value += 1;
        }
        
        break;
      }
    }
  }
}