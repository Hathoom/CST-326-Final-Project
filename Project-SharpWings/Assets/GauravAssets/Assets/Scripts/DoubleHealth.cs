using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleHealth : MonoBehaviour
{
    PlayerMovement player;
    public GameObject sphere;
    public GameObject pickUpEffect;
    public HealthBarScript healthBar;

    private void Awake()
    {
        player = sphere.GetComponent<PlayerMovement>();
        Debug.Log("Initial Health: " + player);
        healthBar.setMaxHealth(player.health);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.health *= 2;
            healthBar.setMaxHealth(player.health);
            //healthBar.setHealth(player.health);
            Instantiate(pickUpEffect, transform.position, transform.rotation);
            Debug.Log("New Health Value: " + player.health);
            Destroy(gameObject);
            
        }
    }
}
