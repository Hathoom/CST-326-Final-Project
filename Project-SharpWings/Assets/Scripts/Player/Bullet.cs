using Enemy;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Vector3 dir;
    public float speed;
    public float damage;

    public float deathTime = 1;
    private float _seconds;
    private float _deathTimer;

    private void Start()
    {
        _deathTimer = Time.time;
    }

    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;

        if (Time.time - _deathTimer > deathTime) Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var enemy = collision.gameObject.GetComponent<IEnemy>();
        enemy?.TakeDamage(damage);
        Destroy(gameObject);
    }
}
