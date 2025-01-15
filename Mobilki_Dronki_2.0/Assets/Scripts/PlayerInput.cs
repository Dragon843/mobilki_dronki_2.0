using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
    public Rigidbody rb;

    [Header("UI Elements")]
    public Slider angleXSlider;
    public Slider thrustPowerSlider;

    [Header("Flight settings")]
    [SerializeField]
    private float thrustForcePlayer = 15f; //Siła lotu drona
    [SerializeField]
    private float rotationSpeedX = 18f; //Prędkość zmiany nachylenia w X
    /*[SerializeField]
    private float rotationSpeedY = 20f; //Czulosc przechylenia telefonu */
    [SerializeField]
    private float rotationSpeedZ = 20f; //Prędkość zmiany nachylenia w Z
    
    private float rotationPhone;
    private float yTransform;
    private float xTransform;
    private float zTransform;

    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        thrustPowerSlider.onValueChanged.AddListener(OnThrPowSliderChanged);
        angleXSlider.onValueChanged.AddListener(OnAngleXSliderChanged);

        //Wlaczamy akcelerometr
        Input.gyro.enabled = true;

        if (!SystemInfo.supportsGyroscope)
        {
            Debug.LogError("Gyroscope not supported on this device!");
            return;
        }
    }

    private void Update()
    {
        if (transform.position.y <= -10f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void FixedUpdate()
    {        
        rb.AddRelativeForce(0f, yTransform * thrustForcePlayer * Time.fixedDeltaTime - Physics.gravity.y, 0f, ForceMode.Acceleration);
        gyroControll();
        RotationControll();
    }

    private void OnThrPowSliderChanged(float value)
    {
        if(value <= 0.2f && value >= -0.2)
        {
            yTransform = 0;
        }
        else
        {
            yTransform = value;
        }
        
    } 

    private void OnAngleXSliderChanged(float value)
    {
        if(value <= 9f && value >= -9f)
        {
            xTransform = 0f;
        }
        else if(value > 9f || value < -9f)
        {
            xTransform = value;
        }
    }

    private void gyroControll()
    {
        rotationPhone = Input.gyro.attitude.eulerAngles.z;
        if(rotationPhone >= 255f && rotationPhone <= 285f){
            zTransform = 0f;
        }
        else if(rotationPhone <= 360 && rotationPhone > 285f || rotationPhone < 255f && rotationPhone >= 180)
        {
            zTransform = rotationPhone - 270f;
        }

        Debug.Log(rotationPhone);
    }

    private void RotationControll()
    {
        Quaternion stabilzeRotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
        Vector3 rotation = transform.rotation.eulerAngles;

        rotation.x = xTransform * rotationSpeedX * Time.fixedDeltaTime;
        rotation.z = zTransform * rotationSpeedZ * Time.fixedDeltaTime;

        transform.rotation = Quaternion.Euler(rotation);

        //Debug.Log("żyroskop: " + Input.gyro.attitude.eulerAngles.z);
    }
}
