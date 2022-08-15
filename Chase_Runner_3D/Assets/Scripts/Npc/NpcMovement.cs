using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

[SelectionBase]
public  class NpcMovement : MovementBaseClass
{
    [SerializeField] private float forwardSpeed;
    private enum CurrentLane{ Middle, Left, Right}

    private readonly int[] _randomTurn = {-1, 1};
    private CurrentLane _lane;
    [SerializeField] private NpcStates curState;
    [SerializeField] private NpcStates[] statesArray;
    private int _curStateIndex;
    [Header("NPC Behaviour")] 
    [SerializeField] private Animator banditAnimator;
    [SerializeField] private float wanderDistance;
    [SerializeField] private float attackDistance;
    [SerializeField] private float outOfBoundDistance;
    [SerializeField] private Transform playerTarget;
    [SerializeField] private Transform banditTransform;
    [SerializeField] private SegmentManager segmentManager;
    [SerializeField] private Transform goldBarSpawnPoint;
    [SerializeField] private float timerForPickUps;
    private ObjectPooler _objectPool;
    private static readonly int ThrowBarrelAnim = Animator.StringToHash("Throw_Barrel");
    private static readonly int ThrowProjectileAnim = Animator.StringToHash("Throw_Projectile");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Win = Animator.StringToHash("Win");

    private float _curDistanceFromPlayer;
    private float _curSpeed;
    
    protected override void Start()
    {
        _objectPool=ObjectPooler.instance;
        base.Start();
        curState = statesArray[_curStateIndex];
        ControlStateBehaviour();
        _lane = CurrentLane.Right;
        XMove = 3.25f;
        EventsManager.OnNpcRun += TweenNpc;
        EventsManager.OnObstacleHit += WinNpc;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        EventsManager.OnNpcRun -= TweenNpc;
        EventsManager.OnObstacleHit -= WinNpc;
    }

    public void MoveNpc()
    {
        if (!GameManager.GameHasStarted || GameManager.PlayerHasDied) return;
        
        RubberBandingEffect();
        HandleMovement(_curSpeed);
        LookAtPlayer();
    }
   
    public void TurnNpc()
    {   
         if(_lane==CurrentLane.Middle)
         {
             var random = _randomTurn[UnityEngine.Random.Range(0, _randomTurn.Length)];
             _lane = random == 1 ? CurrentLane.Right : CurrentLane.Left;
             XMove = 3.25f * random;
             HandleRotation(random);
         } 
         else if (_lane == CurrentLane.Right)
         {
             _lane = CurrentLane.Middle;
             XMove = 0f;
             HandleRotation(-1);
         }
         else if (_lane == CurrentLane.Left)
         {
             HandleRotation(1);
             _lane = CurrentLane.Middle;
              XMove = 0f;
         }
    }

    private void RubberBandingEffect()
    {
        if (Mathf.Abs(Vector3.Distance(transform.position, playerTarget.position) - (_curDistanceFromPlayer)) > 2f )
        {
               
            if (Vector3.Distance(transform.position, playerTarget.position) > _curDistanceFromPlayer)
                _curSpeed = -forwardSpeed;
            else
                _curSpeed = forwardSpeed;
        }
        else
        {
            _curSpeed = 0;
        }
    }

    public void ChangeState()
    {
        _curStateIndex++;
        if (_curStateIndex >= statesArray.Length)
            _curStateIndex = 0;
        curState = statesArray[_curStateIndex];
        ControlStateBehaviour();
    }
    
    private void ControlStateBehaviour()
    {
        CancelInvoke();
        switch (curState)
        {
            case NpcStates.Wander:
                Wander();
                break;
            case NpcStates.Flee:
                Flee();
                break;
            case NpcStates.ThrowBarrel:
                ThrowBarrel();
                InvokeRepeating(nameof(TriggerBarrelThrow),2,2);
                break;
            case NpcStates.ThrowProjectile:
                ThrowProjectile();
                InvokeRepeating(nameof(TriggerProjectileThrow),2,2);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Wander()
    {
        banditAnimator.SetBool(Attack,false);
        SetDistanceAt(wanderDistance);
    }

    private void Flee()
    {
        banditAnimator.ResetTrigger(ThrowBarrelAnim);
        banditAnimator.ResetTrigger(ThrowProjectileAnim);
        banditAnimator.SetBool(Attack,false);
        SetDistanceAt(outOfBoundDistance);
       
    }
    
    private void ThrowProjectile()
    {
        SetDistanceAt(attackDistance);
    }

    private void ThrowBarrel()
    {
        SetDistanceAt(attackDistance);
    }

    private void SetDistanceAt(float value) => _curDistanceFromPlayer = value;

    private bool _isRotated;
    private void LookAtPlayer()
    {
        if (curState!=NpcStates.ThrowProjectile && !_isRotated)
        {
            _isRotated = true;
            banditTransform.transform.DOLocalRotate(new Vector3(0, 180, 0), .25f);
            return;
        }
        _isRotated = false;
        var position = playerTarget.position;
        var lookAtGoal = new Vector3(position.x, 
            banditTransform.transform.position.y, 
            position.z);
        banditTransform.transform.LookAt(lookAtGoal);
    }

    IEnumerator StopSpawningPickUps()
    {
        yield return new WaitForSeconds(timerForPickUps);
        CancelInvoke();
    }
    
    private void TriggerBarrelThrow()
    {
        banditAnimator.ResetTrigger(ThrowProjectileAnim);
        banditAnimator.SetBool(Attack,true);
        banditAnimator.SetTrigger(ThrowBarrelAnim);
        
    }
    private void TriggerProjectileThrow()
    {
        banditAnimator.ResetTrigger(ThrowBarrelAnim);
        banditAnimator.SetBool(Attack,true);
        banditAnimator.SetTrigger(ThrowProjectileAnim);
    }

    private void ThrowGoldBars()
    {
        var gold = _objectPool.SpawnFromPool("GoldBar", Vector3.zero, Quaternion.identity);
        gold.gameObject.SetActive(false);
        gold.transform.parent = null;
        gold.GetComponent<PickUp>().SetMoveSpeed(segmentManager.GetCurSpeed());
        gold.transform.DOMove(goldBarSpawnPoint.position, 0).OnComplete(()=>gold.gameObject.SetActive(true));
    }

    #region Event Callbacks

    private void TweenNpc()
    {
        _curSpeed = forwardSpeed;
    }

    private void WinNpc()
    {
        banditAnimator.SetTrigger(Win);
    }

    #endregion

}
