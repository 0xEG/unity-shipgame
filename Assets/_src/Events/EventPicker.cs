using System.Collections.Generic;
using System.Linq;
using _src.Character;
using _src.Managers;
using _src.MaterialSystems;
using _src.SOs;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace _src.Events
{
    public class EventPicker : MonoBehaviour
    {
        private List<EventSystem> EventArrList = new List<EventSystem>();
        private List<EventSystem> EventArrList50 = new List<EventSystem>();
        private List<EventSystem> EventArrList25 = new List<EventSystem>();
        private List<EventSystem> EventArrList5 = new List<EventSystem>();

        private EventSystem[] EventArr;
        private EventSystem[] EventArr50;
        private EventSystem[] EventArr25;
        private EventSystem[] EventArr5;

        [SerializeField] private GameObject EventCanvasGameObj;

        [SerializeField] private MaterialBase moraleManager;
        [SerializeField] private CharacterManager characterManager;

        [SerializeField] private FightSliderController fightController;

        #region UI

        [Header("Text field")]
        [SerializeField]
        private TextMeshProUGUI header;

        [SerializeField] private TextMeshProUGUI descriptionWImage;
        [SerializeField] private TextMeshProUGUI description;
        [Space][SerializeField] private Image image;
        [Space][SerializeField] private Button[] buttonArr;

        #endregion

        private EventSystem _currentEvent;

        private void Awake()
        {
            print("Loading events.");

            EventArr = Resources.LoadAll<EventSystem>("Event/Normal");
            EventArrList = EventArr.ToList();

            EventArr50 = Resources.LoadAll<EventSystem>("Event/%50moral");
            EventArrList50 = EventArr50.ToList();

            EventArr25 = Resources.LoadAll<EventSystem>("Event/%25moral");
            EventArrList25 = EventArr25.ToList();

            EventArr5 = Resources.LoadAll<EventSystem>("Event/%5moral");
            EventArrList5 = EventArr5.ToList();
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
            if (state == GameState.Event)
            {
                PickRandomEvent();
            }
        }

        private void PickRandomEvent(EventSystem subEvent = null)
        {
            if (subEvent != null)
            {
                _currentEvent = subEvent;
            }
            else
            {
                var rand = Random.Range(0, 10);

                if (rand > 8)
                {
                    if (moraleManager.Value > 25 && moraleManager.Value <= 50)
                    {
                        if (!EventArrList50.Any())
                        {
                            ExecuteEvent(ref EventArrList);
                        }

                        ExecuteEvent(ref EventArrList50);
                    }
                    else if (moraleManager.Value > 5 && moraleManager.Value <= 25)
                    {
                        if (!EventArrList25.Any())
                        {
                            ExecuteEvent(ref EventArrList);
                        }

                        ExecuteEvent(ref EventArrList25);
                    }
                    else if (moraleManager.Value <= 5)
                    {
                        if (!EventArrList5.Any())
                        {
                            ExecuteEvent(ref EventArrList);
                        }

                        ExecuteEvent(ref EventArrList5);
                    }
                }
                else
                {
                    ExecuteEvent(ref EventArrList);
                }
            }

            RemoveListeners();
            ClearGUI();
            PlaceGUI(_currentEvent.name, _currentEvent.description, _currentEvent.image);
            AddUnityEvents(_currentEvent.Events, _currentEvent.Choices, _currentEvent.EventTree,
                _currentEvent.EventTreeFalse, _currentEvent.chance, _currentEvent.isWar, _currentEvent.WarEvents);
        }

        private void ExecuteEvent(ref List<EventSystem> eventList)
        {
            var randomInt = Random.Range(0, eventList.Count);
            _currentEvent = eventList[randomInt];
            if (_currentEvent.isRepeatable == false)
            {
                eventList.RemoveAt(randomInt);
            }
        }

        private void AddUnityEvents(UnityEvent[] events, string[] choices, EventSystem[] EventTree, EventSystem[] EventTreeFalse,
            int[] chances, bool isWar, EventSystem[] warEvents)
        {
            for (int i = 0; i < buttonArr.Length; i++)
            {
                if (choices[i] == "" || choices[i].Equals("") || choices[i].Equals(null))
                {
                    continue;
                }

                if (_currentEvent.ClassRequirements[i] == ClassIdEnum.None)
                {
                    buttonArr[i].gameObject.SetActive(true);
                    buttonArr[i].GetComponentInChildren<TextMeshProUGUI>().text = choices[i];
                    buttonArr[i].onClick.AddListener(events[i].Invoke);

                    EventTreeListener(EventTree, EventTreeFalse, chances, isWar, warEvents);
                }

                if (_currentEvent.ClassRequirements[i] != ClassIdEnum.None)
                {
                    foreach (var character in characterManager.Crew)
                    {
                        if (_currentEvent.ClassRequirements[i] == character.ClassId)
                        {
                            buttonArr[i].gameObject.SetActive(true);
                            buttonArr[i].GetComponentInChildren<TextMeshProUGUI>().text = choices[i];
                            buttonArr[i].onClick.AddListener(events[i].Invoke);
                            EventTreeListener(EventTree, EventTreeFalse, chances, isWar, warEvents);
                        }
                    }
                }
            }
        }


        private void AddListeners(Button button, EventSystem[] Events, int j)
        {
            for (int i = 0; i < Events.Length; i++)
            {
                button.onClick.AddListener(() => PickRandomEvent(Events[j]));
            }
        }
    
        private void RemoveListeners()
        {
            for (int i = 0; i < buttonArr.Length; i++)
            {
                buttonArr[i].gameObject.SetActive(false);
                buttonArr[i].onClick.RemoveAllListeners();
            }
        }
        private void EventTreeListener(EventSystem[] eventTree, EventSystem[] eventTreeFalse, int[] chances, bool isWar,
            EventSystem[] warEvents)
        {
            for (var i = 0; i < buttonArr.Length; i++)
            {
                var button = buttonArr[i];
                if (!isWar)
                {
                    var rand = Random.Range(0, 10);
                    if (rand > chances[i] || chances[i] == 10)
                    {
                        AddListeners(button, eventTree, i);
                    }
                    else
                    {
                        AddListeners(button, eventTreeFalse, i);
                    }
                }
                else
                {
                    if (fightController.DetermineFightPower(_currentEvent.difficulty) > _currentEvent.difficulty)
                    {
                        AddListeners(button, warEvents, 0);
                    }
                    else
                    {
                        AddListeners(button, warEvents, 1);
                    }
                }
            }
        }

        private void PlaceGUI(string headerName, string _description, Sprite img)
        {
            EventCanvasGameObj.SetActive(true);

            header.text = headerName;

            if (img == null)
            {
                image.enabled = false;
                description.text = _description;
            }
            else
            {
                image.enabled = true;
                image.sprite = img;
                image.preserveAspect = true;

                descriptionWImage.text = _description;
            }
        }

        private void ClearGUI()
        {
            EventCanvasGameObj.SetActive(false);
            header.text = null;
            description.text = null;
            descriptionWImage.text = null;
            image.sprite = null;
        }
    }
}
