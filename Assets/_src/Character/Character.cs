using UnityEngine;

namespace PCC{
    public enum ClassIdEnum{
        Chef = 0,
        Carpenter,
        Musician,
        Navigator,
        Gunner,
        Merchant,
        None
    }

    public class Character
    {
        public string Name {get; }
        public ClassIdEnum ClassId {get; }
        public float BuffModifier { get;set; }
        public float FoodConsumption { get; set; }
        public bool IsAbleToWork {get; set; }
        public bool IsCrazy { get; set; }
        public Sprite CharacterSprite { get; set; }
        

        #region Constructer
        public Character(string name, ClassIdEnum classId, float buffModifier  , float foodConsumption)
        {
            Name = name;
            ClassId = classId;
            BuffModifier = buffModifier;
            FoodConsumption = foodConsumption;
        }
        public Character(string name, ClassIdEnum classId, float buffModifier, float foodConsumption, bool isAbleToWork)
        {
            Name = name;
            ClassId = classId;
            BuffModifier = buffModifier;
            FoodConsumption = foodConsumption;
            this.IsAbleToWork = isAbleToWork;
        }
        public Character(string name, ClassIdEnum classId, float buffModifier, float foodConsumption, bool isAbleToWork, bool isCrazy)
        {
            Name = name;
            ClassId = classId;
            FoodConsumption = foodConsumption;
            BuffModifier = buffModifier;
            this.IsAbleToWork = isAbleToWork;
            this.IsCrazy = isCrazy;
        }
        #endregion
    }
}