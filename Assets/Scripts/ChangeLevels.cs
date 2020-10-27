using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevels : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1f;

    [Tooltip("Name of the scene you want to go to.")]
    public string destination;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().ResetPlayer();
            GotToNextLevel();
        }
    }

    public void GotToNextLevel(int index = 0) 
    {
        //StartCoroutine();
        if(index != 0)
        {
            //Going to specific level 
            UnityEngine.SceneManagement.SceneManager.LoadScene(index);
            return;
        }
        else if (destination == "")
        {
            destination = "MainMenu";
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(destination);
    }

    // IEnumerator LoadLevel(int levelIndex)
    // {
    //     transition.SetTrigger("Start");

    //     yield return new WaitForSeconds(transitionTime);

    //     SceneManager.LoadScene(levelIndex);
    // }
}
