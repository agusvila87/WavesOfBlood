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
        _controller = GetComponent<TopDownController>();
        _brain = GetComponent<AIBrain>();
    }

    private void OnEnable()
    {
        if (_health != null)
        {
            _health.Revive();
            _health.ResetHealthToMaxHealth();
        }

        if (_controller != null)
        {
            _controller.CollisionsOn();
            _controller.SetKinematic(false);
            _controller.Reset();
        }

        if (_brain != null)
        {
            _brain.ResetBrain();
        }
    }
}
