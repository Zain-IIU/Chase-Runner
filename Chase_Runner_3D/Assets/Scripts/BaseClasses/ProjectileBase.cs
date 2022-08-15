using System;
using System.Collections;
using UnityEngine;


    public abstract class ProjectileBase : MonoBehaviour
    {
        [SerializeField] protected Rigidbody rb;
        [SerializeField] protected float throwForce;

        protected void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        protected virtual void Start()
        {
            StartCoroutine(nameof(PutBackInPool));
        }

        public virtual void Throw()
        {}
        public virtual void Throw(Transform var)
        {}

        IEnumerator PutBackInPool()
        {
            yield return new WaitForSeconds(5);
            ParticlesManager.Instance.PlayJumpVfxAt(transform);
            gameObject.SetActive(false);
        }
        
        protected virtual void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                EventsManager.ObstacleHit();
                ParticlesManager.Instance.PlayJumpVfxAt(transform);
                gameObject.SetActive(false);
            }
        }
    }
