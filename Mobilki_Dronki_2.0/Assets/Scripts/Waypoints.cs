using UnityEngine;
public class Waypoints : MonoBehaviour
{
    public Transform[] waypoints; // Tablica punkt�w kontrolnych
    void Start(){ if (waypoints.Length == 0) {Debug.LogError("Nie znaleziono �adnych waypoint�w!");}}
    public Transform GetWaypoint(int index)
    {
        if (index >= 0 && index < waypoints.Length) {return waypoints[index];}
        else {Debug.LogError($"Indeks {index} jest poza zakresem! Liczba waypoint�w: {waypoints.Length}");return null;}
    }

    public int GetWaypointCount(){ return waypoints.Length;} // Zwraca liczb� punkt�w kontrolnych
}
