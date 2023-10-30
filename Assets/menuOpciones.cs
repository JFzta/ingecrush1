using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class menuOpciones : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

   public void pantallaCompleta (bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }

    public void cambiarVolumen (float volumen) 
    {
        audioMixer.SetFloat("volumen", volumen);
    }

    public void cambiarCalidad (int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
        
}
