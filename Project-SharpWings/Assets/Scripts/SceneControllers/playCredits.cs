using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playCredits : MonoBehaviour
{
    public void RollCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
