using System;
using System.Collections;
using UnityEngine;


    public class GameName : MonoBehaviour
    {
        [SerializeField] private Rigidbody[] rb;

        private void Start()
        {
            foreach (var rigidbody1 in rb)
            {
                rigidbody1.isKinematic = true;
            }

            EventsManager.OnNpcRun += SetRigidBody;
        }

        private void OnDisable()
        {
            EventsManager.OnNpcRun -= SetRigidBody;
        }

        private void SetRigidBody()
        {
            foreach (var rigidbody1 in rb)
            {
                rigidbody1.isKinematic = false;
            }

            StartCoroutine(nameof(DisableLogo));
        }

        IEnumerator DisableLogo()
        {
            yield return new WaitForSeconds(1.5f);
            gameObject.SetActive(false);
        }
    }
