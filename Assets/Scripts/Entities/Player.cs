using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {
  // Params
  [SerializeField]
  private float moveSpeed;

  // State
  private Rigidbody2D m_rigidbody;

  [SerializeField]
  private SpriteRenderer m_sprite;

  [Space(10)]
  [SerializeField]
  private Sprite i1;
  [SerializeField]
  private Sprite i2;
  [SerializeField]
  private Sprite w1;
  [SerializeField]
  private Sprite w2;

  private bool m_state;

  private bool m_isMoving;

  public void Awake() {
    m_rigidbody = GetComponent<Rigidbody2D>();
    m_state = false;
  }

  private void Update()
  {
    // TODO
    // --- Tools ---
  }

    void Start()
    {
    StartCoroutine(SetSprite());
    }

    private void FixedUpdate()
  {
    // --- Movement ---

    var wishDir = GetWishDir(out bool isMoving);
    m_isMoving = isMoving;
    var damping = m_rigidbody.linearVelocity;
    var movementSpeed = wishDir * moveSpeed - damping;
    m_rigidbody.AddForce(movementSpeed, ForceMode2D.Impulse);
  }

  private IEnumerator SetSprite()
  {
    m_state = !m_state;
    if (m_state)
    {
      if (m_isMoving)
      {
        m_sprite.sprite = w1;
      }
      else
      {
        m_sprite.sprite = i1;
      }
    }
    else
    {
      if (m_isMoving)
      {
        m_sprite.sprite = w2;
      }
      else
      {
        m_sprite.sprite = i2;
      }   
    } 
      yield return new WaitForSeconds(.25f);
      StartCoroutine(SetSprite());
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
      m_sprite.flipX = false;
      isMoving = true;
    }

    if (Input.GetKey(KeyCode.A)) {
      wishDir.x -= 1;
      m_sprite.flipX = true;
      isMoving = true;
    }
    
    return wishDir.normalized;
  }
}
