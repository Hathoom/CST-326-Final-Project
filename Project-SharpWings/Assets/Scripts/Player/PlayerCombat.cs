using System;
using UnityEngine;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
        [Header("Bullets")]
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform bulletExtrudePoint;
        [SerializeField] private float bulletDamage = 1, bulletSpeed = 20, bulletLifetime = 3;
        [SerializeField] private float fireRate;
        private float _fireRateTimer;

        [Header("Bombs")]
        [SerializeField] private GameObject bombPrefab;
        [SerializeField] private Transform bombExtrudePoint;
        [SerializeField] private float explosionDamage = 1, bombSpeed = 20;
        [SerializeField] private float explosionDelay = 1, explosionRadius = 4f, explosionForce = 700f;
        [SerializeField] private int startingBombCount;
        private int _bombCount;

        [Header("Audio")]
        [SerializeField] private AudioClip shootEffect;
        [SerializeField] private AudioClip bombShootEffect;
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
            ChangeShootSound(shootEffect);
            ChangeBombSound(bombShootEffect);
            _fireRateTimer = Time.time;
            _bombCount = startingBombCount;
            _playerScore = 0;
        }

        private void Update()
        {
            // we need to get the button pressed for ether keyboard and mouse or controller.
            if (Input.GetButton("Fire1") && Time.time - _fireRateTimer > fireRate)
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

            // add the bomb section
            if(Input.GetKeyDown(KeyCode.Space) && _bombCount > 0)
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
        }

        private void AddScore(int score) => _playerScore += score;
    
        public int GetScore() => _playerScore;

        public int GetBombCount() => _bombCount;

        public int AddBombCount(int bombs) => _bombCount += bombs;
        
        private void ChangeShootSound(AudioClip audioClip) => _shootAudio.clip = audioClip;
        private void ChangeBombSound(AudioClip audioClip) => _bombAudio.clip = audioClip;

        public void UpgradeWeapon()
        {
            return;
        }
    }
}
