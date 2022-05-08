using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesScript : MonoBehaviour
{
    public TMP_Text lives;
    private int amount = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        lives.SetText("Lives: " + amount);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            amount += 1;
            lives.SetText("Lives: " + amount);
        }
    }
}
