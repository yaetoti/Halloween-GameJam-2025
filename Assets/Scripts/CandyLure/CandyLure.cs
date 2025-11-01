using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class CandyLure : MonoBehaviour
{
    public SpriteRenderer LureSprite;
    public float Health;

    private float m_maxHealth;
    private int m_spriteIndex;

    [Space(10)]
    public HealthBar HealthBar;
    [Space(10)]

    public Sprite SpriteA;
    public Sprite SpriteB;
    public Sprite AlmostBroken;
    public Sprite Broken;

    private void OnEnable()
    {
        m_maxHealth = Health;
        HealthBar.MaxHealth = m_maxHealth;
        HealthBar.Health = Health;
    }

    private void Start()
    {
        StartCoroutine(SetSpriteIndex());
    }

    public void Damage()
    {
        Health--;
        HealthBar.Health = Health;
        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(transform.parent.gameObject);
    }

    public void SetStartHealth(float health)
    {
        m_maxHealth = Health = health;
    }

    private float GetHealthPercent()
    {
        return Health / m_maxHealth * 100;
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
