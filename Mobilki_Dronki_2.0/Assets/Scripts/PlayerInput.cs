using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    public Rigidbody playerBody;

    [Header("UI Elements")]
    public Slider sliderAngleX;
    public Slider sliderThrustPower;
    public Button buttonLeftRotateZ;
    public Button buttonRightRotateZ;

    [Header("Flight settings")]
    [SerializeField]
    private float playerThrustForce = 30f; //Siła lotu drona
    [SerializeField]
    private float playerAngleX = 65f; //Maksymalne nachylenie w osi X
    [SerializeField]
    private float playerAngleZ = 65f; //Maksymalne nachylenie w osi Z
    [SerializeField]
    private float playerRotateZ;

    private float phoneRotation; //
    private float yTransformVec;
    private float xTransformRot;
    private float zTransformRot;

    private bool buttonLeftPressed = false;
    private bool buttonRightPressed = false;

    private Vector3 droneRotation;

    private void Start()
    {
        playerBody = GetComponent<Rigidbody>();

        sliderThrustPower.onValueChanged.AddListener(OnThrPowSliderChanged);
        sliderAngleX.onValueChanged.AddListener(OnAngleXSliderChanged);

        droneRotation = transform.rotation.eulerAngles;

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
        playerBody.AddRelativeForce(0f, yTransformVec * playerThrustForce * Time.fixedDeltaTime - Physics.gravity.y, 0f, ForceMode.Acceleration);
        gyroControll();
        RotationControll();
    }

    private void OnThrPowSliderChanged(float value)
    {
        if(value <= 0.2f && value >= -0.2)
        {
            yTransformVec = 0;
        }
        else
        {
            yTransformVec = value;
        }
        
    } 

    private void OnAngleXSliderChanged(float value)
    {
        if(value <= 0.2f && value >= -0.2f)
        {
            xTransformRot = 0f;
        }
        else
        {
            xTransformRot = value;
        }
    }

    private void OnButtonLeftDown()
    {
        buttonLeftPressed = true;
    }

    private void OnButtonLeftUp()
    {

    }

    private void OnButtonRightDown()
    {

    }

    private void OnButtonRightUp()
    {
        
    }

    private void OnClickLeftButton()
    {
        droneRotation.x = transform.rotation.x;
        droneRotation.z = transform.rotation.z;
        droneRotation.y += playerRotateZ * Time.fixedDeltaTime;

        transform.rotation = Quaternion.Euler(droneRotation);
    }

    private void OnClickRightButton()
    {
        droneRotation.x = transform.rotation.x;
        droneRotation.z = transform.rotation.z;
        droneRotation.y -= playerRotateZ;

        transform.rotation = Quaternion.Euler(droneRotation);
    }

    private void gyroControll()
    {
        phoneRotation = Input.gyro.attitude.eulerAngles.z;
        if(phoneRotation >= 255f && phoneRotation <= 285f){
            zTransformRot = 0f;
        }
        else if(phoneRotation <= 360 && phoneRotation > 285f || phoneRotation < 255f && phoneRotation >= 180)
        {
            zTransformRot = phoneRotation - 270f;
        }

        Debug.Log(phoneRotation);
    }


    private void RotationControll()
    {
        //Quaternion stabilzeRotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
        
        
        droneRotation.x = xTransformRot * playerAngleX * 45f * Time.fixedDeltaTime;
        droneRotation.z = zTransformRot * playerAngleZ * Time.fixedDeltaTime;

        
        //droneRotation.y = transform.rotation.z;

        transform.rotation = Quaternion.Euler(droneRotation);
        
        
        //Debug.Log("żyroskop: " + Input.gyro.attitude.eulerAngles.z);
    }
}
