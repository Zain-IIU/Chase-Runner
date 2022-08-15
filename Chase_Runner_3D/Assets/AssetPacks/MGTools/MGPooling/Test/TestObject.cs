using UnityEngine;

public class TestObject : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 25f);
    }
}
