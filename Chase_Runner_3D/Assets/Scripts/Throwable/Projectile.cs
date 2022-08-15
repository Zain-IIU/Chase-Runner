using UnityEngine;

public class Projectile : ProjectileBase
    {
        protected override void Start()
        {
            base.Start();
            print("This is a projectile");
        }
        public override void Throw(Transform player)
        {
            var transform1 = transform;
            transform1.parent = null;
            rb.isKinematic = false;

            var direction = (player.position - transform1.position).normalized;
        
            rb.AddForce(direction* throwForce, ForceMode.Impulse);
            rb.AddTorque(Vector3.right* throwForce*2f,ForceMode.Impulse);
        }

        protected override void OnCollisionEnter(Collision other)
        {
            base.OnCollisionEnter(other);
            print("");
        }
    }
