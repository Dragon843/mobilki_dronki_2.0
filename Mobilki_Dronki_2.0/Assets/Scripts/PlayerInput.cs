using System;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
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
    float tiltZPlayer = 2f;
    //Moc drona
    [SerializeField]
    float thrustForcePlayer = 20f;
    //Czu³oœæ przechylenia drona
    [SerializeField]
    float rotationPlayer;

    private Rigidbody body;

    private Vector3 movement;

    public void Update()
    {
        // kiedy dron spadnie ponizej wartosci -10 zostanie respawnowany do miejsca poczatkowego
        if (transform.position.y <= -10f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        //
    }

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

        body.AddForce(CalculateTiltForceX(), CalculateThrustForce(), CalculateTiltForceZ(),ForceMode.Acceleration);
        //RotationController();

        //Debug.Log("Body speed: " + body.GetAccumulatedForce());
        Debug.Log("Ruch znormalizowany: " + movement);
    }

    private float CalculateThrustForce()
    {
        /*
        if (thrustForcePlayer - gravity.Y <= gravity.Y)
        {
            return movement.y * thrustForcePlayer;
        }
        else
        {
            return (movement.y * (thrustForcePlayer - gravity.Y)) + gravity.Y;
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
        body.AddForce(Vector3.right * thrustForcePlayer * phoneTiltZ * powerSlider.value, ForceMode.Acceleration);
        
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        //Debug.Log(targetRotation);
    }*/
}
