using System.Collections;
using UnityEngine;

public class PlayerPhysics : PhysicsBaseClass
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float rollTime;

    private static readonly int Land = Animator.StringToHash("Land");

    protected override void Start()
    {
        EventsManager.OnPlayerJump += MakePlayerJump;
        EventsManager.OnPlayerRoll += MakePlayerRoll;
    }

    protected override void OnDisable()
    {
        EventsManager.OnPlayerJump -= MakePlayerJump;
        EventsManager.OnPlayerRoll -= MakePlayerRoll;
    }

    public void GravityForPlayer()
    {
        ApplyGravity();
        playerAnimator.SetBool(Land, IsGrounded);
        if (IsGrounded)
            gravityAmount = -30f;
    }

    private void MakePlayerRoll()
    {
        var controllerCenter = controller.center;
        controllerCenter.y = 1.22f;
        controller.center = controllerCenter;
        controller.height = 1.3f;
        StartCoroutine(nameof(ResetControllerPosition));
        gravityAmount *= 10;
    }

    private void MakePlayerJump()
    {
        if (!IsGrounded) return;

        Velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravityAmount);
    }

    IEnumerator ResetControllerPosition()
    {
        yield return new WaitForSeconds(rollTime);
        var controllerCenter = controller.center;
        controllerCenter.y = 1.59f;
        controller.center = controllerCenter;
        controller.height = 2.4f;
    }


}
