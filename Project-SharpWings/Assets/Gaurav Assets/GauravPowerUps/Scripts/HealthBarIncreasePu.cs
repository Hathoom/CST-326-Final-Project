using System;
using System.Collections;
using System.Collections.Generic;
using GauravPowerUps.Scripts;
using UnityEngine;


public class HealthBarIncreasePu : MonoBehaviour
{
    
    PlayerMovement player;
    public GameObject sphere;
    public GameObject pickUpEffect;

    private void Awake()
    {
        player = sphere.GetComponent<PlayerMovement>();
        Debug.Log("Initial Health: " + player);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.health *= 2;
            Instantiate(pickUpEffect, transform.position, transform.rotation);
            Debug.Log("New Health Value: " + player.health);
            Destroy(gameObject);
        }
    }
}
