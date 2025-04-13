using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class settingFunctions : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] Grain grain;

    [SerializeField] PostProcessVolume postProcessVolume;

    [SerializeField] Slider slider;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
        postProcessVolume = GameObject.FindWithTag("PostProcess").GetComponent<PostProcessVolume>();

        grain = postProcessVolume.profile.GetSetting<Grain>();

        slider.value = 0.01f;
        MainAudio();
        FilmGrain();
    }

    void Update()
    {
        
    }

    public void MainAudio()
    {
        audioSource.volume = slider.value;
        audioSource.Pause();
        audioSource.Play();
    }

    public void FilmGrain()
    {
        grain.intensity.value = slider.value;
    }
}
