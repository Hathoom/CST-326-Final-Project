using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Crash : MonoBehaviour
{

    [HideInInspector] public float damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        var player = collision.gameObject.GetComponent<PlayerManager>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
        
    }
}
