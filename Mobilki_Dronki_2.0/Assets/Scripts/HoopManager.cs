using System.Collections.Generic;
using UnityEngine;

public class HoopManager : MonoBehaviour
{
    public List<GameObject> hoops; // Lista obr�czy
    private HashSet<GameObject> passedHoops = new HashSet<GameObject>(); // Zbi�r pokonanych obr�czy
    public static int counter = 0; // Licznik pokonanych obr�czy


    private void Start(){ if (hoops.Count == 0){ Debug.LogError("Nie przypisano obr�czy do HoopManager!");}}

    public void HoopPassed(GameObject hoop)
    {
        // Sprawd�, czy obr�cz jest na li�cie i czy nie zosta�a jeszcze pokonana
        if (hoops.Contains(hoop) && !passedHoops.Contains(hoop))
        {
            passedHoops.Add(hoop); // Oznacz obr�cz jako pokonan�
            counter++; // Zwi�ksz licznik
        }
    }
}