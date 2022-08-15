using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Player_Related
{ public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerMovement playerMovement;

        [SerializeField] private PlayerPhysics playerPhysics;
        private void Update()
        {
            playerPhysics.GravityForPlayer();
            playerMovement.MovePlayer();
        }
    }
}