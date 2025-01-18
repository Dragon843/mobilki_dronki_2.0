using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class wygrana : MonoBehaviour { 


   public void OnTriggerEnter(Collider other) 
   {
        if (other.CompareTag("Dron")) {
            SceneManager.LoadScene("menu");
        }

   } 

}