using System.Collections.Generic;
using UnityEngine;

public class HoopManager : MonoBehaviour
{
    public List<GameObject> hoops; // Lista obrêczy
    private HashSet<GameObject> passedHoops = new HashSet<GameObject>(); // Zbiór pokonanych obrêczy
    public static int counter = 0; // Licznik pokonanych obrêczy


    private void Start(){ if (hoops.Count == 0){ Debug.LogError("Nie przypisano obrêczy do HoopManager!");}}

    public void HoopPassed(GameObject hoop)
    {
        // SprawdŸ, czy obrêcz jest na liœcie i czy nie zosta³a jeszcze pokonana
        if (hoops.Contains(hoop) && !passedHoops.Contains(hoop))
        {
            passedHoops.Add(hoop); // Oznacz obrêcz jako pokonan¹
            counter++; // Zwiêksz licznik
        }
    }
}