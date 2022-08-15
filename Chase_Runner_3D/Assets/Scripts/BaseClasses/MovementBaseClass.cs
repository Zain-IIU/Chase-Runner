using DG.Tweening;
using UnityEngine;


public abstract class MovementBaseClass : MonoBehaviour
{

    [SerializeField] protected float lerpTime;
    [SerializeField] protected float turningTime;

    [SerializeField] protected Transform body;
    [SerializeField] protected float maxRot;

     //protected properties

    protected float XMove;

    //-----------------

    
    protected virtual void Start()
    {
        EventsManager.OnGameStart += StartMovement;
    }

    protected virtual void OnDisable()
    {
       
        EventsManager.OnGameStart += StartMovement;
    }


    private void StartMovement()
    {
       
    }

    protected void HandleMovement()
    {
        HandleForwardMovement();
    }

    protected void HandleMovement(float speed)
    {
        HandleForwardMovementNpc(speed);
    }

    #region  Movement Methods

    private void HandleForwardMovementNpc(float speed)
    {
        var position = transform.localPosition;
        var localPosition = position;
        localPosition.x = XMove;
        position = Vector3.Lerp(position, localPosition, lerpTime * Time.deltaTime);
        transform.localPosition = position;
        transform.Translate(Vector3.forward*speed*Time.deltaTime);
    }
    private void HandleForwardMovement()
    {       
        var position = transform.localPosition;
        var localPosition = position;
        localPosition.x = XMove;
        position = Vector3.Lerp(position, localPosition, lerpTime * Time.deltaTime);
        transform.localPosition = position;
    }
    protected void HandleRotation(int value)
    {
        body.transform.DOLocalRotate(new Vector3(0, maxRot * value, 0), turningTime).OnComplete(() =>
            {
                body.transform.DOLocalRotate(Vector3.zero, turningTime/4);
            });
    }
    
    #endregion
}
