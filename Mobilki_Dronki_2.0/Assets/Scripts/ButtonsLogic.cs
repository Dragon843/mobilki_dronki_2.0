using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonsLogic : MonoBehaviour
{
    public Button buttonLeft;
    public Button buttonRight;

    private bool isButtonLeftPressed = false;
    private bool isButtonRightPressed = false;

    private Vector3 rotate;

    public void OnButtonLeftDown(PointerEventData eventData)
    {  
        isButtonLeftPressed = true;
        ChangeButtonVisual(buttonLeft, true);
    }

    public void OnButtonRightDown(PointerEventData eventData)
    {  
        isButtonRightPressed = true;
        ChangeButtonVisual(buttonRight, true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isButtonLeftPressed)
        {
            rotate.y += 0.5f;
            transform.rotation = Quaternion.Euler(rotate);
        }
        if(isButtonRightPressed)
        {
            rotate.y -= 0.5f;
            transform.rotation = Quaternion.Euler(rotate);
        }
    }

    private void ChangeButtonVisual(Button button,bool pressed)
    {
        var colors = button.colors;
        colors.normalColor = pressed ? Color.gray : Color.white;
        button.colors = colors;
    }
}
