using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private Canvas CharacterSelectionCanvas;
    [SerializeField] private SaveLoad _saveLoad;

    [SerializeField] private Button[] buttonArr;

    public void Continue()
    {
        
    }
    
    public void NewGame()
    {
        CharacterSelectionCanvas.gameObject.SetActive(true);
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
   
	    foreach(var button in buttonArr){
		    button.interactable = true;
	    }
    }
    public void Quit()
    {
        Application.Quit();
    }
}