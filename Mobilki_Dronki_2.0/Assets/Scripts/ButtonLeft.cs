using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonLeft : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public PlayerInput player;

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
            player.OnDownLeftButton();
        }
    }
}
