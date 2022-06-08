using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DayCycle : MonoBehaviour
{

    [SerializeField] private MaterialBase _foodManager;
    [SerializeField] private MaterialBase _time;
    [SerializeField] private CharacterManager _characterManager;
    [SerializeField] private MaterialBase MoraleManager;
    [Space]
    [SerializeField] private GameObject Yagmur;
    [SerializeField] private GameObject Ruzgar;
    [Space]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip main, shop;
    [Space]
    [SerializeField] private FadeBlack _fadeBlack;
    [SerializeField] private TextMeshProUGUI DayText;

    [SerializeField] private DotWeenAnims _dotWeen;
    [SerializeField] private RectTransform _eventPanel;
    [SerializeField] private Canvas _city;

    
    [Tooltip("1. Element must be food slider")][SerializeField] private GetDailyMats[] _dailyMats;

    private void Start()
    {
        Yagmur.SetActive(false);
        Ruzgar.SetActive(true);
    }

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

    private int day = 1;
    public IEnumerator HandleDayCycle()
    {
        day++;
        DayText.text = "Day " + day.ToString();
        _fadeBlack.FadeIn(2);

        yield return new WaitForSeconds(2);
        _eventPanel.anchoredPosition = new Vector2(0, 1020);
        _dotWeen.isOpened = false;

        if (day == 10)
        {
            _city.gameObject.SetActive(true);
            _audioSource.clip = shop;
            if (!_audioSource.isPlaying.Equals(shop))
            {
                _audioSource.Play();
            }

        }
        else
        {
            _city.gameObject.SetActive(false);
            _audioSource.clip = main;

            if (!_audioSource.isPlaying.Equals(main))
            {
                _audioSource.Play();
            }
        }
        
        var rand = Random.Range(0, 1);
        if (rand == 0)
        {
            Yagmur.SetActive(true);
            Ruzgar.SetActive(false);
            
        }
        else
        {
            Ruzgar.SetActive(true);
            Yagmur.SetActive(false);
        }
        
	if(_foodManager.Value !> 0){
		_foodManager.Add(FoodData());
	}
	else{
		MoraleManager.Add(-10);
	}

        if (_time.Value <=0)
        {
            GameManager.Instance.UpdateGameState(GameState.Beast);
        }
        else
        {
            GameManager.Instance.UpdateGameState(GameState.Save);
        }
        yield return  new WaitForSeconds(2);
        
        _fadeBlack.FadeOut(2);
        
        yield return null;
    }

    public float FoodData()
    {
        var modifier = _dailyMats[0].Modifier;
        var total = .0f;

        foreach (var character in _characterManager.Crew)
        {
            total += character.FoodConsumption;
        }

	MoraleManager.Add(-_dailyMats[0].Morale);

        if (modifier != 0)
        {
            return -total / _dailyMats[0].Modifier;
        }
        return total= 0;
    }
}
