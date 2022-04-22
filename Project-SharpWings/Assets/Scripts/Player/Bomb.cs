using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public Bomb bomb;

    public Vector3 dir;
    public float speed;

    public float radius = 4f;
    public float explosionForce = 700f;

    public System.Action destroy;

    public int deathTime = 1;
    public int seconds = 0;
    public float time = 0.0f;

    bool hasExploded = false;

    public GameObject explosionEffect;

    // Update is called once per frame
    void Update()
    {

        this.transform.position += this.dir * this.speed * Time.deltaTime;

        reduceTime(Time.deltaTime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(transform.tag == "Floor" || transform.tag == "Enemy"){
            Destroy(this.gameObject);
        }

        
    }


    public void reduceTime(float reduce)
    {

        time += reduce;

        seconds = (int)(time % 60);

        if (seconds > deathTime && !hasExploded)
        {
            //Destroy(this.gameObject);
            // at this point we should make the bomb exploid and do damage for sometime
            Explode();
            hasExploded = true;
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
        Destroy(this.gameObject);
    }

}
