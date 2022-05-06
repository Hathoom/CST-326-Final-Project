using Enemy;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public float damage;
    public float radius;
    public float force;
    public float delay;
    
    private float _explosionTimer;

    public GameObject explosionEffect;

    public delegate void EnemyDeath(int score);
    public event EnemyDeath OnEnemyDeath;
    
    private void Start()
    {
        _explosionTimer = Time.time;
    }

    private void Update()
    {
        transform.position += direction * (speed * Time.deltaTime);

        if (Time.time - _explosionTimer > delay)
        {
            Explode();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    private void Explode()
    {
        //show effect
        Instantiate(explosionEffect, transform);

        // get nearby objects
        var colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var nearbyObject in colliders)
        {
            //add force
            
            var rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }

            //Damage
            var enemy = nearbyObject.GetComponent<IEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                if (enemy.GetHealth() <= 0) OnEnemyDeath?.Invoke(enemy.GetScore());
            }
        }

        //remove bomb
        Destroy(gameObject);
    }

}
