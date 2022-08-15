using System;
using UnityEngine;


public class PlayerParticles : MonoBehaviour
{
       [SerializeField] private ParticleSystem footStepVFX;
       [SerializeField] private ParticleSystem jumpVfx;
       [SerializeField] private GameObject stunnedVFX;

       private void Start()
       {
              EventsManager.OnPlayerJump += PlayJumpVfx;
              EventsManager.OnGameLose += PlayStunnedVfx;
       }

       private void OnDisable()
       {
              EventsManager.OnPlayerJump -= PlayJumpVfx;
              EventsManager.OnGameLose -= PlayStunnedVfx;

       }

       public void PlayFootStepVfx()
       {
              footStepVFX.Play();
       }
       private void PlayJumpVfx()
       {
              jumpVfx.Play();
       }

       private void PlayStunnedVfx()
       {
              stunnedVFX.SetActive(true);
       }
       
}
