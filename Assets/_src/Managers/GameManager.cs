using System;
using _src.Menu;
using UnityEngine;

namespace _src.Managers
{
    public enum GameState : byte
    {
        Load,
        Save,
        Event,
        EndOfDay,
        Beast,
        Victory,
        Lose
    }

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Canvas _sacrifacecanvas;
        [SerializeField] private SacrifaceScreen _sacrifaceScript;

        public static GameManager Instance;

        [SerializeField] private GameState currentState;

        public GameState CurrentState
        {
            get => currentState;
            private set
            {
                currentState = value;
            }
        }

        public static event Action<GameState> OnGameStateChanged;
    
        public void UpdateGameState(GameState state)
        {
            CurrentState = state;

            switch (state)
            {
                case GameState.Save:
                    break;
                case GameState.Load:
                    break;
                case GameState.Event:
                    break;
                case GameState.EndOfDay:
                    break;
                case GameState.Beast:
                    _sacrifacecanvas.gameObject.SetActive(true);
                    _sacrifaceScript.SetupScreen();
                    UpdateGameState(GameState.Event);
                    break;
                case GameState.Victory:
                    break;
                case GameState.Lose:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }

            OnGameStateChanged?.Invoke(state);
        }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            UpdateGameState(GameState.Load);
        }

        public void FinishTheDay()
        {
            GameManager.Instance.UpdateGameState(GameState.EndOfDay);
        }
    }
}