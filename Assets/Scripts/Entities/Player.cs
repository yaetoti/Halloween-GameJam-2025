using System;
using UnityEngine;

public class Player : MonoBehaviour {
  // Params
  public float moveSpeed;
  
  // State
  private Rigidbody2D m_rigidbody;

  public void Awake() {
    m_rigidbody = GetComponent<Rigidbody2D>();
  }

  private void Update() {

  }

  private void FixedUpdate() {
    var wishDir = GetWishDir(out bool isMoving);
    var damping = m_rigidbody.linearVelocity;
    var movementSpeed = wishDir * moveSpeed - damping;
    m_rigidbody.AddForce(movementSpeed, ForceMode2D.Impulse);
  }

  private Vector2 GetWishDir(out bool isMoving) {
    Vector2 wishDir = new ();
    isMoving = false;
    
    if (Input.GetKey(KeyCode.W)) {
      wishDir.y += 1;
      isMoving = true;
    }

    if (Input.GetKey(KeyCode.S)) {
      wishDir.y -= 1;
      isMoving = true;
    }
    
    if (Input.GetKey(KeyCode.D)) {
      wishDir.x += 1;
      isMoving = true;
    }

    if (Input.GetKey(KeyCode.A)) {
      wishDir.x -= 1;
      isMoving = true;
    }
    
    return wishDir.normalized;
  }
}
