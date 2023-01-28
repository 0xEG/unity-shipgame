using _src.MaterialSystems;
using UnityEngine;

namespace _src.Managers
{
    public class MaterialManager : MonoSingleton<MaterialManager>
    {
        [field: SerializeField] public MaterialBase FoodMaterial { get; private set; }
        [field: SerializeField] public MaterialBase TimeMaterial { get; private set; }
        [field: SerializeField] public MaterialBase MoraleMaterial { get; private set; }
        [field: SerializeField] public MaterialBase GoldMaterial { get; private set; }
        [field: SerializeField] public MaterialBase FightMaterial { get; private set; }
        [field: SerializeField] public MaterialBase WineMaterial { get; private set; }
        [field: SerializeField] public MaterialBase WoodMaterial { get; private set; }

        [field: SerializeField] public CharacterManager CharacterManager { get; private set; }
        
        [Tooltip("1. Element must be food slider")]
        [SerializeField] private GetDailyMats[] dailyMats;
        
        public void OnDayEnd()
        {
            if (FoodMaterial.Value > 0)
            {
                FoodMaterial.Add(DailyFoodConsumption());
            }
            else
            {
                MoraleMaterial.Add(-10);
            }
            if (TimeMaterial.Value <= 0 || TimeMaterial.Value <= float.Epsilon)
            {
                GameManager.Instance.UpdateGameState(GameState.Beast);
            }
            else
            {
                GameManager.Instance.UpdateGameState(GameState.Save);
            }
        }
        
        private float DailyFoodConsumption()
        {
            var totalFoodConsumption = .0f;
            var foodModifier = dailyMats[0].Modifier;

            totalFoodConsumption = CharacterManager.TotalFoodConsumption();

            if (foodModifier != 0)
                return -totalFoodConsumption / dailyMats[0].Modifier;

            ReduceMorale();
            
            return 0;
        }

        private void ReduceMorale()
        {
            MoraleMaterial.Add(-dailyMats[0].Morale);
        }
    }
}