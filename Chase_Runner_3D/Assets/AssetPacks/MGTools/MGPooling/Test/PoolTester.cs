using UnityEngine;

public class PoolTester : MonoBehaviour
{
   private ObjectPooler _objectPooler;

   private void Start()
   {
      _objectPooler = ObjectPooler.instance;
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Space))
      {
         _objectPooler.SpawnFromPool("Cube", Vector3.zero, Quaternion.identity);
      }
   }
}
