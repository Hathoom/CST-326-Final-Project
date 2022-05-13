using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

   public AudioClip soundClip;
   public AudioSource AS;

   public void PlayGame()
   {
      AS.clip = soundClip;
      AS.Play();
      SceneManager.LoadScene("final_level");
   }

   public void QuitGame()
   {
      Application.Quit();
   }
    
}
