using System;
//using System.Runtime.CompilerServices;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using static UnityEngine.Rendering.DebugUI;

public class PlayerInput : MonoBehaviour
{
    public Slider angleXSlider;
    public Slider thrustPowerSlider;

    public GameObject player;

    //Czu�o�� przechylenia telefonu
    [SerializeField]
    private float tiltPhoneZ = 2f;
    //Moc drona
    [SerializeField]
    private float thrustForcePlayer = 0.2f;
    //Czu�o�� przechylenia drona
    [SerializeField]
    private float rotationPlayer;

    private Vector3 movement;
    private Vector3 gyroOrientation;


    private void Start()
    {
        thrustPowerSlider.onValueChanged.AddListener(OnThrPowSliderChanged);

        //W��czamy akcelerometr
        Input.gyro.enabled = true;

        if (!SystemInfo.supportsGyroscope)
        {
            Debug.LogError("Gyroscope not supported on this device!");
            return;
        }
    }

    private void Update()
    {

    }

    private void OnThrPowSliderChanged(float value)
    {
        
    }

    
}
