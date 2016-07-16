
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : AbstractEnemyState
{
    private float _slerpSpeed = 0.1f;
    //private GameObject _gun;
    //private GameObject _bulletPrefab;
    private System.Random random;
    private int _aimDistortion = 10; //The lower, the less distortion in degrees
    private float _aimSteadiness = 0.1f; // the higher, the more inaccurate
    private int _acceptableShotRange = 10; //Max distortion (in deg) in which enemy will still shoot
    private bool _acceptableShot;
    private bool _countFireDelta;
    private float _fireDelta;
    private Transform _turret;
    private Vector3 _turretVector;
    private GameObject _muzzleFlash;
    private GameObject _bloodParticles;
    private GameObject _impactParticles;

    private bool playerAboutToDie;
    private float dieValue = 2;
    private float dieCounter;

    public AttackState(EnemyAgent agent, GameObject muzzleFlash, GameObject bloodParticles, GameObject impactParticles, GameObject turretPoint) : base(agent)
    {
        playerAboutToDie = false;
        dieCounter = 0;
        random = new System.Random();
        _muzzleFlash = muzzleFlash;
        //_gun = gun;
        //_bulletPrefab = bulletPrefab;
        _bloodParticles = bloodParticles;
        _impactParticles = impactParticles;
        _turret = turretPoint.transform;
        _turretVector = _turret.position;
    }

    public override void Update()
    {
        if (_agent.EnteredNewState)
        {
            playerAboutToDie = false;
            dieCounter = 0;
            _agent.EnteredNewState = false;
        }
        if (_agent.SeesTarget && (_agent.Target.transform.position - _agent.Parent.transform.position).magnitude < _agent.SightRange / 2)
        {
            playerAboutToDie = true;
        }
        else
        {
            dieCounter = 0;
            playerAboutToDie = false;
            _agent.SetState(typeof(ChaseState));
        }
        KillPlayer();
    }
    private void KillPlayer()
    {
        if (playerAboutToDie)
        {

            dieCounter += Time.deltaTime;
            /*if (dieCounter >= dieValue / 4 && dieCounter < dieValue/2 && !_agent.fire.isPlaying)
            {
                _agent.fire.Play();
            }*/
            if (dieCounter >= dieValue/2 && dieCounter < dieValue*1.5f && !_agent.fire.isPlaying)
            {
                _agent.fire.Play();
            }
            if (dieCounter >= dieValue*1.5f && !_agent.fire.isPlaying)
            {
                SoundManager.instance.PlaySingle(_agent.EnemyNeutralized);
                _agent.gameOver.enabled = true;
            }
        }
    }

    

    void LineOfAimHandler()
    {
        RaycastHit hit;
        if (Physics.Raycast(_agent.transform.position, _agent.Target.transform.position-_agent.Parent.transform.position, out hit))
        {
            if (!_countFireDelta) _countFireDelta = true;
            if (_fireDelta == 0)
            {
                //_agent.CreateParticlesRotated(_muzzleFlash, _turretVector, Quaternion.LookRotation(_actualAim, Vector3.up));
                if (hit.collider.gameObject.tag == "Player")
                {
                    Debug.Log("Player was hit!");
                    
                    _agent.CreateParticles(_bloodParticles, hit.point);
                }
                else
                {
                    Debug.Log("MISSED COZ I SUCK");
                    _agent.CreateParticles(_impactParticles, hit.point);
                }
            }
            if (_countFireDelta)
            {
                _fireDelta += Time.deltaTime;
            }
            if (_fireDelta >= 1f)
            {
                _fireDelta = 0;
                _countFireDelta = false;
            }
        }
        /* _turretVector = _turret.position;
         Vector3 differenceVector = _agent.Target.transform.position - _turretVector; //Vector to get length and height from
         Vector3 customVector = _agent.Parent.transform.forward; //Vector to use as a guide while aiming, has randomness
         customVector.Normalize();
         customVector *= differenceVector.magnitude;
         customVector.y = differenceVector.y;
         customVector = Quaternion.Euler(0, random.Next(-_aimDistortion, _aimDistortion + 1), random.Next(-_aimDistortion, _aimDistortion + 1)) * customVector;
         _actualAim = Vector3.Slerp(_actualAim, customVector, _aimSteadiness); //Vector that the actual shot is cast from, slerps between different customVec pos's
         _actualAim.Normalize();
         _actualAim *= differenceVector.magnitude;

         var targetAngle = Vector3.Angle(_actualAim, differenceVector);

         if (targetAngle > _acceptableShotRange)
         {

             //Debug.DrawLine(_agent.Parent.transform.position, _agent.Parent.transform.position + _actualAim, Color.red);
             Debug.DrawLine(_turretVector, _turretVector + _actualAim, Color.red);
             //Sees if Target is in acceptable range, if not, no ray is cast
         }
         else
         {

         }*/
    }

    void Shoot(RaycastHit hit)
    {
        //Debug.Log("Shoot Activated");
        
    }
}

