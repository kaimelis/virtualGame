using UnityEngine;
using System.Collections;

public class DistractedState : AbstractEnemyState
{

    private NavMeshAgent _navMeshAgent;

    public DistractedState(EnemyAgent agent, Vector3 target) : base(agent)
    {
        _navMeshAgent = _agent.GetComponent<NavMeshAgent>();
        _navMeshAgent.SetDestination(target);
    }

    public override void Update()
    {

    }

}