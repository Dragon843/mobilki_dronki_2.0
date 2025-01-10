using UnityEngine;

public class AIDroneController : MonoBehaviour
{
    public Waypoints waypointsObject; // Obiekt Waypoints, który zawiera punkty kontrolne
    public int currentWaypoint = 0;
    public Timer timer;
    private bool hasReachedWaypoint19 = false;
    private bool isStopped = false;

    public float moveSpeed = 3f; // Prędkość ruchu
    public float turnSpeed = 4f;  // Prędkość obracania

    private Rigidbody rb;

    void Start()
{
    rb = GetComponent<Rigidbody>();
    if (rb == null)
    {
        Debug.LogError("Nie znaleziono Rigidbody na obiekcie drona!");
        return;
    }

    if (waypointsObject == null)
    {
        Debug.LogError("Obiekt Waypoints nie jest przypisany!");
        return;
    }

    Debug.Log("Waypointy w Waypoints:");
    for (int i = 0; i < waypointsObject.GetWaypointCount(); i++)
    {
        Transform waypoint = waypointsObject.GetWaypoint(i);
        if (waypoint != null)
        {
            Debug.Log($"Waypoint {i}: {waypoint.name}, Pozycja: {waypoint.localPosition}");
        }
        else
        {
            Debug.LogError($"Waypoint {i} jest null!");
        }
    }

    if (timer == null)
        {
            timer = FindObjectOfType<Timer>(); // Automatycznie znajdź Timer w scenie
        }

    currentWaypoint = 0;
}


    void FixedUpdate()
{
    if (waypointsObject == null)
    {
        Debug.LogError("waypointsObject jest null w FixedUpdate! Sprawdź przypisanie obiektu Waypoints.");
        return;
    }

    if (rb == null)
    {
        Debug.LogError("Rigidbody drona jest null! Upewnij się, że Rigidbody jest przypisane.");
        return;
    }

    // Loguj pozycję drona
    Debug.Log($"Pozycja drona: {transform.position}");
    Debug.Log($"Prędkość Rigidbody: {rb.linearVelocity}");

    if (waypointsObject.GetWaypointCount() == 0)
    {
        Debug.LogWarning("Lista waypointów jest pusta!");
        return;
    }

    Debug.Log("Dron AI działa i przetwarza punkty kontrolne.");
    NavigateToWaypoint();
}
// Dodanie metody getter do currentWaypoint
    public int GetCurrentWaypoint()
    {
        return currentWaypoint;
    }

    private void NavigateToWaypoint()
{
    if(isStopped){return;}

    if (waypointsObject == null)
    {
        Debug.LogError("waypointsObject jest null! Upewnij się, że obiekt Waypoints jest przypisany w inspektorze.");
        return;
    }

    if (currentWaypoint >= waypointsObject.GetWaypointCount())
    {
        Debug.LogError("Indeks waypointa poza zakresem!");
        return;
    }

    Transform targetWaypoint = waypointsObject.GetWaypoint(currentWaypoint);

    if (targetWaypoint == null)
    {
        Debug.LogError($"Waypoint {currentWaypoint} jest null! Sprawdź listę waypointów.");
        return;
    }

    // Oblicz kierunek do waypointa
    Vector3 direction = (targetWaypoint.position - transform.position).normalized;
    float distance = Vector3.Distance(transform.position, targetWaypoint.position);

    Debug.Log($"Ruch do waypointa {currentWaypoint}: {targetWaypoint.name}, Pozycja waypointa: {targetWaypoint.localPosition}");
    Debug.Log($"Odległość do waypointa: {distance}");

    // Jeśli odległość do waypointa jest mniejsza niż próg, przejdź do następnego waypointa
    if (distance < 0.3f)
    {
        //Pozycja drona gdy osiągnie waypointa   Debug.Log($"Pozycja drona gdy osiągnie waypointa: {transform.position}");

        Debug.Log($"Osiągnięto waypoint {currentWaypoint}: {targetWaypoint.name}, Pozycja: {targetWaypoint.position}, Drone: {transform.position}");
        rb = GetComponent<Rigidbody>();
        if(targetWaypoint.name == "Waypoint19" && !hasReachedWaypoint19) {
            rb.linearVelocity = Vector3.zero; 
            rb.useGravity = true; 
            float timeAtWaypoint = timer.GetElapsedTime();
            Debug.Log($"Czas to: {timeAtWaypoint} s");
            hasReachedWaypoint19 = true; // Oznacz, że waypoint został osiągnięty
            isStopped = true;
            return;}
        
        currentWaypoint = (currentWaypoint + 1) % waypointsObject.GetWaypointCount(); 
    }
    else
    {
        Debug.Log($"Ruch w kierunku waypointa {currentWaypoint}: {direction}");
        rb.linearVelocity = direction * moveSpeed;
    }

    // Normalizacja kierunku i ruch drona
    direction.Normalize();
    Quaternion targetRotation = Quaternion.LookRotation(direction);
    rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime));
    rb.linearVelocity = transform.forward * moveSpeed;
}
}