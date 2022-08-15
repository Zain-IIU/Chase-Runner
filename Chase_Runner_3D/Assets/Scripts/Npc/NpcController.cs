using System.Collections;
using UnityEngine;

[SelectionBase]
public class NpcController : MonoBehaviour
{
    [SerializeField] private NpcMovement npcMovement;
    [SerializeField] private Sensor npcSensor;
     public bool isTurning;

   
     
   
    
     private void Update()
     {
         if (GameManager.PlayerHasDied) return;
         npcMovement.MoveNpc();
         if (npcSensor.DetectObstacle() && !isTurning)
         { 
             npcMovement.TurnNpc();
             isTurning = true;
             StartCoroutine(nameof(ResetTurning));
         }
     }


     IEnumerator ResetTurning()
     {
         yield return new WaitForSeconds(.25f);
         isTurning = false;
     }
    

   
   
}
