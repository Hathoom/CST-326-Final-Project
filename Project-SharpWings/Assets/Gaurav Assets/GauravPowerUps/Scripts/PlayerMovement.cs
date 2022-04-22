using UnityEngine;

namespace GauravPowerUps.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody rb;

        public int health = 3;
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
        }
    }
}
