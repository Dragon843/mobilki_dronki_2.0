using UnityEngine;

public class AIDroneController : MonoBehaviour
{
    public Waypoints waypointsObject; // Obiekt Waypoints, który zawiera punkty kontrolne
    private int currentWaypoint = 0;

    public float moveSpeed = 5f; // Prędkość ruchu
    public float turnSpeed = 3f;  // Prędkość obracania

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


    private void NavigateToWaypoint()
{
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

    Debug.Log($"Ruch do waypointa {currentWaypoint}: {targetWaypoint.name}, Pozycja waypointa: {targetWaypoint.position}");
    Debug.Log($"Odległość do waypointa: {distance}");

    // Jeśli odległość do waypointa jest mniejsza niż próg, przejdź do następnego waypointa
    if (distance < 1f)
    {
        //Pozycja drona gdy osiągnie waypointa
        Debug.Log($"Pozycja drona gdy osiągnie waypointa: {transform.position}");

        Debug.Log($"Osiągnięto waypoint {currentWaypoint}: {targetWaypoint.name}, Pozycja: {targetWaypoint.position}, Drone: {transform.position}");
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