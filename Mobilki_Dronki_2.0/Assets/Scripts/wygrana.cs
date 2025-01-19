using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class wygrana : MonoBehaviour { 
    public HoopManager hoop;

   public void OnTriggerEnter(Collider other) 
   {
        if (other.CompareTag("Dron")) {
            if(hoop.counter == 1){
                SceneManager.LoadScene("menu");
            }
        }

   } 

}