using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puntaje : MonoBehaviour
{
    public int score = 0;
    public int maxScore;
    public int pointsPerMatch; 
    public Text puntajeTexto;
    public bool jugadorClick;

    
    public void anadirPuntaje(int matches)
    {
        if(!jugadorClick) 
        {
            return;
        }
    
        int potentialPoints = pointsPerMatch * matches;
        score += Mathf.Min(potentialPoints, maxScore - score); // Asegúrate de no superar el maxScore

        // Actualiza el texto de la puntuación si tienes una UI
        if(puntajeTexto != null)
        {
            puntajeTexto.text = "Score: " + score.ToString();
        }

    }

    public bool ganar(int puntajeRequerido)
    {
        if (score > puntajeRequerido)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    private void Update ()
    {
        if(Input.GetMouseButtonDown(0))
        {
            jugadorClick = true;
        }

    }
}








