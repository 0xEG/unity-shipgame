using UnityEngine;

namespace _src.Menu
{
    public class MenuSliders : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioListener;
        public void OnSliderValueChanged(float value)
        {
            _audioListener.volume = value;
        }
    }
}
