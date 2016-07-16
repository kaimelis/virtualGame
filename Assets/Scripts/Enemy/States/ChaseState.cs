using System;
using UnityEngine;

public class ChaseState : AbstractEnemyState
{
    private float _chaseSpeed = 0.01f;
    private float _slerpSpeed = 0.1f;
    private float _timeSinceStartChase;
    private Vector3 _oldPos = Vector3.zero;
    private Vector2 _targetXZ;
    private Vector2 _parentXZ;

    public ChaseState(EnemyAgent agent) : base(agent)
    {

    }

    public override void Update()
    {
        if (_agent.EnteredNewState)
        {
            _agent.NavAgent.destination = _agent.LastSeenTargetPosition;
            _targetXZ = new Vector2(_agent.LastSeenTargetPosition.x, _agent.LastSeenTargetPosition.z);
            _parentXZ = new Vector2(_agent.Parent.transform.position.x, _agent.Parent.transform.position.z);
            _agent.NavAgent.Resume();
            _timeSinceStartChase = 0;
            _agent.EnteredNewState = false;
        }
        if (_agent.SeesTarget && (_agent.Target.transform.position - _agent.Parent.transform.position).magnitude < _agent.SightRange / 2)
        {
            SoundManager.instance.PlaySingle(_agent.EnteringCombatMode);
            _agent.SetState(typeof(AttackState));
        }
        else if (_agent.SeesTarget && (_agent.Target.transform.position - _agent.Parent.transform.position).magnitude > _agent.SightRange / 2)
        {
            return; 
        }
        else if (!_agent.SeesTarget)
        {
            _parentXZ.Set(_agent.Parent.transform.position.x, _agent.Parent.transform.position.z);
            _targetXZ.Set(_agent.LastSeenTargetPosition.x, _agent.LastSeenTargetPosition.z);
          //  Debug.Log("Distance to Target: " + Vector2.Distance(_targetXZ, _parentXZ));
            if(Vector2.Distance(_targetXZ, _parentXZ) < 0.01f)
            {
                SoundManager.instance.PlaySingle(_agent.TargetHasBeenLost);
                _agent.Parent.position = _agent.LastSeenTargetPosition;
                _agent.SetState(typeof(LookoutState));
            }
            else if (_oldPos == _agent.Parent.transform.position && _timeSinceStartChase > 1.0f)
            {
                //Debug.Log("Entering Return State");
                SoundManager.instance.PlaySingle(_agent.TargetHasBeenLost);
                _agent.SetState(typeof(ReturnState));
            }
        }
        _oldPos = _agent.Parent.transform.position;
        _timeSinceStartChase += Time.deltaTime;
        //Debug.Log(_timeSinceStartChase);
    }
}
