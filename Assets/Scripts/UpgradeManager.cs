using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MoreMountains.TopDownEngine;
using UnityEngine.EventSystems;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    [Header("UI")]
    public GameObject upgradePanel;
    public Button option1Button;
    public Button option2Button;
    public Button option3Button;
    public Button confirmButton;
    public TextMeshProUGUI option1Text;
    public TextMeshProUGUI option2Text;
    public TextMeshProUGUI option3Text;
    public TextMeshProUGUI confirmButtonText;

    public PlayerStats playerStats;

    private UpgradeType? selectedUpgrade = null;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        upgradePanel.SetActive(false);
        confirmButton.interactable = false;
    }

    public void ShowUpgradeOptions()
    {
        GameManagerWaves.Instance.CleanupHealthPickups();
        GameManager.Instance.PauseConfirm();
        upgradePanel.SetActive(true);
        selectedUpgrade = null;
        confirmButton.interactable = false;

        // Textos de opciones
        option1Text.text = "Vida Máxima +20";
        option2Text.text = "Daño +10%";
        option3Text.text = "Dash Cooldown -20%";

        // Limpiar listeners previos
        option1Button.onClick.RemoveAllListeners();
        option2Button.onClick.RemoveAllListeners();
        option3Button.onClick.RemoveAllListeners();
        confirmButton.onClick.RemoveAllListeners();

        // Agregar listeners
        option1Button.onClick.AddListener(() => SetSelectedUpgrade(UpgradeType.MoreHP));
        option2Button.onClick.AddListener(() => SetSelectedUpgrade(UpgradeType.MoreDamage));
        option3Button.onClick.AddListener(() => SetSelectedUpgrade(UpgradeType.FasterDash));
        confirmButton.onClick.AddListener(() => ConfirmUpgrade());
    }

    private void SetSelectedUpgrade(UpgradeType type)
    {
        selectedUpgrade = type;
        confirmButton.interactable = true;
        confirmButtonText.text = $"Opción seleccionada: {type}";
        // Opcional: feedback visual
        Debug.Log($"Opción seleccionada: {type}");
    }

    private void ConfirmUpgrade()
    {
        if (!selectedUpgrade.HasValue) return;

        SelectUpgrade(selectedUpgrade.Value);
        upgradePanel.SetActive(false);
        GameManager.Instance.UnPause();
        GameManagerWaves.Instance.SpawnHealthPickups();
        GameManagerWaves.Instance.StartCoroutine(GameManagerWaves.Instance.StartNextWave());
    }

    private void SelectUpgrade(UpgradeType type)
    {
        Debug.Log($"Upgrade confirmada: {type}");
        ApplyUpgrade(type);
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
                    weapon.DamageMultiplier += 0.10f;
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
