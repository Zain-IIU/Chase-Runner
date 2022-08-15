using UnityEngine;

[SelectionBase]
    public class RoadObstacles : ObstacleBase
    {
        protected override void OnCollisionEnter(Collision other)
        {
            base.OnCollisionEnter(other);
            print("");
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            print("");
        }
    }
