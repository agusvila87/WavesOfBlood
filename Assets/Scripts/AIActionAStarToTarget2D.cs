using MoreMountains.TopDownEngine;
using MoreMountains.Tools;
using Pathfinding;
using UnityEngine;

[AddComponentMenu("TopDown Engine/Character/AI/Actions/AI Action A* To Target 2D")]
public class AIActionAStarToTarget2D : AIAction
{
    private AIPath _aiPath;

    public override void Initialization()
    {
        base.Initialization();
        _aiPath = GetComponent<AIPath>();
    }

    public override void PerformAction()
    {
        if (_brain.Target != null)
        {
            //Debug.Log("AStar moving to: " + _brain.Target.position);
            _aiPath.destination = _brain.Target.position;
        }
        else
        {
            Debug.LogWarning("No target assigned to AIBrain.");
        }
    }

}
