using System;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class PlayerInput : MonoBehaviour
{
    Gravity gravity;

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

    private Rigidbody body;

    private Vector3 movement;
    


    private void Start()
    {
        body = player.GetComponentInChildren<Rigidbody>();
        gravity = player.GetComponentInParent<Gravity>();

        //W³¹czamy akcelerometr
        Input.gyro.enabled = true;

        if (!SystemInfo.supportsGyroscope)
        {
            Debug.LogError("Gyroscope not supported on this device!");
            return;
        }
    }

    private void FixedUpdate()
    {
        
        movement = new Vector3(Input.gyro.gravity.x * tiltZPlayer, powerSlider.value, tiltXSlider.value);
        movement.Normalize();

        body.AddForce(CalculateTiltForceX(), CalculateThrustForce(), CalculateTiltForceZ());
        /*body.AddForce(Vector3.forward * CalculateTiltForceZ());
        body.AddForce(Vector3.right * CalculateTiltForceX());
        body.AddForce(Vector3.up * CalculateThrustForce());*/
        TiltController();
        //RotationController();
        ThrustController();

        //body.AddForce(Vector3.forward * PlayerThrustForce);

        Debug.Log("Body speed: " + body.GetAccumulatedForce());
    }

    private void TiltController()
    {
        
    }
    private void ThrustController()
    {
        //Si³a o wartoœci 1 skierowana na oœ y * Prawy suwak * Si³a wznoszenia drona
        
        Debug.Log("Thrust Controller: " + body.GetAccumulatedForce());
        Debug.Log("Calculation Thrust Force" + CalculateThrustForce());
    }

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

        accurateThrustForce = movement.y * thrustForcePlayer;

        return accurateThrustForce;
    }

    private float CalculateTiltForceZ()
    {
        return movement.z * thrustForcePlayer * movement.y;
    }

    private float CalculateTiltForceX()
    {
        return movement.x * thrustForcePlayer * movement.y;
    }

    /*private void RotationController()
    {

        //Quaternion targetRotation = Quaternion.Euler(sliderTiltX.value, transform.eulerAngles.y, phoneTiltZ * PlayerTilt);
        //body.MoveRotation(Quaternion.Slerp(body.rotation, targetRotation, Time.deltaTime * PlayerRotation * -1));
        body.AddForce(Vector3.right * thrustForcePlayer * phoneTiltZ * powerSlider.value, ForceMode.Acceleration);
        
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        //Debug.Log(targetRotation);
    }*/
}
