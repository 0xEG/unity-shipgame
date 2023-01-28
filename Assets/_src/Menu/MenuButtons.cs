using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _src.Menu
{
    public class MenuButtons : MonoBehaviour
    {
        [SerializeField] private Canvas _characterSelectionCanvas;
        [SerializeField] private SaveLoad _saveLoad;

        [SerializeField] private Button[] _buttonArr;

        public void Continue()
        {
        
        }
    
        public void NewGame()
        {
            _characterSelectionCanvas.gameObject.SetActive(true);
            LoadFromFile();
        }

        private void LoadFromFile()
        {
        
        }

        public void LoadGame()
        {
            _saveLoad.SaveAll();
            SceneManager.LoadScene("Scenes/Scene");
        }
        public void OpenSettings()
        {
        
        }
        public void ClearClasses(){
   
            foreach(var button in _buttonArr){
                button.interactable = true;
            }
        }
        public void Quit()
        {
            Application.Quit();
        }
    }
}
