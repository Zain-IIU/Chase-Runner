using UnityEngine;


    public class TriggerArea : MonoBehaviour
    {
        [SerializeField] private int id;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                EventsManager.CarTrigger(id);
            }
        }
    }
