using UnityEngine;
using UnityEngine.UI;

public class SacrifaceScreen : MonoBehaviour
{
    [SerializeField] private CharacterManager _characterManager;
    [SerializeField] private Canvas _sacrifaceCanvas;

    [SerializeField] private Button[] _buttons;
    [SerializeField] private Sprite[] _charSprite;


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
        
        GameObject.FindGameObjectWithTag("Time").GetComponent<MaterialBase>().Add(TimeAfterKill);
    }
}