using System.Collections;
using UnityEngine;

public class VelocityBasedAnimation : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sprite;

    [Space(10)]
    [SerializeField]
    private Sprite i1;
    [SerializeField]
    private Sprite i2;
    [SerializeField]
    private Sprite w1;
    [SerializeField]
    private Sprite w2;

    private bool m_toggle;

    private Rigidbody2D m_rb;
    private void OnEnable()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(SetFrame());
    }

    private IEnumerator SetFrame()
    {
        m_toggle = !m_toggle;
        bool isLeft = m_rb.linearVelocityX < 0;
        sprite.flipX = !isLeft;

        if (m_rb.linearVelocity.magnitude <= .1f)
        {
            sprite.sprite = m_toggle ? i1 : i2;
        }
        else
        {
            sprite.sprite = m_toggle ? w1 : w2;
        }
        yield return new WaitForSeconds(.5f);

        StartCoroutine(SetFrame());
    }
}
