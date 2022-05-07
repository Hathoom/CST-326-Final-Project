using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        [Header("General")]
        public float maxHealth = 100f;
        private float _health;

        [Header("UI")] 
        [SerializeField] private Slider healthSlider;
        [SerializeField] private TextMeshProUGUI bombCounter;
        [SerializeField] private TextMeshProUGUI scoreCounter;

        private PlayerCombat _playerCombat;

        private void Awake()
        {
            if (healthSlider == null) Debug.LogWarning("Player health slider is unassigned!");
            if (bombCounter == null) Debug.LogWarning("Player bomb counter is unassigned!");
            if (scoreCounter == null) Debug.LogWarning("Player score counter is unassigned!");
            _playerCombat = GetComponent<PlayerCombat>();
            if (_playerCombat == null) Debug.LogWarning("Player combat system can't be found!");
        }

        private void Start()
        {
            _health = maxHealth;
        }

        private void Update()
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            var targetHealth = healthSlider.value = _health / maxHealth;
            healthSlider.value = Mathf.Lerp(healthSlider.value, targetHealth, Time.deltaTime * 5);
        
            bombCounter.text = _playerCombat.GetBombCount().ToString();
            scoreCounter.text = _playerCombat.GetScore().ToString();
        }
    
        // public void GainHealth(float health) => _health -= health;

        // public void GainMaxHealth(float health)
        // {
        //     maxHealth += health;
        //     _health += health;
        // }
    
        public void TakeDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                // Die
                Destroy(gameObject);
            }
        }
    }
}
