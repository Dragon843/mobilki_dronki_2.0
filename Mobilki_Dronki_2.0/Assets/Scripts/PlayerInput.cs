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
    //Czu�o�� przechylenia telefonu
    public float PlayerTilt;
    [SerializeField]
    public float PlayerThrustForce;
    [SerializeField]
    //Czu�o�� przechylenia drona
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

        //W��czamy akcelerometr
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
        //Pobiera warto�� przechylenia telefonu i mno�y przez czu�o��
        phoneTilt = Input.gyro.gravity.x * PlayerTilt;
    }
    private void ThrustController()
    {
        //Si�a o warto�ci 1 skierowana na o� y * Prawy suwak * Si�a wznoszenia drona
        body.AddForce(Vector3.up * sliderPower.value * PlayerThrustForce, ForceMode.Force);
    }

    private void RotationController()
    {
        Quaternion targetRotation = Quaternion.Euler(sliderTiltX.value, transform.eulerAngles.y, phoneTilt);
        body.MoveRotation(Quaternion.Slerp(body.rotation, targetRotation, Time.fixedDeltaTime * PlayerRotation));
    }
}
