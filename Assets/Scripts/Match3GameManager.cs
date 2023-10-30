using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Clase principal que gestiona el juego de combinación de 3 (Match-3).
/// </summary>

public class Match3GameManager : MonoBehaviour
{
    bool ban = true;
    // Matriz que contiene todos los tiles del juego.
    Tile[,] grid;

    // Dimensiones de la matriz/grid del juego.
    [SerializeField]
    int sizeX;

    public int sizeY;

    // Array de los prefabs de los tiles que pueden ser instanciados.
    [SerializeField]
    Tile[] tilesPrefabs;

    // Controladores para las acciones del usuario y movimiento de tiles.
    bool CanMove = false;
    bool fast = true;

    // Referencia al sistema de puntuación.
    public Puntaje puntaje;

    public AnimationClip[] tileAnimations;

    /// <summary>
    /// Inicializa el juego al inicio.
    /// </summary>
    /// 
    [Header("animación")]

    public Animator animator;

    string lvl1 = "Nivel1";
    string lvl2 = "Nivel2";
    string lvl3 = "Nivel3";
    string lvl4 = "Nivel4";
    string lvl5 = "Nivel5";


    void Start()
    {
        // Inicializar matriz de tiles y llenarla.
        grid = new Tile[sizeX, sizeY * 2];
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                InstantiateTile(i, j);
            }
        }

        string lvl1 = "Nivel1";
        string lvl2 = "Nivel2";
        string lvl3 = "Nivel3";
        string lvl4 = "Nivel4";
        string lvl5 = "Nivel5";


        Check();

        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Método Update para depuración: imprime el estado de la matriz/grid cuando se presiona la barra espaciadora.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string s = "";
            for (int j = sizeY * 2 - 1; j >= 0; j--)
            {

                for (int i = sizeX - 1; i >= 0; i--)
                {
                    if (grid[i, j] != null)
                        s += grid[i, j].name;
                    else
                        s += "NULL";
                }
                s += "\n";
            }
            print(s);
        }




        if (SceneManager.GetActiveScene().name == lvl1)
        {

            if (puntaje.ganar(10000) && ban)    //PUNTAJE crear una variable BAN para que no se siga repitiendo
            {
                Debug.Log("ganó");
                ban = false;
                SceneManager.LoadScene("VictoriaEscene");

            }
        }


        if (SceneManager.GetActiveScene().name == lvl2)
        {
            if (puntaje.ganar(15000) && ban)    //PUNTAJE crear una variable BAN para que no se siga repitiendo
            {
                Debug.Log("ganó");
                ban = false;

                SceneManager.LoadScene("VictoriaEscene 2");
            }
        }
        if (SceneManager.GetActiveScene().name == lvl3)
        {

            if (puntaje.ganar(20000) && ban)    //PUNTAJE crear una variable BAN para que no se siga repitiendo
            {
                Debug.Log("ganó");
                ban = false;
                SceneManager.LoadScene("VictoriaEscene 3");
            }
        }

        if (SceneManager.GetActiveScene().name == lvl4)
        {
            if (puntaje.ganar(25000) && ban)    //PUNTAJE crear una variable BAN para que no se siga repitiendo
            {
                Debug.Log("ganó");
                ban = false;
                SceneManager.LoadScene("VictoriaEscene 4");
            }
        }


    }

    int dragX = -1;
    int dragY = -1;

    /// <summary>
    /// Comienza el proceso de arrastrar un tile.
    /// </summary>
    public void Drag(Tile tile)
    {
        if (!CanMove)
            return;
        dragX = tile.x;
        dragY = tile.y;
    }
    /// <summary>
    /// Finaliza el proceso de arrastrar y deja caer el tile.
    /// </summary>
    public void Drop(Tile tile)
    {
        if (!CanMove)
            return;

        if (dragX == -1 || dragY == -1)
            return;

        SwapTiles(dragX, dragY, tile.x, tile.y);

        dragX = -1;
        dragY = -1;
    }

    /// <summary>
    /// Intercambia dos tiles y verifica si hay coincidencias.
    /// </summary>
    void SwapTiles(int x1, int y1, int x2, int y2)
    {
        fast = false;
        if (x1 == x2 && y1 == y2)
            return;
        MoveTile(x1, y1, x2, y2);

        List<Tile> TilesToCheck = CheckHorizontalMatches();
        TilesToCheck.AddRange(CheckVerticalMatches());

        if (TilesToCheck.Count == 0)
        {
            MoveTile(x1, y1, x2, y2);
        }
        Check();
    }

    /// <summary>
    /// Verifica coincidencias horizontales y verticales, y actualiza el puntaje.
    /// </summary>
    void Check()
    {
        List<Tile> TilesToDestroy = CheckHorizontalMatches();
        TilesToDestroy.AddRange(CheckVerticalMatches());

        TilesToDestroy = TilesToDestroy.Distinct().ToList();

        bool sw = TilesToDestroy.Count == 0;

        puntaje.anadirPuntaje(TilesToDestroy.Count / 3);

        for (int i = 0; i < TilesToDestroy.Count; i++)
        {
            if (TilesToDestroy[i] != null)
            {

                foreach (Tile tile in TilesToDestroy)
                {
                    Animator tileAnim = tile.GetComponent<Animator>();
                    tileAnim.SetBool("destruir", true);
                }
                Destroy(TilesToDestroy[i].gameObject, .5f);
                InstantiateTile(TilesToDestroy[i].x, TilesToDestroy[i].y + sizeY);
            }
        }

        if (!sw)
            StartCoroutine(Gravity());
    }

    /// <summary>
    /// Aplica gravedad para que los tiles caigan.
    /// </summary>
    IEnumerator Gravity()
    {
        bool Sw = true;
        while (Sw)
        {
            CanMove = false;
            Sw = false;
            for (int j = 0; j < sizeY * 2; j++)
            {
                for (int i = 0; i < sizeX; i++)
                {
                    if (Fall(i, j))
                    {
                        Sw = true;
                    }
                }

                if (j <= sizeY && !fast) //<-Wait
                    yield return null;
            }
        }
        yield return null;
        CanMove = true;
        Check();

    }

    /// <summary>
    /// Hace que un tile en una posición específica caiga.
    /// </summary>
    bool Fall(int x, int y)
    {
        if (x < 0 || y <= 0 || x >= sizeX || y >= sizeY * 2) // <- SizeY * 2
            return false;
        if (grid[x, y] == null)
            return false;
        if (grid[x, y - 1] != null)
            return false;

        MoveTile(x, y, x, y - 1);
        return true;
    }

    /// <summary>
    /// Comprueba las coincidencias horizontales.
    /// </summary>
    List<Tile> CheckHorizontalMatches()
    {
        List<Tile> TilesToCheck = new List<Tile>();
        List<Tile> TilesToReturn = new List<Tile>();
        string Type = "";

        for (int j = 0; j < sizeY; j++)
        {
            for (int i = 0; i < sizeX; i++)
            {
                if (grid[i, j].type != Type)
                {
                    if (TilesToCheck.Count >= 3)
                    {
                        TilesToReturn.AddRange(TilesToCheck);
                    }
                    TilesToCheck.Clear();
                }
                Type = grid[i, j].type;
                TilesToCheck.Add(grid[i, j]);
            }

            if (TilesToCheck.Count >= 3)
            {
                TilesToReturn.AddRange(TilesToCheck);
            }
            TilesToCheck.Clear();
        }
        return TilesToReturn;
    }

    /// <summary>
    /// Comprueba las coincidencias verticales.
    /// </summary>
    List<Tile> CheckVerticalMatches()
    {
        List<Tile> TilesToCheck = new List<Tile>();
        List<Tile> TilesToReturn = new List<Tile>();
        string Type = "";

        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                if (grid[i, j].type != Type)
                {
                    if (TilesToCheck.Count >= 3)
                    {
                        TilesToReturn.AddRange(TilesToCheck);
                    }
                    TilesToCheck.Clear();
                }
                Type = grid[i, j].type;
                TilesToCheck.Add(grid[i, j]);
            }

            if (TilesToCheck.Count >= 3)
            {
                TilesToReturn.AddRange(TilesToCheck);
            }
            TilesToCheck.Clear();
        }
        return TilesToReturn;
    }

    /// <summary>
    /// Mueve un tile de una posición a otra.
    /// </summary>
    void MoveTile(int x1, int y1, int x2, int y2)
    {
        if (grid[x1, y1] != null)
            grid[x1, y1].transform.position = new Vector3(x2, y2);


        if (grid[x2, y2] != null)
            grid[x2, y2].transform.position = new Vector3(x1, y1);


        Tile temp = grid[x1, y1];
        grid[x1, y1] = grid[x2, y2];
        grid[x2, y2] = temp;

        if (grid[x1, y1] != null)
            grid[x1, y1].ChangePosition(x1, y1);
        if (grid[x2, y2] != null)
            grid[x2, y2].ChangePosition(x2, y2);

    }

    /// <summary>
    /// Crea y coloca un nuevo tile en la matriz/grid.
    /// </summary>
    void InstantiateTile(int x, int y)
    {
        Tile go = Instantiate(
                    tilesPrefabs[Random.Range(0, tilesPrefabs.Length)],
                    new Vector3(x, y),
                    Quaternion.identity,
                    transform // <- go.transform.SetParent(transform) (Vieja version ); 
                    ) as Tile;

        go.Constructor(this, x, y);
        grid[x, y] = go;

    }

    public void PlayHorizontalMovementAnimation()
    {
        if (animator != null)
        {
            animator.Play("movimientoHorizontal");

        }

    }

}
