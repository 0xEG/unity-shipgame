using _src.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace _src.Menu
{
    public class SacrifaceScreen : MonoBehaviour
    {
        [SerializeField] private CharacterManager _characterManager;
        [SerializeField] private Canvas _sacrifaceCanvas;

        [SerializeField] private Button[] _buttons;

        [SerializeField] private float TimeAfterKill;
        public void SetupScreen()
        {
            foreach (var button in _buttons)
            {
                button.onClick.RemoveAllListeners();
            }
            for (int i = 0; i < _characterManager.Crew.Count; i++)
            {
                foreach (var button in _buttons)
                {
                    if (_characterManager.Crew[i].ClassId.ToString() != button.gameObject.name) continue;
                    button.gameObject.SetActive(true);
                    button.onClick.AddListener(() => _sacrifaceCanvas.gameObject.SetActive(false));
                    button.onClick.AddListener(() => button.gameObject.SetActive(false));
                }
            }
        
            MaterialManager.instance.TimeMaterial.Add(TimeAfterKill);
        }
    }
}