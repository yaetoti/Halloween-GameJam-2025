using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private Doors m_parent;

    void OnEnable()
    {
        transform.GetComponent<Collider2D>().isTrigger = true;
        m_parent = transform.parent.GetComponent<Doors>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            m_parent.SwitchRoom(transform, collision.transform);
            m_parent.IsAbleSwitchRoom++;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            m_parent.IsAbleSwitchRoom--;
        }
    }

}
