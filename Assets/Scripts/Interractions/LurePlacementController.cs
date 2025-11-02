using System;
using UnityEngine;

public class LurePlacementController : MonoBehaviour {
  public static readonly int SMALL_LURE_PRICE = 5;
  public static readonly int BIG_LURE_PRICE = 10;
  
  public Camera mainCamera;
  public Collider2D placementCollider;
  public GameObject smallLurePrefab;
  public GameObject bigLurePrefab;

  private int m_lureCounter = 0;

  private void Start() {
    // TODO bedug
    //GameManager.Instance.ToggleRoom();
  }

  private void Update() {
    // (?) TODO check that new object collider does not overlap other lures
    // TODO OOOOOOOOOOOH FUCK, WE CAN DO RED PREVIEW. Just a sprite. Just one, BABEEEEE, PWEEEEEASE
    
    // Get Lure collider size. What is a prefab? GameObject? Does it have values or not? maybe static?
    
    HandlePlacement();
  }

  public void HandlePlacement() {
    // TODO money
    
    if (ApplicationManager.Instance.GetAppState() != AppState.GAME
      || GameManager.Instance.GetGameState() != GameState.GAME
      || GameManager.Instance.GetCurrentRoom() != RoomType.GARAGE
      || !Input.GetKeyDown(KeyCode.Mouse0)) {
      return;
    }
    
    Vector3 mousePosWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    mousePosWorld.z = 0;
    bool isMouseOverCollider = placementCollider.OverlapPoint(mousePosWorld);
    if (!isMouseOverCollider) {
      return;
    }
    
    GameObject prefab = null;
    
    if (GameManager.Instance.GetActiveTool() == ToolType.LURE_SMALL) {
      prefab = smallLurePrefab;
      if (GameManager.Instance.CandyCount.Value < SMALL_LURE_PRICE) {
        return;
      }

      GameManager.Instance.CandyCount.Value -= SMALL_LURE_PRICE;
    }
    
    if (GameManager.Instance.GetActiveTool() == ToolType.LURE_BIG) {
      prefab = bigLurePrefab;
      
      if (GameManager.Instance.CandyCount.Value < BIG_LURE_PRICE) {
        return;
      }

      GameManager.Instance.CandyCount.Value -= BIG_LURE_PRICE;
    }

    if (prefab is not null) {
      prefab.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = m_lureCounter;
      var lureObject = Instantiate(prefab, mousePosWorld, Quaternion.identity);
      //EntityManager.Instance.RegisterLure(lureObject);
      m_lureCounter++;
    }
  }
}