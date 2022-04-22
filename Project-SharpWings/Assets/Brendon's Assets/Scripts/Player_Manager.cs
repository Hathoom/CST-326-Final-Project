using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{

    public float health = 100.0f;

    public int bombCount = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        // Informaton on how the player starts.
        Debug.Log(health);
        Debug.Log("Current Bomb count:  " + bombCount);
        Debug.Log("collect different objects in the field to game more bombs");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
