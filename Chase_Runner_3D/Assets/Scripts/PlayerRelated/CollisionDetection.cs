using UnityEngine;

    public class CollisionDetection : MonoBehaviour
    {
      
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("InTunnel"))
            {
                EventsManager.EnterTunnel();
            }
            if (other.gameObject.CompareTag("OutTunnel"))
            {
                EventsManager.ExitTunnel();
            }

            if (other.gameObject.CompareTag("Gold"))
            {
                other.gameObject.SetActive(false);
                EventsManager.CoinPickUp();
            }
        }

    }
