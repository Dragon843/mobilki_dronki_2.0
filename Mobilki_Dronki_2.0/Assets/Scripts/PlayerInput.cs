using System;
using System.Runtime.CompilerServices;
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
    float tiltZPhone = 2f;
    //Moc drona
    [SerializeField]
    float thrustForcePlayer = 20f;
    //Czu�o�� przechylenia drona
    [SerializeField]
    float rotationPlayer;

    private Rigidbody body;

    private Vector3 movement;


    private void Start()
    {
        body = player.GetComponentInChildren<Rigidbody>();

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
        
        movement = new Vector3(Input.gyro.gravity.x * tiltZPhone, powerSlider.value, tiltXSlider.value);
        movement.Normalize();

        //body.AddForce(CalculateTiltForceX(), CalculateThrustForce(), CalculateTiltForceZ(),ForceMode.Acceleration);
        body.AddForce(0, CalculateThrustForce(), 0,ForceMode.Acceleration);
        
        RotationController();

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

        return 9.81f + (powerSlider.value * (thrustForcePlayer - 9.81f));
    }

    private float CalculateTiltForceZ()
    {
        return movement.z * thrustForcePlayer * movement.y;
    }

    private float CalculateTiltForceX()
    {
        return movement.x * thrustForcePlayer * movement.y;
    }

    private void RotationController()
    {
        float gyroOriantation = Input.gyro.gravity.y;
        Quaternion anglePlayer = Quaternion.Euler(tiltXSlider.value, 0, -0.5f + (45 * Input.gyro.gravity.y));
        player.transform.rotation = Quaternion.Lerp(player.transform.rotation, anglePlayer, Time.deltaTime * rotationPlayer);

        Debug.Log(Input.gyro.gravity.y);
    }

    
}
