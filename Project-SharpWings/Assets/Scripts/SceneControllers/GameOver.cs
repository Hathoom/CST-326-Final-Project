using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    
    public AudioClip soundClip;
    public AudioSource AS;
    
    public void Retry()
    {
        AS.clip = soundClip;
        AS.Play();
        SceneManager.LoadScene("final_level");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
