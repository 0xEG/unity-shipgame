using TMPro;
using UnityEngine;

namespace _src.MaterialSystems
{
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
    
        public float Morale { get; private set; }
        public float Modifier { get; private set; }
    
        private void Start()
        {
            SliderValue(Values.Medium);
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
        private void SliderValue(Values sliderValue)
        {
            switch (sliderValue)
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
}