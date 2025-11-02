using System;
using System.Collections;
using UnityEngine;

public class CandyLure : MonoBehaviour
{
    public SpriteRenderer LureSprite;

    private HealthSystem m_healthSystem;
    //private float m_maxHealth;
    private int m_spriteIndex;

    public Sprite SpriteA;
    public Sprite SpriteB;
    public Sprite AlmostBroken;
    public Sprite Broken;

    private void Awake() {
      m_healthSystem = GetComponent<HealthSystem>();
      m_healthSystem.health.AddListener(OnHealthChanged);
    }
    
    private void Start() {
      EntityManager.Instance.RegisterLure(transform.parent.gameObject);
      StartCoroutine(SetSpriteIndex());
    }

    private void OnDestroy() {
      m_healthSystem?.health.RemoveListener(OnHealthChanged);
      EntityManager.Instance?.UnregisterLure(transform.parent.gameObject);
    }

    public void Damage() {
      m_healthSystem.Health--;
    }

    private void Update() {
    }

    private void OnHealthChanged(int health) {
      if (health == 0) {
        // DIE MOTHERFUCKER DIE MOTHERFUCKER DIEEEEEEEEEEEE
        Destroy(transform.parent.gameObject);
      }
    }

    private float GetHealthPercent() {
        return (float)m_healthSystem.Health / m_healthSystem.MaxHealth * 100.0f;
    }

    private IEnumerator SetSpriteIndex()
    {
        switch (GetHealthPercent())
        {
            case < 40:
                LureSprite.sprite = Broken;
                break;
            case < 90:
                LureSprite.sprite = AlmostBroken;
                break;
            case <= 100:
                if (m_spriteIndex == 0)
                {
                    LureSprite.sprite = SpriteA;
                    m_spriteIndex = 1;
                }
                else
                {
                    LureSprite.sprite = SpriteB;
                    m_spriteIndex = 0;
                }
                break;
        }
        yield return new WaitForSeconds(.5f);
        StartCoroutine(SetSpriteIndex());
    }
}
