using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
    {
        public static bool GameHasStarted;
        public static bool PlayerHasDied;


        private void Awake()
        {
            DOTween.SetTweensCapacity(500,500);
        }

        private void Start()
        {
            PlayerHasDied = false;
            GameHasStarted = false;
        }
        //to be called from event 
        public void StartGame()
        {
           EventsManager.NpcStartRun();
           GameHasStarted = true;
           StartCoroutine(nameof(StartGameAfterNpcRuns));
        }

        public void RestartGame()
        {
            PlayerPrefs.Save();
            Debug.Log("Reloading level");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        private IEnumerator StartGameAfterNpcRuns()
        {
            yield return new WaitForSeconds(1f);
            EventsManager.GameStart();
        }

        private void OnDestroy()
        {
            DOTween.KillAll();
        }
    }
