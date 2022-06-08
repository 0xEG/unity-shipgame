using TMPro;
using UnityEngine;

public class GetDailyMats : MonoBehaviour
{
    enum Values
    {
        None = 0,
        Low,
        Medium,
        High
    }

    [SerializeField] private TextMeshProUGUI sliderText;
    [SerializeField] public float Morale = 0;

    public float Modifier { get; set; }
    private void Awake()
    {
        sliderText.text = "Medium";
        Modifier = 2;
    }

    public void OnSliderValueChanged(float value)
    {
        SliderValue(value);
    }

    private void SliderValue(float sliderValue)
    {
        switch ((Values) sliderValue)
        {
            case Values.None:
                sliderText.text = "None";
                Modifier = 0;
                Morale = 10f;
                break;
            case Values.Low:
                sliderText.text = "Low";
                Modifier = 4;
                Morale = 5f;
                break;
            case Values.Medium:
                sliderText.text = "Normal";
                Modifier = 2;
                Morale = 2.5f;
                break;
            case Values.High:
                sliderText.text = "High";
                Modifier = 1;
                Morale = 0f;
                break;
        } 
    }
}