using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShopScript : MonoBehaviour
{
    public MaterialBase material;

    public MaterialBase _money;

    public float neededMoney;
    
    public void AddFood(float amount)
    {

	if (_money.Value > Mathf.Abs(neededMoney))
        {
        material.Add(amount);
        _money.Add(neededMoney);
            return;
        }
    }
}
