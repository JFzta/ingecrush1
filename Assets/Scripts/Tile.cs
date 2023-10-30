using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public string type;

    Match3GameManager gameManager;
    [HideInInspector]
    public SpriteRenderer render;

    public int x, y;

    

    public void Constructor(Match3GameManager GameManager, int X, int Y)
    {        
        x = X;
        y = Y;
        gameManager = GameManager;

        name = string.Format("({0}, {1})", x , y);
        render = GetComponent<SpriteRenderer>();
        render.enabled = Y < gameManager.sizeY;
    }

    public void ChangePosition(int X, int Y)
    {
        x = X;
        y = Y;

        name = string.Format("({0}, {1})", x , y);
        render.enabled = Y < gameManager.sizeY;
      
    }

    void OnMouseDown()
    {
        gameManager.Drag(this);
        this.GetComponent<Animator>().SetBool("selected", true);
        print(string.Format("Drag {0},{1}", x , y));
    }
    void OnMouseUp() {
        this.GetComponent<Animator>().SetBool("selected", false);
    }
    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {
            gameManager.Drop(this);
            print(string.Format("Drop {0},{1}", x, y));
        }
    }
}
