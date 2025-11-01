using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private float m_health;
    private float m_maxHealth;
    public float Health
    {
        get { return m_health; }
        set { m_health = value; UpdateHealthBar(); }
    }
    public float MaxHealth
    {
        get { return m_maxHealth; }
        set { m_maxHealth = value; UpdateHealthBar(); }
    }

    private Material m_healthBarMat;

    private void OnEnable()
    {
        m_healthBarMat = transform.GetChild(0).
                        GetComponent<SpriteRenderer>().material;
                        
        MaxHealth = MaxHealth == 0 ? 1 : MaxHealth;
    }

    private void UpdateHealthBar()
    {
        m_health = Mathf.Clamp(m_health, 0, m_maxHealth);
        m_healthBarMat.SetFloat("_Health", Health / MaxHealth);
    }
}
