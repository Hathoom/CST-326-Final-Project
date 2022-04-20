using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public Bomb bomb;

    public Vector3 dir;
    public float speed;

    public System.Action destroy;
 

    // Update is called once per frame
    void Update()
    {

        this.transform.position += this.dir * this.speed * Time.deltaTime;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(transform.tag == "Floor" || transform.tag == "Enemy"){
            Destroy(this.gameObject);
        }
    }
}
