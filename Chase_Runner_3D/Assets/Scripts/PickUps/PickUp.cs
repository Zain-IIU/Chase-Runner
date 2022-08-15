using UnityEngine;

    public class PickUp : MonoBehaviour
    {
        
        private float _curSpeed;

        private void Start()
        {
            EventsManager.OnGameLose += StopMoving;
        }

        private void OnDisable()
        {
            EventsManager.OnGameLose -= StopMoving;
        }

        public void SetMoveSpeed(float value)
        {
            _curSpeed = value;
        }
      
        private void StopMoving()
        {
            _curSpeed = 0;
        }
       
    }
