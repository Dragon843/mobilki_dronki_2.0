using UnityEngine;

public class HoopTiger : MonoBehaviour
{
    public HoopManager hoopManager; // Przypisz HoopManager w inspektorze

    private void OnTriggerEnter(Collider other)
    {
        // SprawdŸ, czy obiekt, który wszed³ w trigger, ma tag "Drone"
        if (other.CompareTag("Dron"))
        {
            hoopManager.HoopPassed(gameObject); // Powiadom HoopManager o przejœciu
        }
    }

    public void DisableHoop()
    {
        gameObject.SetActive(false); // Wy³¹cz obiekt
    }
}