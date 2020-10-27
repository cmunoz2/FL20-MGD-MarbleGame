using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevels : MonoBehaviour
{
    [Tooltip("Name of the scene you want to go to.")]
    public string destination;

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            other.GetComponent<PlayerMovement>().ResetPlayer();
            GotToNextLevel();
        }
    }

    public void GotToNextLevel(int index = 0) {
        if(index != 0){
            //Going to specific level 
            UnityEngine.SceneManagement.SceneManager.LoadScene(index);
            return;
        }
        else if (destination == ""){
            destination = "MainMenu";
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(destination);
    
    }
}
