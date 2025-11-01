using UnityEngine;

public class Doors : MonoBehaviour
{
    public Transform RoomA;
    public Transform RoomB;

    private BoxCollider2D m_colA;
    private BoxCollider2D m_colB;

    private void OnEnable()
    {
        m_colA = RoomA.GetComponent<BoxCollider2D>();
        m_colB = RoomB.GetComponent<BoxCollider2D>();
    }

    
}
