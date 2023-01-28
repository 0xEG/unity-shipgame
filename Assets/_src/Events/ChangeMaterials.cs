using _src.Character;
using _src.Managers;
using _src.MaterialSystems;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace _src.Events
{
    /// Used as a wrapper to get around Unity's inspector for making the development environment easier for designers,
    /// 
    /// As we were in a game jam I was not able to find better solution at the time,
    /// As the game is not resource heavy on the system and this calls not being much it doesn't effect performance much.
    ///
    /// Used '_MethodName' naming to prevent confusion since this methods made for inspector and not suggested to use in actual code.
    public class ChangeMaterials : MonoBehaviour
    {
        private readonly string FightManagerStr = "FightMat";
        private readonly string sldValue = "FightSlider";
    
        private GameObject tempGameObject;

        public void _ChangeGold(float gold)
        {
            MaterialManager.instance.GoldMaterial.Add(gold);
        }
    
        public void _FightMaterial(float y)
        {
            MaterialManager.instance.FightMaterial.Add(y);
        }
    
        public void _FightMat2()
        {
            tempGameObject = GameObject.FindGameObjectWithTag(FightManagerStr);
            print(tempGameObject);
            var b = GameObject.FindGameObjectWithTag(sldValue);
            print(b);
            print(-b.GetComponent<Slider>().value);
            tempGameObject.GetComponent<MaterialBase>().Add(-b.GetComponent<Slider>().value);
        }
    
        public void _ChangeTime(float time)
        {
            MaterialManager.instance.TimeMaterial.Add(time);
        }    
        
        public void __ChangeFoodModifier(float y)
        {
            var characterManager = MaterialManager.instance.CharacterManager;

            foreach (var character in characterManager.Crew)
            {
                if (character.ClassId == ClassIdEnum.Chef)
                {
                    tempGameObject.GetComponent<MaterialBase>().Modifier += y;
                }
            }
        }
        
        public void _ChangeIsHurt(bool test)
        {
            var characterManager = MaterialManager.instance.CharacterManager;

            var randClass = Random.Range(0, characterManager.Crew.Count);
        
            characterManager.Crew[randClass].IsAbleToWork = false;
        }
    
        public void _ChangeFood(float y)
        {
            MaterialManager.instance.FoodMaterial.Add(y);
        }
    
        public void _ChangeWine(float y)
        {
            MaterialManager.instance.WineMaterial.Add(y);
        }
    
        public void _ChangeWood(float y)
        {
            MaterialManager.instance.WoodMaterial.Add(y);
        }
        public void _ChangeMoral(float y)
        {
            MaterialManager.instance.MoraleMaterial.Add(y);
        }
        public void _ChangeFoodConsumption(float y)
        {
            var characterManager = MaterialManager.instance.CharacterManager;

            foreach (var character in characterManager.Crew)
            {
                character.FoodConsumption += y;
            }
        }
    }
}
