using UnityEngine;
using DG.Tweening;

public class Doors : MonoBehaviour
{
    public Transform RoomA;
    public Transform RoomB;
    public int IsAbleSwitchRoom; // If equals zero than we able to switch rooms

    private void OnEnable()
    {
        IsAbleSwitchRoom = 0;
    }

    public void SwitchRoom(Transform room, Transform passenger)
    {
        if (IsAbleSwitchRoom != 0)
        {
            return;
        }

        GameManager.Instance.ToggleRoom();

        Collider2D collider = passenger.GetComponent<Collider2D>();
        collider.enabled = false;

        if (room == RoomA)
        {
            passenger.DOMove(RoomB.position, 1, false).SetEase(Ease.InCubic).OnComplete(() => collider.enabled = true);
        }
        else
        {
            passenger.DOMove(RoomA.position,1,false).SetEase(Ease.InCubic).OnComplete(() => collider.enabled = true);
        }
    }


}
