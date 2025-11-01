using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private Doors m_parent;

    void OnEnable()
    {
        transform.GetComponent<Collider2D>().isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
