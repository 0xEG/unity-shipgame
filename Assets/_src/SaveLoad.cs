using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using _src.Character;
using _src.Managers;
using _src.MaterialSystems;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSaveData{
    public CharacterSaveData(Character character)
    {
        Name = character.Name;
        ClassId = character.ClassId;
        BuffModifier = character.BuffModifier;
        FoodConsumption = character.FoodConsumption;
        isAbleToWork = character.IsAbleToWork;
        isCrazy = character.IsCrazy;
    }

    public string Name;
    public ClassIdEnum ClassId;
    public float BuffModifier;
    public float FoodConsumption;
    public bool isAbleToWork;
    public bool isCrazy; 
}
public class MaterialSaveData
{
    public MaterialSaveData(MaterialBase material)
    {
        ModifierClass = material.ModifierClass;
        Value = material.Value;
        Modifier = material.Modifier;
        isMax = material.doesHaveLimit;
    }

    public ClassIdEnum ModifierClass;
    public float Value;
    public float Modifier;
    public bool isMax;
}

public class SaveLoad : MonoBehaviour
{
    public CharacterSaveData Data;
    public MaterialSaveData MatData;
    
    public string json;

    private string SaveFileCharacters;
    private string SaveFileMaterials;

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
        if (state == GameState.Save)
        {
            SaveAll();
        }
        if (state == GameState.Load)
        {
            Load();
        }
    }

    private void Awake()
    {
        SaveFileCharacters = Application.persistentDataPath + "/" + "save.file";
        SaveFileMaterials = Application.persistentDataPath + "/" + "mat.file";
    }


    [SerializeField] private CharacterManager _characterManager;
    [SerializeField] private MaterialBase[] materialArray;

    public void SaveAll()
    {
        SaveCharacters();
        SaveMats();
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            GameManager.Instance.UpdateGameState(GameState.Load);
        }
    }

    public void Load()
    {
        LoadCharacters();
        LoadMats();
        
        GameManager.Instance.UpdateGameState(GameState.Event);
    }
    
    public void SaveLoadGame()
    {
        SaveAll();
        Load();
    }
    [ContextMenu("Save")]
    private void SaveCharacters()
    {
        if (File.Exists(SaveFileCharacters))
        {
            File.Delete(SaveFileCharacters);
        }
        TextWriter textWriter = new StreamWriter(SaveFileCharacters,true);

        foreach (var character in _characterManager.Crew)
        {
            Data = new CharacterSaveData(character);
            string json = JsonUtility.ToJson(Data);
            textWriter.WriteLine(json);
        }
        textWriter.Close();
    }


    [ContextMenu("Load")]
    private void LoadCharacters()
    {
        if (!File.Exists(SaveFileCharacters))
        {
            return;
        }

        _characterManager.Crew.Clear();

        foreach (var line in File.ReadLines(SaveFileCharacters))
        {
            Data = JsonUtility.FromJson<CharacterSaveData>(line);
            _characterManager.AddCharacter(Data.Name, Data.ClassId, Data.BuffModifier, Data.FoodConsumption, Data.isAbleToWork, Data.isCrazy);
        }
    }

    [ContextMenu("MatSave")]
    public void SaveMats()
    {
        if (File.Exists(SaveFileMaterials))
        {
            File.Delete(SaveFileMaterials);
        }
        TextWriter textWriter = new StreamWriter(SaveFileMaterials, true);

        foreach (var material in materialArray)
        {
            MatData = new MaterialSaveData(material);
            string json = JsonUtility.ToJson(MatData);
            textWriter.WriteLine(json);
        }
        textWriter.Close();
    }

    [ContextMenu("LoadMats")]
    public void LoadMats()
    {
        if (!File.Exists(SaveFileMaterials))
        {
            return;
        }

        var x = 0;
        foreach (var line in File.ReadLines(SaveFileMaterials))
        {
            // Setting everything to 0 before load to prevent miss data
            materialArray[x].SetAll(0, 0, 1, false);
            MatData = JsonUtility.FromJson<MaterialSaveData>(line);
            materialArray[x].SetAll(MatData.ModifierClass, MatData.Value, MatData.Modifier, MatData.isMax);
            x++;
        }
    }
}


