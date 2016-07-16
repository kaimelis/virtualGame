using UnityEngine;
using System.Collections;

public class ParalyzedState : AbstractEnemyState
{
    private NavMeshAgent _navMeshAgent;
    public int paralyzedTime = 5;
    private float paralyzedTimer;
    public ParalyzedState(EnemyAgent agent) : base(agent)
    {
        _navMeshAgent = _agent.GetComponent<NavMeshAgent>();
        _navMeshAgent.Stop();
        Debug.Log("Paralyzed");
    }

    public override void Update()
    {
        paralyzedTimer += Time.deltaTime;
        if (paralyzedTimer >= paralyzedTime)
        {
            paralyzedTimer = 0;
            _navMeshAgent.Resume();
            _agent.UnParalyze();
        }
    }
}