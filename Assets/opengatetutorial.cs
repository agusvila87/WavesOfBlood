using UnityEngine;
using MoreMountains.TopDownEngine;

public class OpenGateTutorial : MonoBehaviour
{
    [Header("Enemigos que deben ser eliminados")]
    public Health[] enemigos;

    [Header("Puerta a desactivar")]
    public GameObject puerta;

    private bool _gateOpened = false;

    void Update()
    {
        if (_gateOpened) return;

        bool todosMuertos = true;

        foreach (Health enemigo in enemigos)
        {
            if (enemigo != null && enemigo.CurrentHealth > 0)
            {
                todosMuertos = false;
                break;
            }
        }

        if (todosMuertos)
        {
            puerta.SetActive(false);
            _gateOpened = true;
        }
    }
}

