using UnityEngine;


public class Sensor : MonoBehaviour
{
    [SerializeField] private Transform boxCastPoint;

    [SerializeField] private Vector3 boxCastDimensions;
    [SerializeField] private float sensingDistanceForward;
    [SerializeField] private float sensingDistanceBackward;
    [SerializeField] private LayerMask obstacleLayer;
    private bool _hitForward;
    private bool _hitBackward;
    private RaycastHit _mHitForwardRaycastHit;
    private RaycastHit _mHitBackwardRaycastHit;
    public bool DetectObstacle()
    {
        var position = boxCastPoint.position;
        _hitForward = Physics.BoxCast(position, boxCastDimensions, Vector3.forward,out _mHitForwardRaycastHit, Quaternion.identity,
            sensingDistanceForward, obstacleLayer);
        _hitBackward = Physics.BoxCast(position, boxCastDimensions, Vector3.back,out _mHitBackwardRaycastHit, Quaternion.identity,
            sensingDistanceBackward, obstacleLayer);
        
        return _hitForward || _hitBackward;
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Check if there has been a hit yet
        if (_hitForward)
        {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(boxCastPoint.position, transform.forward * _mHitForwardRaycastHit.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(boxCastPoint.position + transform.forward * _mHitForwardRaycastHit.distance, boxCastDimensions);
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(boxCastPoint.position, -transform.forward * _mHitBackwardRaycastHit.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(boxCastPoint.position + transform.forward * _mHitBackwardRaycastHit.distance, boxCastDimensions);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(boxCastPoint.position, transform.forward * sensingDistanceForward);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(boxCastPoint.position + transform.forward * sensingDistanceForward, boxCastDimensions);
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(boxCastPoint.position, transform.forward * -sensingDistanceBackward);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(boxCastPoint.position + transform.forward * -sensingDistanceBackward, boxCastDimensions);
        }
    }

}