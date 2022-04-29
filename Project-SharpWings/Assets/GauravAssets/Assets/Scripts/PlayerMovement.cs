using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    
            public int health = 3;

            public BoostBarScript boostBar;
            
            // Start is called before the first frame update
            void Start()
            {
                rb = GetComponent<Rigidbody>();
                Debug.Log(health);
            }
    
            // Update is called once per frame
            void Update()
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    transform.Translate(0, 0, 10 * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    transform.Translate(0, 0, -10 * Time.deltaTime);
                }
    
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
                    
                }

                if (Input.GetKey(KeyCode.A))
                {
                    boostBar.useStamina(0.02f);
                }
                
            }
}
