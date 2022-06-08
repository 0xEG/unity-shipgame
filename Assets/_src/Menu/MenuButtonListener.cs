using UnityEngine;
using UnityEngine.UI;

public class MenuButtonListener : MonoBehaviour
{
     [SerializeField] private CharacterManager _characterManager;
     [SerializeField] private CharacterCreator[] _classArr;
     [SerializeField] private Button[] _buttonArr;
     
     
     private void Start()
     {
        // Adds each button a listener with class stats.
          for (int i = 0; i < _buttonArr.Length; i++)
          {
            var temp = _classArr[i];
            _buttonArr[i].onClick.AddListener(() => _characterManager.AddCharacter(
                temp.name, temp.ClassId, temp.BuffModifier ,temp.FoodConsumption));
            
          }
     }
}
