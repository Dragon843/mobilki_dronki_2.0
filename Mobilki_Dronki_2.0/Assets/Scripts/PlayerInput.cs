using System;
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

    //Czu³oœæ przechylenia telefonu
    [SerializeField]
    public float PlayerTilt;
    //Moc drona
    [SerializeField]
    public float PlayerThrustForce;
    //Czu³oœæ przechylenia drona
    [SerializeField]
    public float PlayerRotation;


    Gravity gravity;
    private Rigidbody body;

    //Zmienna przechowuj¹ca wartoœæ przechylenia telefonu
    private float phoneTiltZ;
    //Moc do utrzymania pozycji
    private float accurateThrustForce;
    


    private void Start()
    {
        body = player.GetComponentInChildren<Rigidbody>();
        gravity = player.GetComponentInParent<Gravity>();

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
        phoneTiltZ = Input.gyro.gravity.x;

        //TiltController();

        RotationController();
        ThrustController();

        //body.AddForce(Vector3.forward * PlayerThrustForce);
    }
    
    /*private void TiltController()
    {
        body
    }*/
    private void ThrustController()
    {
        //Si³a o wartoœci 1 skierowana na oœ y * Prawy suwak * Si³a wznoszenia drona
        //body.AddForce(Vector3.up * sliderPower.value * PlayerThrustForce * (1 - Math.Abs(phoneTiltZ)), ForceMode.Force);
        body.AddForce(Vector3.up * gravity.Y);
        Debug.Log("Thrust Controller: " + body.GetAccumulatedForce());
    }

    private void RotationController()
    {

        //Quaternion targetRotation = Quaternion.Euler(sliderTiltX.value, transform.eulerAngles.y, phoneTiltZ * PlayerTilt);
        //body.MoveRotation(Quaternion.Slerp(body.rotation, targetRotation, Time.deltaTime * PlayerRotation * -1));
        body.AddForce(Vector3.right * PlayerThrustForce * phoneTiltZ * sliderPower.value, ForceMode.Acceleration);
        
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        //Debug.Log(targetRotation);
    }

    private void CalculateForces()
    {
        accurateThrustForce = sliderPower.value * PlayerThrustForce;
    }
}
