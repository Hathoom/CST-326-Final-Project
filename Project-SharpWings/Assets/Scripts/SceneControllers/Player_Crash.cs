using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Crash : MonoBehaviour
{
    [HideInInspector] public float damage= 10f;
    
    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.gameObject.GetComponent<PlayerManager>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
        
    }

    
}
