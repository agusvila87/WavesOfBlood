using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MoreMountains.TopDownEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    [Header("UI")]
    public GameObject upgradePanel;
    public Button option1Button;
    public Button option2Button;
    public Button option3Button;
    public TextMeshProUGUI option1Text;
    public TextMeshProUGUI option2Text;
    public TextMeshProUGUI option3Text;

    public PlayerStats playerStats;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        upgradePanel.SetActive(false);
    }

    public void ShowUpgradeOptions()
    {
        GameManagerWaves.Instance.CleanupHealthPickups();
        Time.timeScale = 0f; // pausa el juego durante la elección
        upgradePanel.SetActive(true);

        option1Text.text = "➕ Vida Máxima +20";
        option2Text.text = "💥 Daño +25%";
        option3Text.text = "⚡ Dash Cooldown -20%";

        option1Button.onClick.RemoveAllListeners();
        option2Button.onClick.RemoveAllListeners();
        option3Button.onClick.RemoveAllListeners();

        option1Button.onClick.AddListener(() => SelectUpgrade(UpgradeType.MoreHP));
        option2Button.onClick.AddListener(() => SelectUpgrade(UpgradeType.MoreDamage));
        option3Button.onClick.AddListener(() => SelectUpgrade(UpgradeType.FasterDash));
    }

    private void SelectUpgrade(UpgradeType type)
    {
        Debug.Log($"Upgrade seleccionada: {type}");
        ApplyUpgrade(type);
        upgradePanel.SetActive(false);
        Time.timeScale = 1f;

        GameManagerWaves.Instance.SpawnHealthPickups(); // 🟢 Spawnea pickups ahora

        GameManagerWaves.Instance.StartCoroutine(GameManagerWaves.Instance.StartNextWave());
    }



    private void ApplyUpgrade(UpgradeType type)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        switch (type)
        {
            case UpgradeType.MoreHP:
                var health = player.GetComponent<Health>();
                if (health != null)
                {
                    health.MaximumHealth += 20f;
                    health.CurrentHealth = Mathf.Min(health.CurrentHealth + 20f, health.MaximumHealth);
                }
                break;

            case UpgradeType.MoreDamage:
                var weapons = player.GetComponentsInChildren<Weapon>();
                foreach (var weapon in weapons)
                {
                    weapon.DamageMultiplier += 0.25f;
                    weapon.DamageMultiplier = Mathf.Clamp(weapon.DamageMultiplier, 1f, 5f);
                }
                break;

            case UpgradeType.FasterDash:
                var dash = player.GetComponent<CharacterDash2D>();
                if (dash != null)
                {
                    dash.Cooldown.ConsumptionDuration *= 0.8f;
                    dash.Cooldown.ConsumptionDuration = Mathf.Max(dash.Cooldown.ConsumptionDuration, 0.2f);
                }
                break;

        }
    }

    public enum UpgradeType
    {
        MoreHP,
        MoreDamage,
        FasterDash
    }
}
