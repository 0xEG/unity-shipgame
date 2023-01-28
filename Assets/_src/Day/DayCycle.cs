using System.Collections;
using _src.Managers;
using _src.Menu;
using TMPro;
using UnityEngine;

namespace _src.Day
{
    public class DayCycle : MonoBehaviour
    {
        private int _dayCount = 1;

        [SerializeField] private ShopManager shopManager;

        [SerializeField] private MaterialManager materialManager;
        
        [Space] [Header("TRANSITION")]
        [SerializeField] private FadeBlack fadeBlack;

        [SerializeField] private TextMeshProUGUI dayText;

        [SerializeField] private DotWeenAnims _dotWeen;
        [SerializeField] private RectTransform _eventPanel;
    
        private void OnEnable()
        {
            GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
        }

        private void GameManagerOnGameStateChanged(GameState state)
        {
            if (state == GameState.EndOfDay)
            {
                StartCoroutine(HandleDayCycle());
            }
        }

        private IEnumerator HandleDayCycle()
        {
            IncreaseDayCount();

            fadeBlack.FadeIn(2);
            yield return new WaitForSeconds(2);

            ResetEventPanel();
            
            shopManager.OnDayEnd(_dayCount);
            materialManager.OnDayEnd();

            yield return new WaitForSeconds(2);

            fadeBlack.FadeOut(2);

            yield break;
        }

        private void ResetEventPanel()
        {
            _eventPanel.anchoredPosition = new Vector2(0, 1020);
            _dotWeen.isOpened = false;
        }

        private void IncreaseDayCount()
        {
            _dayCount++;
            dayText.text = "Day " + _dayCount.ToString();
        }
    }
}