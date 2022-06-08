using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PCC;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private CharacterPrinter cPrinter; 

    public List<Character> Crew = new List<Character>();
    public void AddCharacter(string name, ClassIdEnum classId, float buffModifier, float foodConsumption)
    {
        Crew.Add(new Character(name, classId, buffModifier , foodConsumption));
    }
    public void AddCharacter(string name, ClassIdEnum classId, float buffModifier, float foodConsumption, bool isAbleToWork, bool isCrazy)
    {
        Crew.Add(new Character(name, classId, buffModifier, foodConsumption, isAbleToWork, isCrazy));
    }

    public void RemoveCharacter(int classId)
    {
        ClassIdEnum x = (ClassIdEnum) classId;
        for (int i = 0; i < Crew.Count; i++)
        {
            if (Crew[i].ClassId == x)
            {
                Crew.Remove(Crew[i]);
            }
        }
        cPrinter.PrintToCanvas();
        GameManager.Instance.UpdateGameState(GameState.Save);
    }

    public void RemoveAllCharacters()
    {
        Crew.Clear();
    }
}