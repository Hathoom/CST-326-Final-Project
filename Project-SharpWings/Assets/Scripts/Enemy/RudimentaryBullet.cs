using UnityEngine;

public class RudimentaryBullet : MonoBehaviour
{
    public float speed;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward  * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        Destroy(gameObject);
    }
}
