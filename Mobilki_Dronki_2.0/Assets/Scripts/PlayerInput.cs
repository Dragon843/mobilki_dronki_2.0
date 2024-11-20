using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    public Slider sliderAngle;
    [SerializeField]
    public Slider sliderPower;
    [SerializeField]
    public GameObject PlayerControlled;
    [SerializeField]
    public float PlayerSpeed = 5f;

    private float LeftRightControll;

    void Start()
    {
        LeftRightControll = Input.acceleration.x;
    }

    void Update()
    {
        PlayerControlled.GetComponent<Rigidbody>().AddForce(0, sliderPower.value, 0);
        PlayerControlled.GetComponent<Rigidbody>().AddTorque(PlayerSpeed * sliderAngle.value, 0, 0);
    }
}
