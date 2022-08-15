
using DG.Tweening;
using UnityEngine;

[SelectionBase]
    public class PlayerMovement : MovementBaseClass
    {
       
        private enum CurrentLane{ Middle, Left, Right}
        private CurrentLane _lane;
        
        protected override void Start()
        {
            _lane = CurrentLane.Middle;
            EventsManager.OnGameStart += RotatePlayer;
             EventsManager.OnObstacleHit += PlayerFallDown;
            // EventsManager.OnGameWin += PlayerLost_Won;
            // EventsManager.OnGameLose += PlayerLost_Won;
            // EventsManager.OnReachedEnd += StopPlayer;
        }

        protected override void OnDisable()
        {
            EventsManager.OnGameStart -= RotatePlayer;
            EventsManager.OnObstacleHit -= PlayerFallDown;
            // EventsManager.OnGameWin -= PlayerLost_Won;
            // EventsManager.OnGameLose -= PlayerLost_Won;
            // EventsManager.OnReachedEnd -= StopPlayer;
        }


        public void MovePlayer()
        {
            if (GameManager.PlayerHasDied) return;
            GatherInput();
            HandleMovement();
           
        }
        private void GatherInput()
        {            
            if (SwipeManager.swipeLeft)
            {
                if(_lane==CurrentLane.Middle)
                {
                    _lane = CurrentLane.Left;
                    XMove = -3.25f;
                    HandleRotation(-1);
                } 
                else if (_lane == CurrentLane.Right)
                {
                    _lane = CurrentLane.Middle;
                    XMove = 0f;
                    HandleRotation(-1);
                }
            }
            if (SwipeManager.swipeRight)
            {
                if(_lane==CurrentLane.Middle)
                {
                    _lane = CurrentLane.Right;
                    XMove = 3.25f;
                    HandleRotation(1);
                } 
                else if (_lane == CurrentLane.Left)
                {
                    _lane = CurrentLane.Middle;
                    XMove = 0f;
                    HandleRotation(1);
                }
            }
            if (SwipeManager.swipeUp)
            
                EventsManager.PlayerJump();
           

            if (SwipeManager.swipeDown)
                EventsManager.PlayerRoll();
            
        }
    
        


        #region Event Callbacks

        private void PlayerFallDown()
        {
            var transform1 = transform;
            var position = transform1.localPosition;
            var localPosition = position;
            localPosition.y = 0;
            position = localPosition;
            transform1.localPosition = position;
        }

        private void RotatePlayer()
        {
            body.DOLocalRotate(Vector3.zero, .15f);
        }
        #endregion
        
    }
