using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBackgroundMusic : MonoBehaviour
{
    AudioSource aud;
    public List<AudioClip> songs;
    [Tooltip("What Levels start a new world?")]
    public int World1Start, World2Start, World3Start, World4Start;
    // Start is called before the first frame update
    void Start()
    {
        aud = this.GetComponent<AudioSource>();
        CheckForNewWorld();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Should be called by ChangeLevel script or PlayerMovement script
    public void CheckForNewWorld()
    {
        int currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        switch(currentScene)
        {  
            case 0:
            case 1:
            case 2:
            case 3:
            case 4: aud.clip = songs[0];
                    aud.Play(); 
                    break;

            case 5:
            case 6:
            case 7: aud.clip = songs[1];
                    aud.Play(); 
                    break;

            case 8: 
            case 9:
            case 10: 
            case 11: aud.clip = songs[2];
                     aud.Play();
                     break;

            case 12:aud.clip = songs[3];
                     aud.Play();
                     break;
            case 13: 

            default: Debug.Log("You're in a strange world.");
                     break;
        }
    }

    public void CheckForNewWorld2()
    {
        int currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        if(currentScene == World1Start)
        {
            aud.clip = songs[0];
            aud.Play();
        }
        if(currentScene == World2Start)
        {
            aud.clip = songs[1];
            aud.Play();
        }
        if(currentScene == World3Start)
        {
            aud.clip = songs[2];
            aud.Play();
        }
        if(currentScene == World4Start)
        {
            aud.clip = songs[3];
            aud.Play();
        }
    }
}
