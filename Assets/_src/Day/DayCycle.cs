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
    [SerializeField] private MaterialBase _moraleManager;
    [Space]
    [SerializeField] private GameObject _rainPrefab;
    [SerializeField] private GameObject _windPrefab;
    [Space]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _mainMusic, _shopMusic;
    [Space]
    [SerializeField] private FadeBlack _fadeBlack;
    [SerializeField] private TextMeshProUGUI _dayText;

    [SerializeField] private DotWeenAnims _dotWeen;
    [SerializeField] private RectTransform _eventPanel;
    [SerializeField] private Canvas _cityCanvas;

    
    [Tooltip("1. Element must be food slider")][SerializeField] private GetDailyMats[] _dailyMats;

    private void Start()
    {
        _rainPrefab.SetActive(false);
        _windPrefab.SetActive(true);
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
        _dayText.text = "Day " + day.ToString();
        _fadeBlack.FadeIn(2);

        yield return new WaitForSeconds(2);
        _eventPanel.anchoredPosition = new Vector2(0, 1020);
        _dotWeen.isOpened = false;

        if (day == 10)
        {
            _cityCanvas.gameObject.SetActive(true);
            _audioSource.clip = _shopMusic;
            if (!_audioSource.isPlaying.Equals(_shopMusic))
            {
                _audioSource.Play();
            }

        }
        else
        {
            _cityCanvas.gameObject.SetActive(false);
            _audioSource.clip = _mainMusic;

            if (!_audioSource.isPlaying.Equals(_mainMusic))
            {
                _audioSource.Play();
            }
        }
        
        var rand = Random.Range(0, 1);
        if (rand == 0)
        {
            _rainPrefab.SetActive(true);
            _windPrefab.SetActive(false);
            
        }
        else
        {
            _windPrefab.SetActive(true);
            _rainPrefab.SetActive(false);
        }
        
	if(_foodManager.Value !> 0){
		_foodManager.Add(FoodData());
	}
	else{
		_moraleManager.Add(-10);
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

	_moraleManager.Add(-_dailyMats[0].Morale);

        if (modifier != 0)
        {
            return -total / _dailyMats[0].Modifier;
        }
        return total= 0;
    }
}
