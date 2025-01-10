using System.IO;
using UnityEngine;

public class AIDroneDataLogger : MonoBehaviour
{
    public AIDroneController droneController;
    private string filePath;
    private float timeSinceLastWrite = 0f;
    public float writeInterval = 10f; // Zapisuj co 10 sekund
    public int FlightID = 100;

    void Start()
    {
        filePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/DroneTrainingData.csv";

        // Sprawdzamy, czy plik już istnieje. Jeśli nie, zapisujemy nagłówki.
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "FlightID,DronPosX,DronPosY,DronPosZ," +
                                        "WaypointPosX,WaypointPosY,WaypointPosZ," +
                                        "VelocityX,VelocityY,VelocityZ\n");
        }
    }

    void FixedUpdate()
    {
        if (droneController == null) return;

        Rigidbody rb = droneController.GetComponent<Rigidbody>();
        Waypoints waypoints = droneController.waypointsObject;

        if (rb != null && waypoints != null)
        {
            // Zwiększanie czasu od ostatniego zapisu
            timeSinceLastWrite += Time.fixedDeltaTime;

            if (timeSinceLastWrite >= writeInterval)
            {
                // Resetuj licznik czasu i zapisuj dane
                timeSinceLastWrite = 0f;

                Vector3 dronPos = droneController.transform.position;
                Vector3 waypointPos = waypoints.GetWaypoint(droneController.GetCurrentWaypoint()).position;
                Vector3 velocity = rb.linearVelocity;

                // Tworzenie wiersza z danymi
                string dataLine = $"{FlightID},{dronPos.x},{dronPos.y},{dronPos.z}," +
                                  $"{waypointPos.x},{waypointPos.y},{waypointPos.z}," +
                                  $"{velocity.x},{velocity.y},{velocity.z}\n";

                // Zapisujemy dane na końcu pliku
                File.AppendAllText(filePath, dataLine);
            }
        }
    }
}
