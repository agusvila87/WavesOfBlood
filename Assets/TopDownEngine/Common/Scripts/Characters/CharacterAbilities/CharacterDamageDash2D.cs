using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.TopDownEngine;

namespace MoreMountains.TopDownEngine
{
    [AddComponentMenu("TopDown Engine/Character/Abilities/Character Damage Dash 2D")]
    public class CharacterDamageDash2D : CharacterDash2D
    {
        [Header("Damage Dash")]
        [Tooltip("Hitbox de daño que se activa durante el dash (requiere un Collider2D con trigger + DamageOnTouch)")]
        public DamageOnTouch TargetDamageOnTouch;

        [Header("Dash UI Indicator")]
        [Tooltip("Imagen tipo 'Filled' que muestra el progreso del cooldown del dash")]
        public Image DashCooldownImage;

        /// <summary>
        /// Al inicializar, se desactiva el daño y se llena la barra.
        /// </summary>
        protected override void Initialization()
        {
            base.Initialization();

            if (TargetDamageOnTouch != null)
            {
                TargetDamageOnTouch.gameObject.SetActive(false);
            }

            if (DashCooldownImage != null)
            {
                DashCooldownImage.fillAmount = 0f;
            }
        }

        /// <summary>
        /// Al comenzar el dash, se activa el daño y se vacía la barra.
        /// </summary>
        public override void DashStart()
        {
            base.DashStart();

            if (TargetDamageOnTouch != null)
            {
                TargetDamageOnTouch.gameObject.SetActive(true);
            }

            if (DashCooldownImage != null)
            {
                DashCooldownImage.fillAmount = 0f;
            }
        }

        /// <summary>
        /// Al terminar el dash, se desactiva el daño.
        /// </summary>
        public override void DashStop()
        {
            base.DashStop();

            if (TargetDamageOnTouch != null)
            {
                TargetDamageOnTouch.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Actualiza el progreso del cooldown visual en la imagen tipo Filled.
        /// </summary>
        public override void ProcessAbility()
        {
            base.ProcessAbility();

            if (DashCooldownImage != null)
            {
                if (!Cooldown.Ready())
                {
                    float remaining = Cooldown.CurrentDurationLeft;
                    float total = Cooldown.ConsumptionDuration;
                    DashCooldownImage.fillAmount = 1f - Mathf.Clamp01(remaining / total);
                }
                else
                {
                    DashCooldownImage.fillAmount = 1f;
                }
            }
        }
    }
}
