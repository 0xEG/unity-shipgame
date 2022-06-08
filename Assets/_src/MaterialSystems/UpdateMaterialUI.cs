using TMPro;
using UnityEngine;

public class UpdateMaterialUI : MonoBehaviour
{
    [SerializeField] private bool doesHaveConsumption;
    private MaterialBase _materialBase;
    [SerializeField] private TMP_Text MaterialText = null;

    private void Awake()
    {
        _materialBase = GetComponent<MaterialBase>();
    }

    private void OnEnable()
    {
        UpdateText(_materialBase.Value);
        _materialBase.OnValueChanged += HandeValueChanged;
    }
    private void OnDisable()
    {
        _materialBase.OnValueChanged -= HandeValueChanged;
    }

    private void HandeValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (doesHaveConsumption)
        { 
            UpdateText(e.Value);
        }
        else
        {
            SacrifaceScreen();
        }
    }

    private void SacrifaceScreen()
    {
        
    }
    private void UpdateText(in float eValue)
    {
        MaterialText.text = eValue.ToString();
    }
}
