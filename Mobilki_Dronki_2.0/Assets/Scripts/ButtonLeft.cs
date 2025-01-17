using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ButtonLeft : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Button button;
    private bool isPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }

    private void Update()
    {
        if(isPressed)
        {

        }
    }
}
