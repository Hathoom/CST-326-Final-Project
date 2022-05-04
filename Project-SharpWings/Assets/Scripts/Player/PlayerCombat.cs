using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("General")]
    public Transform bulletExtrudePoint;
    public Transform bombExtrudePoint;

    [Header("Bullets")]
    public GameObject bulletPrefab;
    public float bulletDamage, bulletSpeed;
    public float fireRate;
    private float _fireRateTimer;

    [Header("Bombs")]
    public GameObject bomb;
    [SerializeField] private int startingBombCount;
    private int _bombCount;
    
    private void Start()
    {
        _fireRateTimer = Time.time;
    }

    void Update()
    {
        // we need to get the button pressed for ether keyboard and mouse or controller.
        if (Input.GetButtonDown("Fire1") && Time.time - _fireRateTimer > fireRate)
        {
            _fireRateTimer = Time.time;
            var bullet = Instantiate(bulletPrefab, bulletExtrudePoint.position, Quaternion.identity).GetComponent<Bullet>();
            bullet.speed = bulletSpeed;
            bullet.damage = bulletDamage;
        }

        // add the bomb section
        if(Input.GetKeyDown(KeyCode.Space)){
            Instantiate(bomb, bombExtrudePoint.position, Quaternion.identity);
        }
    }
}
