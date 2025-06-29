using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    [Header("Stats")]
    public float DamageMultiplier = 1f;
    public float DashCooldownMultiplier = 1f;
    public int MaxHealthBonus = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Métodos para modificar stats
    public void IncreaseDamagePercent(float percent)
    {
        DamageMultiplier += percent / 100f;
    }

    public void ReduceDashCooldownPercent(float percent)
    {
        DashCooldownMultiplier -= percent / 100f;
        DashCooldownMultiplier = Mathf.Clamp(DashCooldownMultiplier, 0.2f, 10f);
    }

    public void IncreaseMaxHealth(int amount)
    {
        MaxHealthBonus += amount;
        // Si querés también podés notificar a Health que actualice su vida
    }
}

