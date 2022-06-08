using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuSliders : MonoBehaviour
{
    [SerializeField] private AudioSource _audioListener;
    public void OnSliderValueChanged(float value)
    {
        _audioListener.volume = value;
    }
}
