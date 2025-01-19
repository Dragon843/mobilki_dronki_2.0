using UnityEngine;

public class HoopTiger : MonoBehaviour
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

    public void DisableHoop()
    {
        gameObject.SetActive(false); // Wy��cz obiekt
    }
}