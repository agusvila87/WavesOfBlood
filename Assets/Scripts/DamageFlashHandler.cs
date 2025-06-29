using UnityEngine;
using UnityEngine.UI;
using MoreMountains.TopDownEngine;
using System.Collections;

public class DamageFlashPolling : MonoBehaviour
{
    [Header("Damage Flash")]
    public Image damageImage;
    public float fadeOutDuration = 0.5f;

    private Health playerHealth;
    private float lastHealth = -1f;

    private void Start()
    {
        // Buscar al jugador por tag y obtener el componente Health
        StartCoroutine(FindPlayerWithDelay());
    }

    private IEnumerator FindPlayerWithDelay()
    {
        while (playerHealth == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerHealth = player.GetComponent<Health>();
                lastHealth = playerHealth.CurrentHealth;
                damageImage.enabled = true;
                TriggerFlash();
                yield break;
            }

            yield return new WaitForSeconds(0.1f); // reintenta cada 100ms
        }
    }

    private void Update()
    {
        if (playerHealth == null || damageImage == null)
            return;

        float currentHealth = playerHealth.CurrentHealth;

        if (lastHealth < 0f)
        {
            // Primera vez que tomamos referencia
            lastHealth = currentHealth;
            return;
        }

        // Si bajó la vida, mostrar el flash
        if (currentHealth < lastHealth)
        {
            TriggerFlash();
        }

        lastHealth = currentHealth;
    }

    private void TriggerFlash()
    {
        damageImage.CrossFadeAlpha(0.5f, 0f, true);  // aparece inmediato
        damageImage.CrossFadeAlpha(0f, fadeOutDuration, false);  // se desvanece
    }
}


