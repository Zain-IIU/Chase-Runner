using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SegmentManager : MonoBehaviour
{
    
    [SerializeField] private List<Segment> segments = new List<Segment>();
    [SerializeField] private NpcMovement npcBehaviour;
    [SerializeField] private float cullingDistance;
    [SerializeField] private float incrementingDistance;
    [SerializeField] private int laneChangingCounter;
    [SerializeField] private int npcAttackCounter;
    [SerializeField] private Segment tunnelSegment;
    [SerializeField] private float segmentMovingSpeed;

    private float _curSpeed;

    private int _curCounterLanes;
    private int _curCounterAttacks;

    private int _updateCounter;

    private bool _shouldUpdate;
    // Start is called before the first frame update
    void Start()
    {
        _curSpeed = 0;
        _curCounterLanes = 0;
        _curCounterAttacks = 0;
        _updateCounter = 0;
        _shouldUpdate = false;
        EventsManager.OnGameStart += StartMovingSegments;
        EventsManager.OnSegmentSpeedIncrease += IncreaseSpeed;
    }

    private void OnDisable()
    {
        EventsManager.OnGameStart -= StartMovingSegments;
        EventsManager.OnSegmentSpeedIncrease -= IncreaseSpeed;
    }

    private void StartMovingSegments()
    {
        _curSpeed = segmentMovingSpeed;
    }

    private void IncreaseSpeed()
    {
        _curSpeed += 5f;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.PlayerHasDied) return;
        MoveSegments();
        CheckForFirstSegment();
    }

    private void MoveSegments()
    {
        foreach (var segment in segments)
        {
            segment.transform.Translate(Vector3.back* _curSpeed*Time.deltaTime);
        }
    }

    private void CheckForFirstSegment()
    {
        var transformLocalPosition = segments[0].transform.localPosition;
        if (!(transformLocalPosition.z <= -cullingDistance)) return;
        
        if (segments[0].isTunnel)
        {
            segments[0].transform.DOLocalMove(new Vector3(400, 0, 0), 0);
            segments.RemoveAt(0);
            return;
        }

        if (!_shouldUpdate)
        {
            _curCounterLanes++;
            _curCounterAttacks++;
        }
        if(_shouldUpdate)
        {
            _updateCounter++;            
            if (_updateCounter >= segments.Count-2)
            {
                _shouldUpdate = false;
                _updateCounter = 0;
            }
            segments[0].IncrementSegment();
        }

        if (_curCounterAttacks >= npcAttackCounter)
        {
            _curCounterAttacks = 0;
            npcBehaviour.ChangeState();
        }
        if(_curCounterLanes>=laneChangingCounter)
        {
            segments[0].IncrementSegment();
            transformLocalPosition.z = segments[segments.Count - 1].transform.localPosition.z + incrementingDistance*2;
        
            segments[0].transform.localPosition = transformLocalPosition;

            segments.Add(tunnelSegment);
           
            segments[segments.Count - 1].transform
                .DOLocalMove(new Vector3(0, 0, segments[segments.Count - 2].transform.localPosition.z +incrementingDistance), 0);
            _shouldUpdate = true;
            print("Lanes Should be updated");
            _curCounterLanes = 0;
        }
        else
        {
            transformLocalPosition.z = segments[segments.Count - 1].transform.localPosition.z + incrementingDistance;
            segments[0].transform.localPosition = transformLocalPosition;
            segments[0].GetActiveLevel().EnablePickUps();
        }

        var temp = segments[0];
        segments.RemoveAt(0);
        segments.Add(temp);
    }

    public float GetCurSpeed() => _curSpeed;
}
