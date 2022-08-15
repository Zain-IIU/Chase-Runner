using System.Collections;
using UnityEngine;


    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private Animator playerAnimator;

        #region Animation Hashing
        private static readonly int GameStart = Animator.StringToHash("StartMov");
        private static readonly int Fail = Animator.StringToHash("Fall");
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int Roll = Animator.StringToHash("Roll");
        private static readonly int Cheer = Animator.StringToHash("Cheer");
        private static readonly int Stop = Animator.StringToHash("Stop");
        private static readonly int Switch = Animator.StringToHash("Switch");
        private static readonly int Fall = Animator.StringToHash("Fall");
        private static readonly int Angry = Animator.StringToHash("Exclame");
        #endregion

        private void Start()
        {
            EventsManager.OnNpcRun += PlayStartAnim;
            EventsManager.OnGameStart += StartMoveAnim;
            EventsManager.OnPlayerJump += PlayJumpAnim;
            EventsManager.OnPlayerRoll += PlayRollAnim;
            EventsManager.OnObstacleHit += PlayFallAnim;
        }

        private void OnDisable()
        {
            EventsManager.OnNpcRun -= PlayStartAnim;
            EventsManager.OnGameStart -= StartMoveAnim;
            EventsManager.OnPlayerJump -= PlayJumpAnim;
            EventsManager.OnPlayerRoll -= PlayRollAnim;
            EventsManager.OnObstacleHit -= PlayFallAnim;

        }


        #region Event Callbacks

        private void StartMoveAnim()
        {
            playerAnimator.SetTrigger(GameStart);
        }
        
        private void PlayJumpAnim()
        {
            playerAnimator.SetTrigger(Jump);
        }

        private void PlayRollAnim()
        {
            playerAnimator.SetTrigger(Roll);
        }

        private void PlayFallAnim()
        {
            playerAnimator.SetTrigger(Fall);
            EventsManager.GameLose();
        }

        private void PlayStartAnim()
        {
            playerAnimator.SetTrigger(Angry);
        }
        private void PlayCheerAnim()
        {
            playerAnimator.SetTrigger(Cheer);
        }

        private void PlayEndAnimation()
        {
            playerAnimator.SetTrigger(Stop);
        }
        #endregion

    }
