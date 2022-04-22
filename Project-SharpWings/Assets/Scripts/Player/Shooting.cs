using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Bullet bullet;
    public Bomb bomb;

    public Transform bullet_extrude_point;
    public Transform bomb_extrude_point;
    public float velocity = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // we need to get the butten pressed for ether keybaord and mouse or controller.
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bullet,bullet_extrude_point.position,Quaternion.identity);
        }

        // add the bomb section
        if(Input.GetKeyDown(KeyCode.Space)){
            Instantiate(bomb,bomb_extrude_point.position,Quaternion.identity);
        }
    }
}
