using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
    public class Level
    {
        [SerializeField] private GameObject segmentLevel;
        [SerializeField] private List<GameObject> pickups = new List<GameObject>();

        public void EnableLevelSegment(bool toEnable) => segmentLevel.SetActive(toEnable);
        
        public void EnablePickUps()
        {
            foreach (var pickup in pickups)
            {
                pickup.SetActive(true);
            }
        }

        public GameObject GetLevel() => segmentLevel;
    }
