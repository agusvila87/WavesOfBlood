using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    public GameObject upgradePanel;
    public Button option1Button;
    public Button option2Button;
    public Button option3Button;

    public TextMeshProUGUI option1Text;
    public TextMeshProUGUI option2Text;
    public TextMeshProUGUI option3Text;

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
        upgradePanel.SetActive(true);

        option1Text.text = "➕ Vida Máxima +20";
        option2Text.text = "💥 Daño +10%";
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
        ApplyUpgrade(type);
        upgradePanel.SetActive(false);

        // acá empieza el countdown de 10 segundos antes de nueva oleada
        StartCoroutine(CountdownAndStartNextWave());
    }

    private IEnumerator CountdownAndStartNextWave()
    {
        float timer = 10f;
        while (timer > 0)
        {
            GameManagerWaves.Instance.waveUI.UpdateNextWaveTimer(timer);
            timer -= Time.deltaTime;
            yield return null;
        }

        GameManagerWaves.Instance.StartCoroutine(GameManagerWaves.Instance.StartNextWave());
    }


    private void ApplyUpgrade(UpgradeType type)
    {
        switch (type)
        {
            //case UpgradeType.MoreHP:
            //    PlayerStats.Instance.IncreaseMaxHealth(20);
            //    break;
            //case UpgradeType.MoreDamage:
            //    PlayerStats.Instance.IncreaseDamagePercent(10f);
            //    break;
            //case UpgradeType.FasterDash:
            //    PlayerStats.Instance.ReduceDashCooldownPercent(20f);
            //    break;
        }
    }

    public enum UpgradeType { MoreHP, MoreDamage, FasterDash }
}
