using System.Collections.Generic;
using UnityEngine;

public class HoopManager : MonoBehaviour
{
    public List<GameObject> hoops; // Lista obr�czy
    private HashSet<GameObject> passedHoops = new HashSet<GameObject>(); // Zbi�r pokonanych obr�czy
    public int counter = 0; // Licznik pokonanych obr�czy

    private void Start()
    {
        // Upewnij si�, �e lista obr�czy jest wype�niona w inspektorze
        if (hoops.Count == 0)
        {
            Debug.LogError("Nie przypisano obr�czy do HoopManager!");
        }
    }

    public void HoopPassed(GameObject hoop)
    {
        Debug.Log(hoops.Count);
        // Sprawd�, czy obr�cz jest na li�cie i czy nie zosta�a jeszcze pokonana
        if (hoops.Contains(hoop) && !passedHoops.Contains(hoop))
        {
            passedHoops.Add(hoop); // Oznacz obr�cz jako pokonan�
            counter++; // Zwi�ksz licznik
            Debug.Log($"Przelecia�e� przez obr�cz! Licznik: {counter}");

            // Sprawd�, czy wszystkie obr�cze zosta�y pokonane
            if (counter >= hoops.Count)
            {
                Debug.Log("Gratulacje! Przelecia�e� przez wszystkie obr�cze!");
            }
        }
        else
        {
            Debug.Log("Ta obr�cz zosta�a ju� pokonana lub nie jest na li�cie.");
        }
    }
}