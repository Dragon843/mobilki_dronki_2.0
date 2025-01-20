using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class wygrana : MonoBehaviour { 


   public void OnTriggerEnter(Collider other) 
   {
        int countHoop = HoopManager.counter;
        if (other.CompareTag("Dron") && countHoop >= 6)
        {
            SceneManager.LoadScene("wygrales");
        }
        else if (other.CompareTag("DronBOT")) 
        {
            SceneManager.LoadScene("przegrales");
        }
   } 

}