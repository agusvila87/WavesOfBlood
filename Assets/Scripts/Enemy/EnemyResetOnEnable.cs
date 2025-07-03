using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;

public class EnemyResetOnEnable : MonoBehaviour
{
    private Health _health;
    private TopDownController _controller;
    private AIBrain _brain;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _controller = GetComponent<TopDownController>(); // puede ser null
        _brain = GetComponent<AIBrain>();
    }

    private void OnEnable()
    {
        if (_health != null)
        {
            _health.Revive();
            _health.ResetHealthToMaxHealth();
        }

        //if (_controller != null)
        //{
        //    Debug.Log($"[EnemyResetOnEnable] Controlador activo en {gameObject.name}");
        //    _controller.CollisionsOn();
        //    _controller.SetKinematic(false);
        //    _controller.Reset();
        //}
        else
        {
            Debug.LogWarning($"[EnemyResetOnEnable] ⚠️ No se encontró TopDownController en {gameObject.name}. ¿Está faltando?");
        }

        if (_brain != null)
        {
            _brain.ResetBrain();
        }
    }
}

