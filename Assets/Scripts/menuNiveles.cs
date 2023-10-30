using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuNiveles : MonoBehaviour
{
    public void cambiarEscena(string nombre)
    {
        if(controladorNiveles.instancia!=null)
        {
            controladorNiveles.instancia.aumentarNiveles();
        }
        SceneManager.LoadScene(nombre);
    }
}
