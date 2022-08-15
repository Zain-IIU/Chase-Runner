using DG.Tweening;
using UnityEngine;
public class NpcThrow : MonoBehaviour
    {
        [SerializeField] private Transform spawnPointBarrel;
        [SerializeField] private Transform spawnPointProjectile;
        private ObjectPooler _objectPool;

       
        [SerializeField] private Transform playerTarget;
        
        private void Start()
        {
            _objectPool = ObjectPooler.instance;
        }
        private void ThrowBarrelAtPlayer()
        {
            var barrel=_objectPool.SpawnFromPool("Barrel", Vector3.zero, Quaternion.identity);
            barrel.transform.parent = spawnPointBarrel;
            //var barrel = Instantiate(barrelPrefab, spawnPointBarrel, true);
            barrel.gameObject.SetActive(false);
            barrel.transform.DOLocalMove(Vector3.zero, 0).OnComplete(() => barrel.gameObject.SetActive(true));
            barrel.transform.DOLocalRotate(Vector3.zero, 0).OnComplete(() =>
            {
                barrel.GetComponent<Barrel>().Throw();
            });
        }
        private void ThrowProjectileAtPlayer()
        {
            var projectile =_objectPool.SpawnFromPool("Projectile", Vector3.zero, Quaternion.identity);
            projectile.transform.parent = spawnPointProjectile;
            projectile.gameObject.SetActive(false);
            projectile.transform.DOLocalMove(Vector3.zero, 0).OnComplete(() => projectile.gameObject.SetActive(true));
            projectile.transform.DOLocalRotate(new Vector3(0, 0, 90), 0).OnComplete(() =>
            {
                projectile.GetComponent<Projectile>().Throw(playerTarget.transform);
            });
        }
    }
