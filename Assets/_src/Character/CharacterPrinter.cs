using _src.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _src.Character
{
    public class CharacterPrinter : MonoBehaviour
    {
        [SerializeField] private GameObject[] _parent;

        [SerializeField] private Image[] _images;
        [SerializeField] private TextMeshProUGUI[] _class;
        [SerializeField] private TextMeshProUGUI[] _name;
        [SerializeField] private TextMeshProUGUI[] _food;
        [SerializeField] private TextMeshProUGUI[] _hurt;
        [SerializeField] private CharacterManager _characterManager;

        [SerializeField] private Sprite[] spriteArr;

        private void Start()
        {
            PrintToCanvas();
        }

        [ContextMenu("Print")]
        public void PrintToCanvas()
        {
            foreach (var button in _parent)
            {
                button.gameObject.SetActive(false);
            }

            for (int i = 0; i < _characterManager.Crew.Count; i++)
            {
            
                _parent[i].SetActive(true);
                foreach (var sprite in spriteArr)
                {
                    if (_characterManager.Crew[i].ClassId.ToString() == sprite.name)
                    {
                        _images[i].sprite = sprite;
                        _images[i].preserveAspect = true;
                    }
                }
                _class[i].text = _characterManager.Crew[i].ClassId.ToString();
                _name[i].text = $"Name: {_characterManager.Crew[i].Name}";
                _food[i].text = $"Daily Food: {_characterManager.Crew[i].FoodConsumption}";
                
                if (_characterManager.Crew[i].IsAbleToWork == true)
                {
                    _hurt[i].text = "Health: Injured";
                }
                else
                {
                    _hurt[i].text = "Health: Healty";
                }
            }
        }
    }
}
