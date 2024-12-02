using System;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    Slider tiltXSlider;
    [SerializeField]
    Slider powerSlider;

    [SerializeField]
    GameObject player;

    //Czu³oœæ przechylenia telefonu
    [SerializeField]
    float tiltZPlayer;
    //Moc drona
    [SerializeField]
    float thrustForcePlayer;
    //Czu³oœæ przechylenia drona
    [SerializeField]
    float rotationPlayer;


    Gravity gravity;
    private Rigidbody body;
    


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
        TiltController();
        //RotationController();
        ThrustController();
        
        //body.AddForce(Vector3.forward * PlayerThrustForce);
    }

    private void TiltController()
    {
        body.AddForce(Vector3.forward * CalculateTiltForceX());
        body.AddForce(Vector3.right * CalculateTiltForceZ());
    }
    private void ThrustController()
    {
        /*float accumulatedForce = CalculateThrustForce() - (CalculateTiltForceX() + CalculateTiltForceZ());

        if(accumulatedForce < 0)
        {
            accumulatedForce = 0;
        }*/

        //Si³a o wartoœci 1 skierowana na oœ y * Prawy suwak * Si³a wznoszenia drona
        body.AddForce(Vector3.up * CalculateThrustForce());
        Debug.Log("Thrust Controller: " + body.GetAccumulatedForce());
        Debug.Log("Calculation Thrust Force" + CalculateThrustForce());
    }

    /*private void RotationController()
    {

        //Quaternion targetRotation = Quaternion.Euler(sliderTiltX.value, transform.eulerAngles.y, phoneTiltZ * PlayerTilt);
        //body.MoveRotation(Quaternion.Slerp(body.rotation, targetRotation, Time.deltaTime * PlayerRotation * -1));
        body.AddForce(Vector3.right * thrustForcePlayer * phoneTiltZ * powerSlider.value, ForceMode.Acceleration);
        
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        //Debug.Log(targetRotation);
    }*/

    private float CalculateThrustForce()
    {
        float accurateThrustForce;

        //
        /*if (thrustForcePlayer - gravity.Y <= gravity.Y)
        {
            accurateThrustForce = powerSlider.value * thrustForcePlayer;
        }
        else
        {
            accurateThrustForce = (powerSlider.value * (thrustForcePlayer - gravity.Y)) + gravity.Y;
        }*/

        accurateThrustForce = powerSlider.value * thrustForcePlayer;

        return accurateThrustForce;
    }

    private float CalculateTiltForceX()
    {
        float accurateTiltXForce;

        accurateTiltXForce = tiltXSlider.value * thrustForcePlayer;

        Debug.Log("Calculation Tilt X Force: " + accurateTiltXForce);
        return accurateTiltXForce;
    }

    private float CalculateTiltForceZ()
    {
        float phoneTiltZ = Input.gyro.gravity.x;
        float accurateTiltZForce;

        accurateTiltZForce = phoneTiltZ * thrustForcePlayer;

        return accurateTiltZForce;
    }
}
