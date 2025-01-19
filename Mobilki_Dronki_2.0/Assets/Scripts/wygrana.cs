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