using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class PlayerInput : MonoBehaviour
{
    public Slider sliderAngle;
    public Slider sliderPower;
    public GameObject ObjectControlled;


    void Start()
    {
        sliderAngle.onValueChanged.AddListener(onSliderAngleChange);
    }

    void Update()
    {
        ObjectControlled.GetComponent<Rigidbody>().AddForce(0, sliderPower.value, 0);
        ObjectControlled.transform.rotation = Quaternion.Euler(sliderAngle.value, 0, 0);
    }

    private void onSliderAngleChange(float value)
    {
        
    }
}
