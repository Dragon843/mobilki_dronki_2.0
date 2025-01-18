using UnityEngine;

public class HoopTrigger : MonoBehaviour
{
    public HoopManager hoopManager; // Przypisz HoopManager w inspektorze

    private void OnTriggerEnter(Collider other)
    {
        // Sprawd�, czy obiekt, kt�ry wszed� w trigger, ma tag "Drone"
        if (other.CompareTag("Dron"))
        {
            hoopManager.HoopPassed(gameObject); // Powiadom HoopManager o przej�ciu
        }
    }
}