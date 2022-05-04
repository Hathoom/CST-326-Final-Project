using System;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Vector3 dir;
    public float speed;

    public float radius = 4f;
    public float explosionForce = 700f;

    public int deathTime = 1;
    private float _deathTimer;

    private bool _hasExploded;

    public GameObject explosionEffect;

    private void Start()
    {
        _deathTimer = Time.time;
    }

    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;

        if (Time.time - _deathTimer > deathTime)
        {
            Explode();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Enemy")){
            Destroy(gameObject);
        }
    }

    void Explode()
    {
        //show effect
        Instantiate(explosionEffect, transform.position, transform.rotation);

        // get nearby objects
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            //add force
            
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position,radius);
            }

            //Damage
        }

        //remove bomb
        Destroy(gameObject);
    }

}
