using System;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GridSettings gridSettings;
    public SpriteCollection spriteCollection;
    GridCreator grid;

    public bool isGameReady = false;
    void Start()
    {
        grid = GetComponent<GridCreator>();
        grid.SetGridSettings(gridSettings);
        isGameReady = grid.CreateGrid();
    }

    void Update()
    {
        // if (isGameReady)
        // {
        //     Debug.Log("Game Start!");
        // }

        if (isGameReady)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 cubeRay = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D cubeHit = Physics2D.Raycast(cubeRay, Vector2.zero);

                if (cubeHit)
                {
                    Debug.Log("We hit " + cubeHit.collider.name);
                    CheckGridType(cubeHit);
                }
            }
        }

    }

    public void CheckGridType(RaycastHit2D hit)
    {
        // Turn the grid name to integer
        int gridX = Int32.Parse(hit.collider.name.Substring(0, 1));
        int gridY = Int32.Parse(hit.collider.name.Substring(1));

        if (grid.gridArray[gridX, gridY].gridType == BaseGrid.GridType.Mine)
        {
            grid.gridArray[gridX, gridY].GridSprite = spriteCollection.mine;
            Debug.Log(grid.gridArray[gridX, gridY].GridSprite);
            // Debug.Log("MINE!!");
        }

        // Debug.Log("("+ gridX + " "+  gridY + ")");

        // grid.gridArray[gridX,gridY]
    }
}
