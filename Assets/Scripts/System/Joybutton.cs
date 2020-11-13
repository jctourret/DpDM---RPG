using UnityEngine;
using UnityEngine.EventSystems;

public class Joybutton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public bool pressed;

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
    }
}
