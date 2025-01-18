using UnityEngine;

public class AIDroneController : MonoBehaviour
{
    public Waypoints waypointsObject;
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
        if (rb == null){ Debug.LogError("Nie znaleziono Rigidbody na obiekcie drona!"); return;}

        if (waypointsObject == null){ Debug.LogError("Obiekt Waypoints nie jest przypisany!");return;}

        if (timer == null){ timer = FindObjectOfType<Timer>();} // Automatycznie znajdź Timer w scenie
        currentWaypoint = 0;
    }
    void FixedUpdate(){NavigateToWaypoint();}

    public int GetCurrentWaypoint(){ return currentWaypoint;}

    private void NavigateToWaypoint()
    {
        if(isStopped){return;}

        if (waypointsObject == null){ Debug.LogError("waypointsObject jest null! Upewnij się, że obiekt Waypoints jest przypisany w inspektorze.");return;}

        if (currentWaypoint >= waypointsObject.GetWaypointCount()){ Debug.LogError("Indeks waypointa poza zakresem!");return;}

        Transform targetWaypoint = waypointsObject.GetWaypoint(currentWaypoint);

        // Oblicz kierunek do waypointa
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetWaypoint.position);

        if (distance < 0.3f)
        {
            Debug.Log($"Osiągnięto waypoint {currentWaypoint}: {targetWaypoint.name}");
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
        else{ rb.linearVelocity = direction * moveSpeed;}

        // Normalizacja kierunku i ruch drona
        direction.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime));
        rb.linearVelocity = transform.forward * moveSpeed;
    }
}