using System;
using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class CandyLure : MonoBehaviour
{
    public SpriteRenderer LureSprite;

    private Health m_health;
    //private float m_maxHealth;
    private int m_spriteIndex;

    public Sprite SpriteA;
    public Sprite SpriteB;
    public Sprite AlmostBroken;
    public Sprite Broken;

    private void Awake() {
      m_health = GetComponent<Health>();
      m_health.health.AddListener(OnHealthChanged);
    }

    private void OnDestroy() {
      m_health.health.RemoveListener(OnHealthChanged);
    }

    private void Start()
    {
        StartCoroutine(SetSpriteIndex());
    }

    public void Damage() {
      m_health.health.Value--;
    }

    private void OnHealthChanged(int health) {
      if (health == 0) {
        // DIE MOTHERFUCKER DIE MOTHERFUCKER DIEEEEEEEEEEEE
        Destroy(transform.parent.gameObject);
      }
    }

    private float GetHealthPercent() {
        return (float)m_health.health.Value / m_health.maxHealth.Value * 100.0f;
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
