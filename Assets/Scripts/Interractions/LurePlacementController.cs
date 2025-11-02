using System;
using UnityEngine;

public class LurePlacementController : MonoBehaviour {
  public Camera mainCamera;
  public Collider2D placementCollider;
  public GameObject smallLurePrefab;
  public GameObject bigLurePrefab;

  private int m_lureCounter = 0;

  private void Start() {
    // TODO bedug
    GameManager.Instance.ToggleRoom();
  }

  private void Update() {
    // TODO check room
    // TODO check that mouse overlaps trigger
    // TODO spawn a lure. register in a spawn manager
    
    // (?) TODO check that new object collider does not overlap other lures
    
    // TODO OOOOOOOOOOOH FUCK, WE CAN DO RED PREVIEW. Just a sprite. Just one, BABEEEEE, PWEEEEEASE
    
    // Get Lure collider size. What is a prefab? GameObject? Does it have values or not? maybe static?
    
    
    HandlePlacement();
  }

  public void HandlePlacement() {
    if (GameManager.Instance.GetCurrentRoom() != RoomType.GARAGE || !Input.GetKeyDown(KeyCode.Mouse0)) {
      return;
    }
    
    Vector3 mousePosWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    mousePosWorld.z = 0;
    bool isMouseOverCollider = placementCollider.OverlapPoint(mousePosWorld);
    if (!isMouseOverCollider) {
      return;
    }

    // TODO MANI
    GameObject prefab = null;
    
    if (GameManager.Instance.GetActiveTool() == ToolType.LURE_SMALL) {
      prefab = smallLurePrefab;
    }
    
    if (GameManager.Instance.GetActiveTool() == ToolType.LURE_BIG) {
      prefab = bigLurePrefab;
    }

    if (prefab is not null) {
      prefab.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = m_lureCounter;
      Instantiate(prefab, mousePosWorld, Quaternion.identity);
      m_lureCounter++;
    }
  }
}