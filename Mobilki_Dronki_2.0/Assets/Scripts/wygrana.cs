using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class wygrana : MonoBehaviour { 


   public void OnTriggerEnter(Collider other) 
   {
        if (other.CompareTag("Dron"))
        {
            SceneManager.LoadScene("wygrales");
        }
        else if (other.CompareTag("DronBOT")) 
        {
            SceneManager.LoadScene("przegrales");
        }

   } 

}