using System.Collections;
using Player;
using UnityEngine;

namespace Power_Up
{
    public class HealthPickup : MonoBehaviour
    {
        [SerializeField] private float healthGain;
        [SerializeField] private bool fullHeal;

        private AudioSource _audio;
        private void Awake() => _audio = GetComponent<AudioSource>();

        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<PlayerManager>();
            if (player == null) return;
            var health = fullHeal ? player.maxHealth : healthGain;
            _audio.Play();
            GetComponent<Collider>().enabled = false;
            foreach (var render in GetComponentsInChildren<Renderer>())
            {
                render.enabled = false;
            }
            player.GainHealth(health);
            StartCoroutine(PickupEffect());
        }

        private IEnumerator PickupEffect()
        {
            while (_audio.isPlaying) yield return null;
            Destroy(gameObject);
        }
    }
}
