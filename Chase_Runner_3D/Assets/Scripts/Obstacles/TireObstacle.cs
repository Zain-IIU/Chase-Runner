
using UnityEngine;

public class TireObstacle : ObstacleBase
    {
        [SerializeField] private Rigidbody[] tires;
        protected override void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                GetComponent<Collider>().enabled = false;
                EventsManager.ObstacleHit();
                ThrowTires();
            }
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Robber"))
            {
                GetComponent<Collider>().enabled = false;
                ThrowTires();
            }
            
            
        }

        private void ThrowTires()
        {
            foreach (var tire in tires)
            {
                tire.constraints = RigidbodyConstraints.None;
                tire.useGravity = true;
                var forceDir = (Vector3.right *Randomizer + Vector3.up);
                Randomizer *= -1;
                tire.AddForce(forceDir*7f,ForceMode.Impulse);
                tire.AddTorque(Vector3.up*7f,ForceMode.Impulse);
            }
        }
    }
