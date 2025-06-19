using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MoreMountains.Tools
{
	/// <summary>
	/// Decisions are components that will be evaluated by transitions, every frame, and will return true or false. Examples include time spent in a state, distance to a target, or object detection within an area.  
	/// </summary>
	public abstract class AIDecision : MonoBehaviour
	{
		/// Decide will be performed every frame while the Brain is in a state this Decision is in. Should return true or false, which will then determine the transition's outcome.
		public abstract bool Decide();




        /// a label you can set to organize your AI Decisions, not used by anything else 
        [Tooltip("a label you can set to organize your AI Decisions, not used by anything else")]
		public string Label;
		public virtual bool DecisionInProgress { get; set; }
		protected AIBrain _brain;


        [Header("Zone Avoidance (optional)")]
        public bool AvoidDangerZone = false;
        public LayerMask DangerZoneLayer;
        public float DangerCheckDistance = 1f;


        /// <summary>
        /// On Awake we grab our Brain
        /// </summary>
        protected virtual void Awake()
		{
			_brain = this.gameObject.GetComponentInParent<AIBrain>();
		}

		/// <summary>
		/// Meant to be overridden, called when the game starts
		/// </summary>
		public virtual void Initialization()
		{

		}

		/// <summary>
		/// Meant to be overridden, called when the Brain enters a State this Decision is in
		/// </summary>
		public virtual void OnEnterState()
		{
			DecisionInProgress = true;
		}

		/// <summary>
		/// Meant to be overridden, called when the Brain exits a State this Decision is in
		/// </summary>
		public virtual void OnExitState()
		{
			DecisionInProgress = false;
		}

        protected virtual bool IsDangerAhead()
        {
            if (!AvoidDangerZone) return false;

            if (_brain.Target == null) return false;

            Vector2 direction = (_brain.Target.position - this.transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, DangerCheckDistance, DangerZoneLayer);

            return hit.collider != null;
        }

    }
}