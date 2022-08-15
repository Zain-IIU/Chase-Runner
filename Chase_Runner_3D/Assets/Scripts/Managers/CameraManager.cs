using UnityEngine;

  public class CameraManager : MonoBehaviour
  {
      [SerializeField]
      private CameraElement cameraElements;


      private void Start()
        {
            EventsManager.OnGameStart += StartFollowCam;
            EventsManager.OnGameLose += EnableLoseCam;
            EventsManager.OnGameWin += EnableWinCam;
            EventsManager.OnReachedEnd += EnableEndCam;
            EventsManager.OnFinalAttack += EnableHitCam;
            EventsManager.OnEnterTunnel += EnableTunnelCam;
            EventsManager.OnExitTunnel += StartFollowCam;
        }


        #region Event Call Backs
      
        private void StartFollowCam()
        {
           cameraElements.StartFollowCam();
        }

        private void EnableLoseCam()
        {
            cameraElements.EnableLoseCam();
        }
        private void EnableWinCam()
        {
            cameraElements.EnableWinCam();
        }
        private void EnableEndCam()
        {
            cameraElements.EnableEndCam();
        }

        private void EnableHitCam()
        {
            cameraElements.EnableHitCam();
        }

        private void EnableTunnelCam()
        {
            cameraElements.EnableTunnelCam();
        }
      
        #endregion

        private void OnDisable()
        {
            EventsManager.OnGameStart -= StartFollowCam;
            EventsManager.OnGameLose -= EnableLoseCam;
            EventsManager.OnGameWin -= EnableWinCam;
            EventsManager.OnReachedEnd -= EnableEndCam;
            EventsManager.OnFinalAttack -= EnableHitCam;
            EventsManager.OnEnterTunnel -= EnableTunnelCam;
            EventsManager.OnExitTunnel -= StartFollowCam;
        }
    }
