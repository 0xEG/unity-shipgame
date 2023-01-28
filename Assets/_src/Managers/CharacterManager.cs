using System.Collections.Generic;
using _src.Character;
using UnityEngine;

namespace _src.Managers
{
    public class CharacterManager : MonoBehaviour
    {
        [SerializeField] private CharacterPrinter cPrinter; 

        public List<Character.Character> Crew = new List<Character.Character>();
    
        public float TotalFoodConsumption()
        {
            var total = 0f;
        
            foreach (var character in Crew)
            {
                total += character.FoodConsumption;
            }

            return total;
        }

        #region Factory
        public void AddCharacter(string name, ClassIdEnum classId, float buffModifier, float foodConsumption)
        {
            Crew.Add(new Character.Character(name, classId, buffModifier , foodConsumption));
        }
        public void AddCharacter(string name, ClassIdEnum classId, float buffModifier, float foodConsumption, bool isAbleToWork, bool isCrazy)
        {
            Crew.Add(new Character.Character(name, classId, buffModifier, foodConsumption, isAbleToWork, isCrazy));
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
        #endregion
    }
}