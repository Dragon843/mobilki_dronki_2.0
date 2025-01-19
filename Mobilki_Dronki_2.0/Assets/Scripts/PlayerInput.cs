using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    public Rigidbody playerBody;
    public Waypoints waypointsObject;
    public int currentWaypoint = 18;
    public Timer timer;

    [Header("UI Elements")]
    public Slider sliderAngleX;
    public Slider sliderThrustFower;

    [Header("Flight settings")]
    [SerializeField]
    private float playerThrustForce = 40f; // Siła lotu drona
    [SerializeField]
    private float playerAngleX = 20f; // Maksymalne nachylenie w osi X
    [SerializeField]
    private float playerRotateY = 140f; // Prędkość zmiany obrotu w osi Y

    /* [SerializeField]
    private float playerAngleZ = 20f; // Maksymalne nachylenie w osi Z */

    //private float phoneRotation; // Zmienna do przetrzymywania wartości żyroskopu na osi Z

    private float yTransformVec; // Zmienna przechowująca wartość procentową sliderThrustPower
    private float xTransformRot; // Zmienna przechowująca wartość procentową sliderAngleX

    //private float zTransformRot;

    private Vector3 droneRotation; // Wektor obrotu w przestrzeniu 3D dla drona

    [Header("Waypoints")]
    public Waypoints waypoints; // Referencja do skryptu Waypoints
    public Timer timer; // Referencja do skryptu Timer
    private bool flag = false;

    private void Start()
    {
        currentWaypoint = 18;
        playerBody = GetComponent<Rigidbody>();

        sliderThrustFower.onValueChanged.AddListener(OnThrPowSliderChanged);
        sliderAngleX.onValueChanged.AddListener(OnAngleXSliderChanged);

        droneRotation = transform.rotation.eulerAngles;

        /* // Wlaczamy akcelerometr
        Input.gyro.enabled = true;

        if (!SystemInfo.supportsGyroscope)
        {
            Debug.LogError("Gyroscope not supported on this device!");
            return;
        } */

        // Znajdź skrypty Waypoints i Timer w scenie, jeśli nie są przypisane w inspektorze
        if (waypoints == null)
        {
            waypoints = FindObjectOfType<Waypoints>();
        }
        if (timer == null)
        {
            timer = FindObjectOfType<Timer>();
        }
        
    }

    private void Update()
    {
        if (transform.position.y <= -10f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        // Sprawdź odległość od waypointa
        CheckWaypointDistance();
    }

    private void FixedUpdate()
    {        
        playerBody.AddRelativeForce(0f, yTransformVec * playerThrustForce * Time.deltaTime - Physics.gravity.y, 0f, ForceMode.Acceleration);
        //gyroControll();
        RotationControll();
    }

    // Sprawdź odległość od waypointa o indeksie 18
    private void CheckWaypointDistance()
    {
        if (waypoints == null || timer == null)
        {
            Debug.LogError("Brak przypisanego skryptu Waypoints lub Timer!");
            return;
        }

        // Pobierz waypoint 
        Transform waypoint19 = waypoints.GetWaypoint(18);

        // Oblicz odległość między dronem a waypointem
        float distance = Vector3.Distance(transform.position, waypoint19.position);
        int counterHoop = HoopManager.counter;
        if (distance < 0.3f && !flag && counterHoop >= 6)
        {
            float elapsedTime = timer.GetElapsedTime();
            Debug.Log($"Waypoint 18 osiągnięty! Czas: {elapsedTime:F2} sekund.");
            flag = true;
        }
    }

    // Event Listener dla sliderThrustForce
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

    // Event Listener dla sliderAngleX
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

    /*private void gyroControll()
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
    }*/

    // Funkcja aktywująca się przy przyciśnięciu lewego przycisku
    public void OnDownLeftButton()
    {
        droneRotation.y -= playerRotateY * Time.deltaTime;

        transform.rotation = Quaternion.Euler(droneRotation);
    }

    // Funkcja Aktywująca się przy przyciśnięciu prawego przycisku
    public void OnDownRightButton()
    {
        droneRotation.y += playerRotateY * Time.deltaTime;

        transform.rotation = Quaternion.Euler(droneRotation);
    }

    // Funkcja kontrolująca obrót drona
    private void RotationControll()
    {
        droneRotation.x = xTransformRot * playerAngleX * 45f * Time.deltaTime;
        //droneRotation.z = zTransformRot * playerAngleZ * Time.fixedDeltaTime;
        
        playerBody.rotation = Quaternion.Euler(droneRotation);
        
        //Debug.Log("żyroskop: " + Input.gyro.attitude.eulerAngles.z);
        //Debug.Log("transform.rotation: " + transform.rotation);

        //Sprawdzanie czy koniec wyjściugu
         Transform targetWaypoint = waypointsObject.GetWaypoint(currentWaypoint);

        // Oblicz kierunek do waypointa
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetWaypoint.position);

        if (distance < 0.3f)
        {
            Debug.Log($"Osiągnięto waypoint {currentWaypoint}: {targetWaypoint.name}");
            if(targetWaypoint.name == "Waypoint19"){
                float timeAtWaypoint = timer.GetElapsedTime();
                Debug.Log($"Czas to: {timeAtWaypoint} s");  
                return;
            }
        }
    }
}
