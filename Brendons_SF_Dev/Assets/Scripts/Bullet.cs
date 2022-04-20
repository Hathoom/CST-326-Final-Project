using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Vector3 dir;
    public float speed;

    public System.Action destroy;

    public int deathTime = 1;
    public int seconds = 0;
    public float time = 0.0f;
   

    // Update is called once per frame
    void Update()
    {
        this.transform.position += this.dir * this.speed * Time.deltaTime;

        reduceTime(Time.deltaTime);


    }

    private void OnTriggerExit(Collider other)
    {
        if (transform.tag == "Area_Around_Player")
        {
            Destroy(this.gameObject);
        }
    }

    public void reduceTime(float reduce){

        time += reduce;

        seconds = (int)(time % 60);

        if(seconds > deathTime )
        {
            Destroy(this.gameObject);
        }

    }
}
