using System.Collections;
using UnityEngine;

public class DoorAnimator : MonoBehaviour
{
    public SpriteRenderer SpriteRendererA;
    public SpriteRenderer SpriteRendererB;

    public float AnimationSpeed;

    [Range(0, 2)]
    public int damage;

    [Space(10)]

    public Sprite ac0;
    public Sprite ac1;
    public Sprite ac2;
    public Sprite ao0;
    public Sprite ao1;
    public Sprite ao2;

    [Space(10)]

    public Sprite bc0;
    public Sprite bc1;
    public Sprite bc2;
    public Sprite bo0;
    public Sprite bo1;
    public Sprite bo2;

    public AudioSource audio;
    public AudioSource clockRoom;

    private bool m_isRoomA;

    void OnEnable()
    {
        m_isRoomA = true;
    }

    public void UpdateState(bool isOpen = false)
    {
        if (isOpen)
        {
            switch (damage)
            {
                case 0:
                    SpriteRendererA.sprite = ao0;
                    SpriteRendererB.sprite = bo0;
                    break;
                case 1:
                    SpriteRendererA.sprite = ao1;
                    SpriteRendererB.sprite = bo1;
                    break;
                case 2:
                    SpriteRendererA.sprite = ao2;
                    SpriteRendererB.sprite = bo2;
                    break;
            }
        }
        else
        {
            switch (damage)
            {
                case 0:
                    SpriteRendererA.sprite = ac0;
                    SpriteRendererB.sprite = bc0;
                    break;
                case 1:
                    SpriteRendererA.sprite = ac1;
                    SpriteRendererB.sprite = bc1;
                    break;
                case 2:
                    SpriteRendererA.sprite = ac2;
                    SpriteRendererB.sprite = bc2;
                    break;
            }
        }
    }

    public void OpenDoorAnimation()
    {
        StartCoroutine(OpenSequence());
    }

    private IEnumerator OpenSequence()
    {
        UpdateState(true);

        m_isRoomA = !m_isRoomA;
        audio.Play();
        clockRoom.volume = m_isRoomA ? 0.185f : .05f;
        
        yield return new WaitForSeconds(AnimationSpeed);

        UpdateState(false);
    }
}
