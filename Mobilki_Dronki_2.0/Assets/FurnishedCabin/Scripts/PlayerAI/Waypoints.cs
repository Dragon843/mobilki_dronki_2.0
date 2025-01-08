using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public Transform[] waypoints; // Tablica punktów kontrolnych

    void Start()
    {
        // Automatyczne przypisanie waypointów, jeœli nie zosta³y rêcznie przypisane
        /*if (waypoints == null || waypoints.Length == 0)
        {
            waypoints = GetComponentsInChildren<Transform>();
            waypoints = System.Array.FindAll(waypoints, w => w != transform); // Wykluczamy sam obiekt Waypoints
        }*/

        if (waypoints.Length == 0) {Debug.LogError("Nie znaleziono ¿adnych waypointów!");}
    }

    public Transform GetWaypoint(int index)
    {
        if (index >= 0 && index < waypoints.Length) {return waypoints[index];}
        else {Debug.LogError($"Indeks {index} jest poza zakresem! Liczba waypointów: {waypoints.Length}");return null;}
    }

    public int GetWaypointCount()
    {return waypoints.Length;} // Zwraca liczbê punktów kontrolnych
}
