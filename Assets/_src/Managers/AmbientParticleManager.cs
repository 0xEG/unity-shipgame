using UnityEngine;

namespace _src.Managers
{
    public class AmbientParticleManager : MonoBehaviour
    {
        [SerializeField] private GameObject _rainPrefab;
        [SerializeField] private GameObject _windPrefab;

        private void OnEnable()
        {
            GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        }

        private void Start()
        {
            _rainPrefab.SetActive(false);
            _windPrefab.SetActive(true);
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
        }

        private void GameManagerOnGameStateChanged(GameState state)
        {
            if (state == GameState.EndOfDay)
            {
                SelectRandomAmbientParticle();
            }
        }

        private void SelectRandomAmbientParticle()
        {
            if (Random.Range(0, 1) == 0)
            {
                _rainPrefab.SetActive(true);
                _windPrefab.SetActive(false);
            }
            
            else
            {
                _windPrefab.SetActive(true);
                _rainPrefab.SetActive(false);
            }
        }
    }
}