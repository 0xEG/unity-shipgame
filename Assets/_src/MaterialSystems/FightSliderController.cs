using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _src.MaterialSystems
{
    enum Values
    {
        Low = 1,
        Medium,
        High
    }
    public class FightSliderController : MonoBehaviour
    {
        [SerializeField] private Slider _timeSlider;
        [SerializeField] private Slider _riskSlider;
        [SerializeField] private Slider _warPower;

        [Space] 
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private TextMeshProUGUI _riskText;
        [SerializeField] private TextMeshProUGUI _warText;
        private int _time;
        private int _risk;
        private int _fight;
    
        private void Start()
        {
            _timeSlider.onValueChanged.AddListener((v)=>
            {
                _time = (int)v;

            });
            _riskSlider.onValueChanged.AddListener((v)=>
            {
                _risk = (int)v;

            });
            _warPower.onValueChanged.AddListener((v)=>
            {
                _fight = (int)v;

            });
        }

        public float DetermineFightPower(float fightDiff)
        {
            var time = _time * 3;

            float x= ((2*_fight*(time/10+1))-_risk-fightDiff/5);

            x = Mathf.Clamp(x, 1, 10);
            return x;
        }
    }
}