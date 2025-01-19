using UnityEngine;
using TMPro;

public class DisplayPoints : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText; // Referencja do TextMeshProUGUI
    [SerializeField] private HoopManager hoopManager; // Referencja do HoopManager

    private void Update()
    {
        if (hoopManager != null && pointsText != null)
        {
            // Pobierz wartoœæ counter z HoopManager
            int counters = HoopManager.counter;
            // Ustaw tekst w formacie "Punkty Kontrolne X/6"
            pointsText.text = $"Punkty Kontrolne {counters}/6";
        }
    }
}