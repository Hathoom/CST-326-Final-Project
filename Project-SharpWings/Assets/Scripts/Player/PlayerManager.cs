using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        [Header("General")] public float maxHealth = 100f;
        [SerializeField] private GameObject scoreTransferObject;
        
        public float collisionDamage = 10f;
        private float _health;

        [Header("UI")] [SerializeField] private Slider healthSlider;
        [SerializeField] private Slider meterSlider;
        [SerializeField] private float meterFadeValue;
        [SerializeField] private TextMeshProUGUI bombCounter;
        [SerializeField] private TextMeshProUGUI scoreCounter;
        [SerializeField] private RectTransform farReticle;
        [SerializeField] private RectTransform nearReticle;
        private CanvasGroup _meterGroup;
        private RectTransform _meterTransform;

        private Camera _mainCamera;
        private PlayerMovement _playerMovement;
        private PlayerCombat _playerCombat;
        private Transform _transform;

        private void Awake()
        {
            if (healthSlider == null) Debug.LogWarning("Player health slider is unassigned!");
            if (bombCounter == null) Debug.LogWarning("Player bomb counter is unassigned!");
            if (scoreCounter == null) Debug.LogWarning("Player score counter is unassigned!");

            _playerCombat = GetComponent<PlayerCombat>();
            if (_playerCombat == null) Debug.LogWarning("Player combat system can't be found!");

            _playerMovement = GetComponent<PlayerMovement>();
            if (_playerMovement == null) Debug.LogWarning("Player movement system can't be found!");

            _mainCamera = Camera.main;
            _transform = transform;
            _meterGroup = meterSlider.GetComponent<CanvasGroup>();
            _meterTransform = meterSlider.GetComponent<RectTransform>();
        }

        private void Start()
        {
            _meterGroup.alpha = 0;
            _health = maxHealth;
        }

        private void Update()
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            // health slider
            var targetHealth = healthSlider.value = _health / maxHealth;
            healthSlider.value = Mathf.Lerp(healthSlider.value, targetHealth, Time.deltaTime * 5);

            // text
            bombCounter.text = _playerCombat.GetBombCount().ToString();
            scoreCounter.text = _playerCombat.GetScore().ToString();

            // reticle
            var forward = _transform.forward;
            var position = _transform.position;

            farReticle.position = _mainCamera.WorldToScreenPoint(forward * 100 + position);
            // var localPosition = farReticle.localPosition;
            // farReticle.localPosition = new Vector3(localPosition.x, localPosition.y, 0);

            nearReticle.position = _mainCamera.WorldToScreenPoint(forward * 10 + position);
            // var localPosition = nearReticle.localPosition;
            // nearReticle.localPosition = new Vector3(localPosition.x, localPosition.y, 0);

            // boost meter
            meterSlider.value = _playerMovement.GetMeterRatio();
            _meterTransform.position = _mainCamera.WorldToScreenPoint(position + _mainCamera.transform.right);
            var alpha = _meterGroup.alpha;
            switch (meterSlider.value)
            {
                case < 1.0f when alpha < 1:
                    _meterGroup.alpha += meterFadeValue * Time.deltaTime;
                    break;
                case >= 1.0f:
                    _meterGroup.alpha -= meterFadeValue * Time.deltaTime;
                    break;
            }

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Solid") ||
                collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                TakeDamage(collisionDamage);
            }
        }

        public void GainHealth(float health)
        {
            _health += health;
            if (_health > maxHealth) _health = maxHealth;
        }

        public void GainMaxHealth(float health)
        {
            maxHealth += health;
            _health += health;
        }

        public void TakeDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0) PlayerDeath();
        }

        private void PlayerDeath()
        {
            var sto = scoreTransferObject.GetComponent<ScoreTransfer>();
            sto.score = _playerCombat.GetScore();
            sto.waitingScene = "SaveScore";
            sto.deathState = true;
            sto.StartThing();
            SceneManager.LoadScene("SaveScore");
        }

        public void PlayerFinish()
        {
            var sto = scoreTransferObject.GetComponent<ScoreTransfer>();
            sto.score = _playerCombat.GetScore();
            sto.waitingScene = "SaveScore";
            sto.deathState = false;
            sto.StartThing();
            SceneManager.LoadScene("SaveScore");
        }
    }
}
