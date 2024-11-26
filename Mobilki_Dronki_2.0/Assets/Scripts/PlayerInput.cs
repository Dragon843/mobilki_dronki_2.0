using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    public Slider sliderTiltX;
    [SerializeField]
    public Slider sliderPower;
    [SerializeField]
    public GameObject player;
    [SerializeField]
    //Czu³oœæ przechylenia telefonu
    public float PlayerTilt;
    [SerializeField]
    public float PlayerThrustForce;
    [SerializeField]
    //Czu³oœæ przechylenia drona
    public float PlayerRotation;

    private Rigidbody body;
    private float phoneTilt;

    private void Start()
    {
        body = player.GetComponent<Rigidbody>();

        if(!SystemInfo.supportsGyroscope)
        {
            Debug.LogError("Gyroscope not supported on this device!");
            return;
        }

        //W³¹czamy akcelerometr
        Input.gyro.enabled = true;
    }

    private void FixedUpdate()
    {
        TiltController();
        RotationController();
        ThrustController();
    }
    
    private void TiltController()
    {
        //Pobiera wartoœæ przechylenia telefonu i mno¿y przez czu³oœæ
        phoneTilt = Input.gyro.gravity.x * PlayerTilt;
    }
    private void ThrustController()
    {
        //Si³a o wartoœci 1 skierowana na oœ y * Prawy suwak * Si³a wznoszenia drona
        body.AddForce(Vector3.up * sliderPower.value * PlayerThrustForce, ForceMode.Force);
    }

    private void RotationController()
    {
        Quaternion targetRotation = Quaternion.Euler(sliderTiltX.value, transform.eulerAngles.y, phoneTilt);
        body.MoveRotation(Quaternion.Slerp(body.rotation, targetRotation, Time.fixedDeltaTime * PlayerRotation));
    }
}
