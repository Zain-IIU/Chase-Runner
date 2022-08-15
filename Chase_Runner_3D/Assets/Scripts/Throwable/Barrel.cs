using UnityEngine;


    public class Barrel : ProjectileBase
    {
        protected override void Start()
        {
            base.Start();
            print("This is a barrel");
        }

        public override void Throw()
        {
            transform.parent = null;
            rb.AddForce(Vector3.back*throwForce,ForceMode.Impulse);
            rb.AddTorque(Vector3.right*throwForce,ForceMode.Impulse);
        }

        protected override void OnCollisionEnter(Collision other)
        {
            base.OnCollisionEnter(other);
            print("");
        }
    }
