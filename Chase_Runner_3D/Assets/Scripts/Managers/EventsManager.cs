using System;
using UnityEngine;


public class EventsManager : MonoBehaviour
{
    public static event Action OnGameStart;
    public static event Action OnNpcRun;
    
    public static event Action OnGameWin;
    public static event Action OnGameLose;
    public static event Action OnEnterTunnel;
    public static event Action OnExitTunnel;
    public static event Action OnSegmentSpeedIncrease;
    public static event Action<int> OnCarTrigger;
    public static event Action OnCoinPickUp; 
    public static event Action OnReachedEnd;
    public static event Action OnPlayerJump;
    public static event Action OnPlayerRoll;
    public static event Action OnFinalAttack;
    
    public static event Action OnObstacleHit;


    #region Haptic Callbacks

    // public void Haptic(HapticTypes type)
    // {
    //     MMVibrationManager.Haptic(type, false,true, this);
    //   
    // }

    #endregion



    public static void GameStart()
    {
        OnGameStart?.Invoke();
    }
    
    public static void GameWin()
    {
        OnGameWin?.Invoke();
    }

   

    public static void GameLose()
    {
        OnGameLose?.Invoke();
    }

   
   
  
  

    public static void PlayerReachedEnd()
    {
        OnReachedEnd?.Invoke();
    }

    public static void FinalAttack()
    {
        OnFinalAttack?.Invoke();    
    }

  

    public static void PlayerJump()
    {
        OnPlayerJump?.Invoke();
    }

    public static void PlayerRoll()
    {
        OnPlayerRoll?.Invoke();
    }

    public static void ObstacleHit()
    {
        GameManager.PlayerHasDied = true;
        OnObstacleHit?.Invoke();
    }

    public static void EnterTunnel()
    {
        OnEnterTunnel?.Invoke();
    }

    public static void ExitTunnel()
    {
        OnExitTunnel?.Invoke();
    }

    public static void NpcStartRun()
    {
        OnNpcRun?.Invoke();
    }

    public static void SegmentSpeedIncrease()
    {
        OnSegmentSpeedIncrease?.Invoke();
    }

    public static void CarTrigger(int obj)
    {
        OnCarTrigger?.Invoke(obj);
    }

    public static void CoinPickUp()
    {
        OnCoinPickUp?.Invoke();
    }
}

