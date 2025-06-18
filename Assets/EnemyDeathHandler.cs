using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;


public class EnemyDeathHandler : MonoBehaviour
{
    private Health _health;

    [Header("Drop Settings")]
    public GameObject ammoTypeA;
    public GameObject ammoTypeB;
    [Range(0f, 1f)] public float dropChance = 0.3f; // 30% por defecto

    private void Awake()
    {
        _health = GetComponent<Health>();
        if (_health != null)
        {
            _health.OnDeath += OnEnemyDeath;
        }
    }

    private void OnEnemyDeath()
    {
        if (GameManagerWaves.Instance != null)
        {
            GameManagerWaves.Instance.EnemyDied();

            TryDropAmmo(GameManagerWaves.Instance.currentWave);
        }

        StartCoroutine(DisableAfterDelay(0.25f));
    }

    private void TryDropAmmo(int wave)
    {
        if (wave < 5) return; // solo a partir de la ronda 5

        if (Random.value <= dropChance)
        {
            // Elegimos entre tipo A o B aleatoriamente
            GameObject ammoPrefab = (Random.value < 0.5f) ? ammoTypeA : ammoTypeB;

            // Instanciamos la munición en la posición del enemigo
            Instantiate(ammoPrefab, transform.position, Quaternion.identity);
        }
    }

    private IEnumerator DisableAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (_health != null)
        {
            _health.OnDeath -= OnEnemyDeath;
        }
    }
}


