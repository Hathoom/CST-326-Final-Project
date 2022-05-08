using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
        [Header("Bullets")]
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform bulletExtrudePoint;
        [SerializeField] private float bulletDamage = 1f, bulletSpeed = 50f, bulletLifetime = 3f;
        [SerializeField] private float fireRate = 5;
        private float _fireRate;
        private float _fireRateTimer;

        [Header("Bombs")]
        [SerializeField] private GameObject bombPrefab;
        [SerializeField] private Transform bombExtrudePoint;
        [SerializeField] private float explosionDamage = 1f, bombSpeed = 20f;
        [SerializeField] private float explosionDelay = 1f, explosionRadius = 4f, explosionForce = 700f;
        [SerializeField] private int startingBombCount;
        private int _bombCount;

        [Header("Upgrades")] 
        [SerializeField] private GameObject stapleBulletPrefab;
        [SerializeField] private float upgradeDamage = 2f, upgradeSpeed = 60f, upgradeLifetime = 5f;
        [SerializeField] private float upgradeFireRate = 10f;
        [SerializeField] private List<GameObject> staplers;

        [Header("Audio")] 
        [SerializeField] private AudioClip shootSound;
        [SerializeField] private AudioClip upgradeShootSound;
        [SerializeField] private AudioClip bombShootSound;
        private AudioSource _shootAudio;
        private AudioSource _bombAudio;

        private int _playerScore;

        private void Awake()
        {
            _shootAudio = bulletExtrudePoint.GetComponent<AudioSource>();
            _bombAudio = bombExtrudePoint.GetComponent<AudioSource>();
        }

        private void Start()
        {
            _fireRate = 1 / fireRate;
            ChangeShootSound(shootSound);
            ChangeBombSound(bombShootSound);
            _fireRateTimer = Time.time;
            _bombCount = startingBombCount;
            _playerScore = 0;
        }

        private void Update()
        {
            if (Input.GetButton("Fire1") && Time.time - _fireRateTimer > _fireRate)
            {
                SpawnBullet();
            }

            if(Input.GetKeyDown(KeyCode.Space) && _bombCount > 0)
            {
                SpawnBomb();
            }
        }

        #region FIRING

        private void SpawnBullet()
        {
            _fireRateTimer = Time.time;
            var bullet = Instantiate(bulletPrefab, bulletExtrudePoint.position, 
                transform.rotation * bulletPrefab.transform.rotation).GetComponent<Bullet>();
            bullet.speed = bulletSpeed;
            bullet.damage = bulletDamage;
            bullet.lifeTime = bulletLifetime;
            bullet.direction = transform.forward;
            bullet.OnEnemyDeath += AddScore;
            _shootAudio.Play();
        }
        
        
        private void ChangeShootSound(AudioClip audioClip) => _shootAudio.clip = audioClip;
        
        #endregion
        
        #region BOMBS
        
        private void SpawnBomb()
        {
            _bombCount--;
            var bomb = Instantiate(bombPrefab, bombExtrudePoint.position, Quaternion.identity).GetComponent<Bomb>();
            bomb.speed = bombSpeed;
            bomb.damage = explosionDamage;
            bomb.force = explosionForce;
            bomb.delay = explosionDelay;
            bomb.radius = explosionRadius;
            bomb.direction = transform.forward;
            bomb.OnEnemyDeath += AddScore;
            _bombAudio.Play();
            
        }
        
        public int GetBombCount() => _bombCount;

        public int AddBombCount(int bombs) => _bombCount += bombs;
        
        private void ChangeBombSound(AudioClip audioClip) => _bombAudio.clip = audioClip;

        #endregion
        
        private void AddScore(int score) => _playerScore += score;
    
        public int GetScore() => _playerScore;

        public void UpgradeWeapon()
        {
            bulletPrefab = stapleBulletPrefab;
            bulletDamage = upgradeDamage;
            bulletSpeed = upgradeSpeed;
            bulletLifetime = upgradeLifetime;
            _fireRate = 1 / upgradeFireRate;
            ChangeShootSound(upgradeShootSound);
            foreach (var stapler in staplers)
            {
                stapler.SetActive(true);
            }
        }
    }
}
