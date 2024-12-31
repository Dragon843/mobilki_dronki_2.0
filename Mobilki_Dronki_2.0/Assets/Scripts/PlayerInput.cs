using System;
using Unity.Mathematics;
using Unity.VisualScripting;
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

    //Czu�o�� przechylenia telefonu
    [SerializeField]
    float tiltZPlayer = 2f;
    //Moc drona
    [SerializeField]
    float thrustForcePlayer = 20f;
    //Czu�o�� przechylenia drona
    [SerializeField]
    float rotationPlayer;



    private Vector3 movement;
    
    private float gravity;


    private void Start()
    {
        
        gravity = Physics.gravity.y;

        //W��czamy akcelerometr
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

        player.transform.position += Vector3.forward * CalculateTiltForceX();
        player.transform.position += Vector3.up * CalculateThrustForce();
        player.transform.position += Vector3.right * CalculateTiltForceZ();

        

        //Debug.Log("Body speed: " + body.GetAccumulatedForce());
        Debug.Log("Ruch znormalizowany: " + movement);
    }

    private float CalculateThrustForce()
    {
        
        /*if(powerSlider.value <= 0.2 && powerSlider.value >= -0.2)
        {
            return gravity;
        }
        else if(powerSlider.value <= 0.4 && powerSlider.value > 0.2 || powerSlider.value < -0.2 && powerSlider.value >= -0.4)
        {
            return gravity + (powerSlider.value * (thrustForcePlayer - gravity));
        }*/

        return movement.y * thrustForcePlayer;
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
        
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        //Debug.Log(targetRotation);
    }*/

    
}
