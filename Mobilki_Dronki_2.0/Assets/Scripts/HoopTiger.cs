using UnityEngine;

public class HoopTrigger : MonoBehaviour
{
    public HoopManager hoopManager; // Przypisz HoopManager w inspektorze

    private void OnTriggerEnter(Collider other)
    {
        // SprawdŸ, czy obiekt, który wszed³ w trigger, ma tag "Drone"
        if (other.CompareTag("Dron"))
        {
            Debug.Log($"Dron przelecia³ przez obrêcz: {gameObject.name}");
            hoopManager.HoopPassed(gameObject); // Powiadom HoopManager o przejœciu
        }
    }
}