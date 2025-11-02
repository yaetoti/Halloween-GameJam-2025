using System;
using UnityEngine;

public class EntityHitController : MonoBehaviour {
  public Camera mainCamera;
  
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
    Debug.Log($"Colliders: {hits.Length}");
    
    foreach (var hit in hits) {
      if (!hit.collider) {
        continue;
      }

      Zombie zombieComponent = hit.collider.GetComponent<Zombie>();
      if (zombieComponent && GameManager.Instance.GetCurrentRoom() == RoomType.GARAGE) {
        Health healthComponent = hit.collider.GetComponent<Health>();
        healthComponent.health.Value -= 1;
        continue;
      }
      
      Ghost ghostComponent = hit.collider.GetComponent<Ghost>();
      if (ghostComponent && GameManager.Instance.GetCurrentRoom() == RoomType.LIVING_ROOM) {
        Health healthComponent = hit.collider.GetComponent<Health>();
        healthComponent.health.Value -= 1;
        
        // TODO NOT HERE, WE CAN'T KNOW IF WE KILLED HIM. WELL, WE CAN, BUT IN A FUCKED UP WAY OF JUST CHECKING HEALTH ANS THAT IS FUCKING STUPID
        
        // Give candy =)
        GameManager.Instance.CandyCount.Value += 1;
      }
    }
  }
}