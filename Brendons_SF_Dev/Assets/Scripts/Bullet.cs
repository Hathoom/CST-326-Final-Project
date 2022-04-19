using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Vector3 dir;
    public float speed;

    public System.Action destroy;
   

    // Update is called once per frame
    void Update()
    {
        this.transform.position += this.dir * this.speed * Time.deltaTime;
    }

    private void OnTriggerExit(Collider other)
    {
        if (transform.tag == "Area_Around_Player")
        {
            Destroy(this.gameObject);
        }
    }
}
