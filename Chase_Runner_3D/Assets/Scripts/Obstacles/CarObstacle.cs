using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarObstacle : ObstacleBase
{
    private readonly int[] _random = {-1, 1};
    [SerializeField] private float tweenTime;

    [SerializeField] private float startX;
    [SerializeField] private int curID;

    private int _randomIndex;
    private void OnEnable()
    {        
        EventsManager.OnCarTrigger += Tween;
    }

    private void OnDisable()
    {
        EventsManager.OnCarTrigger -= Tween;
    }

    private void Start()
    {
        SetRandomStartPos();
    }

    private void SetRandomStartPos()
    {
        _randomIndex=_random[Random.Range(0, _random.Length)];

        transform.DOLocalMoveX(startX * _randomIndex, 0);

        transform.DOLocalRotate(new Vector3(0, 90 * (-_randomIndex), 0),0);
    }
   
    private void Tween(int id)
    {
        if (id == curID)
        {
            transform.DOLocalMoveX(-startX * _randomIndex, tweenTime).OnComplete(SetRandomStartPos);
        }
    }

    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
        print("");
    }
}
