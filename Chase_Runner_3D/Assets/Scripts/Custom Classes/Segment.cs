using UnityEngine;

public class Segment : MonoBehaviour
{
    [SerializeField] private Level[] segments;
    public int curSegment;

    public bool isTunnel;
    private void Start()
    {
        curSegment = 0;
    }

    public void IncrementSegment()
    {
        curSegment++;
        if (curSegment >= segments.Length)
            curSegment = 0;
        foreach (var segment in segments)
        {
            segment.EnableLevelSegment(false);
        }
        segments[curSegment].EnableLevelSegment(true);
    }

    public Level GetActiveLevel()
    {
        foreach (var segment in segments)
        {
            if (segment.GetLevel().activeSelf)
                return segment;

        }

        return null;
    }
}
