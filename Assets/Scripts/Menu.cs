using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{


    // Start is called before the first frame update
    public void ButtomJugar()
    {
        //Aca tendremos que enlazarlo con el ultimo nivel que haya pasado el jugador
        //En caso de ser su primera vez jugando entonces entrara al nivel 1
        SceneManager.LoadScene("Nivel1");

        int ultimoNivelDisponible = PlayerPrefs.GetInt("NivelJugador", 1);
        SceneManager.LoadScene("Nivel" + ultimoNivelDisponible);

    }

    // Update is called once per frame
    public void ButtomSalir()
    {
        //Aca pondremos la accion para salir de la aplicacion
        Debug.Log("Salimos de la aplicacion");
        Application.Quit();

    }

    public void VolverMenu()
    {
        //Aca tendremos que enlazarlo con el ultimo nivel que haya pasado el jugador
        //En caso de ser su primera vez jugando entonces entrara al nivel 1
        SceneManager.LoadScene("MenuScene 1");

    }

    public string loadingSceneName = "Nivel1";
    public float fakeLoadingTime = 2.0f; // Tiempo de carga simulado en segundos


    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene("Carga1"); // Carga la escena de carga primero

        // Simula un retraso antes de cargar la escena real
        Invoke("LoadAfterFakeTime", fakeLoadingTime);
    }

    public void LoadAfterFakeTime()
    {
        // Carga la escena del nivel real después del retraso simulado
        SceneManager.LoadScene("Nivel1"); // Reemplaza "Nivel1" con el nombre de tu escena de nivel
    }




}










