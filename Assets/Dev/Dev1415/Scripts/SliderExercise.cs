using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class SliderExercise : MonoBehaviour
{
    //[SerializeField] private 
    public enum VolumeType
    {
        SFX,
        BGM
    }

    [SerializeField] private VolumeType volumeType;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI text;



    public void Awake()
    {
        slider.onValueChanged.AddListener(OnSliderEvent);
    }



    private void Start(){
        switch (volumeType)
        {
            case VolumeType.SFX:
                slider.value = AudioManager.SFXVolume;

                break;
            case VolumeType.BGM:
                slider.value = AudioManager.BGMVolume;
                break;
        }
    }


    public void OnSliderEvent(float value) // 오디오 매니저에서 sfxVolume = 1f , bgmVolume = 1 두개 0.5f 로 바꿔라
    {
        switch (volumeType)
        {
            case VolumeType.SFX:
                AudioManager.SFXVolume = value;
                Debug.Log("SFX Volume: " + AudioManager.SFXVolume);
                text.text = $"SFX {(value * 100):F0}%";

                break;
            case VolumeType.BGM:
                AudioManager.BGMVolume = value;
                Debug.Log("BGM Volume: " + AudioManager.BGMVolume);
                text.text = $"BGM {(value * 100):F0}%";


                break;
        }
    }

}
