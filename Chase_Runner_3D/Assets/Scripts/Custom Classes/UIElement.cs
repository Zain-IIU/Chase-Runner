using DG.Tweening;
using UnityEngine;


[System.Serializable]
public class UIElement 
{ 
        [SerializeField] private RectTransform mainPanel;
        [SerializeField] private CanvasGroup inGamePanel;
        [SerializeField] private CanvasGroup winPanel;
        [SerializeField] private RectTransform losePanel;

        [SerializeField] private RectTransform tapPanel;
        [SerializeField] private float tweenTime;
        [SerializeField] private Ease easeType;
        
        public void DisableMainPanel()
        {
               // DOTween.To(() => mainPanel.alpha, x => mainPanel.alpha = x, 0, .15f).OnComplete(() => 
                        //{ mainPanel.gameObject.SetActive(false);});
                        mainPanel.DOAnchorPosY(-500, tweenTime).SetEase(easeType);
        }

        public void EnableWinPanel()
        {
                DOTween.To(() => inGamePanel.alpha, x => inGamePanel.alpha = x, 0, 0f).OnComplete(() =>
                {
                        inGamePanel.gameObject.SetActive(false);
                        winPanel.gameObject.SetActive(true);
                });
                DOTween.To(() => winPanel.alpha, x => winPanel.alpha = x, 1, 1.5f);
                //EventsManager.instance.Haptic(HapticTypes.Success);
                //# todo: tiny sauce (this is end  of game with win)
        }
        public void EnableLosePanel()
        {
                losePanel.DOAnchorPosX(0, tweenTime).SetEase(easeType);
        }

        public void EnableEndPanel()
        {
                DOTween.To(() => inGamePanel.alpha, x => inGamePanel.alpha = x, 0, .05f).OnComplete(() =>
                {
                        inGamePanel.gameObject.SetActive(false);
                        tapPanel.DOScale(Vector3.one, .15f);
                });
                
        }


}
