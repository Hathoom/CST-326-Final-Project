using UnityEngine;
using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour
{
    [Header("General")]
    public float maxHealth = 100f;
    private float _health;

    [Header("UI")] private Slider _healthSlider;

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
        var targetHealth = _healthSlider.value = _health / maxHealth;
        _healthSlider.value = Mathf.Lerp(_healthSlider.value, targetHealth, Time.deltaTime * 5);
    }
    
    public void GainHealth(float health) => _health -= health;

    public void GainMaxHealth(float health)
    {
        maxHealth += health;
        GainHealth(health);
    }
    
    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            // Die
        }
    }
}
