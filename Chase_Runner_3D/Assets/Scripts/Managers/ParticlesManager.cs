using DG.Tweening;
using UnityEngine;


    public class ParticlesManager : MonoBehaviour
    {
        public static ParticlesManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        [SerializeField] private ParticleSystem explosionVfx;

        public void PlayJumpVfxAt(Transform pos)
        {
            explosionVfx.transform.DOMove(pos.position, 0).OnComplete(explosionVfx.Play);
        }
    }
