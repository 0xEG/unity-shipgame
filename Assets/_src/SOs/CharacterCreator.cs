using UnityEngine;
using PCC;

[CreateAssetMenu(fileName = "New Class", menuName = "Create Class")]
public class CharacterCreator : ScriptableObject
{
    public new string name;

    [SerializeField] public ClassIdEnum ClassId;

    public Sprite artwork;
    
    public int FoodConsumption = 5;
    public float BuffModifier = 1.0f;


    public void info()
    {
        Debug.Log("Name: " + name + " Food consumption: "+ FoodConsumption + " Buff Modifier: " + BuffModifier);
    }
}
