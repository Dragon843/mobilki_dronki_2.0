using System.Collections.Generic;
using UnityEngine;

public class HoopManager : MonoBehaviour
{
    public List<GameObject> hoops; // Lista obrêczy
    private HashSet<GameObject> passedHoops = new HashSet<GameObject>(); // Zbiór pokonanych obrêczy
    public int counter = 0; // Licznik pokonanych obrêczy

    private void Start()
    {
        // Upewnij siê, ¿e lista obrêczy jest wype³niona w inspektorze
        if (hoops.Count == 0)
        {
            Debug.LogError("Nie przypisano obrêczy do HoopManager!");
        }
    }

    public void HoopPassed(GameObject hoop)
    {
        Debug.Log(hoops.Count);
        // SprawdŸ, czy obrêcz jest na liœcie i czy nie zosta³a jeszcze pokonana
        if (hoops.Contains(hoop) && !passedHoops.Contains(hoop))
        {
            passedHoops.Add(hoop); // Oznacz obrêcz jako pokonan¹
            counter++; // Zwiêksz licznik
            Debug.Log($"Przelecia³eœ przez obrêcz! Licznik: {counter}");

            // SprawdŸ, czy wszystkie obrêcze zosta³y pokonane
            if (counter >= hoops.Count)
            {
                Debug.Log("Gratulacje! Przelecia³eœ przez wszystkie obrêcze!");
            }
        }
        else
        {
            Debug.Log("Ta obrêcz zosta³a ju¿ pokonana lub nie jest na liœcie.");
        }
    }
}