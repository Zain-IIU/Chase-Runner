using UnityEngine;

public abstract class ObstacleBase : MonoBehaviour
{
    [SerializeField] protected Rigidbody rb;
    protected static int Randomizer = 1;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                rb.constraints = RigidbodyConstraints.None;
                rb.useGravity = true;
                EventsManager.ObstacleHit();
            }
        }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Robber"))
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.useGravity = true;
            var forceDir = (Vector3.right *Randomizer + Vector3.up + Vector3.forward);
            Randomizer *= -1;
            rb.AddForce(forceDir*10f,ForceMode.Impulse);
            rb.AddTorque(Vector3.up*10f,ForceMode.Impulse);
        }
    }
}
