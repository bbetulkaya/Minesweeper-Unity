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

        if (!isGameReady)
        {
            Debug.Log("Game board is not ready!");
        }
    }

    void Update()
    {
        if (isGameReady)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 cubeRay = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D cubeHit = Physics2D.Raycast(cubeRay, Vector2.zero);

                if (cubeHit)
                {
                    Debug.Log("We hit " + cubeHit.collider.name);
                    CheckPlayerMove(cubeHit);
                }
            }
        }
        // For Debug!
        if (Input.GetMouseButtonDown(1))
        {
            ShowMinePlace();
        }

    }

    public void CheckPlayerMove(RaycastHit2D hit)
    {
        // Turn the grid name to integer
        int gridX = Int32.Parse(hit.collider.name.Substring(0, 1));
        int gridY = Int32.Parse(hit.collider.name.Substring(1));

        if (grid.gridArray[gridX, gridY].gridType == BaseGrid.GridType.Mine)
        {
            DisplayAllMinePlace();

            Debug.Log("Game Over!");
        }
        else
        {
            SetGridSprite(grid.gridArray[gridX, gridY], adjacentMines(gridX, gridY));

            FFuncover(gridX, gridY, new bool[grid.gridSize, grid.gridSize]);

            if (isFinished())
            {
                Debug.Log("YOU WON!");
            }

        }
    }

    public void DisplayAllMinePlace()
    {
        for (int i = 0; i < grid.mineArray.Length; i++)
        {
            grid.mineArray[i].GridSprite = spriteCollection.mine;
        }
    }
    public void ShowMinePlace()
    {
        for (int i = 0; i < grid.mineArray.Length; i++)
        {
            Debug.Log(grid.mineArray[i].GridName);
        }
    }
    // Find out if a mine is at the coordinates
    public bool mineAt(int x, int y)
    {
        // Coordinates in range? Then check for mine.
        if (x >= 0 && y >= 0 && x < grid.gridSize && y < grid.gridSize)
        {
            if (grid.gridArray[x, y].gridType == BaseGrid.GridType.Mine)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    // Count adjacent mines for an element
    public int adjacentMines(int x, int y)
    {
        int count = 0;

        if (mineAt(x, y + 1)) ++count; // top
        if (mineAt(x + 1, y + 1)) ++count; // top-right
        if (mineAt(x + 1, y)) ++count; // right
        if (mineAt(x + 1, y - 1)) ++count; // bottom-right
        if (mineAt(x, y - 1)) ++count; // bottom
        if (mineAt(x - 1, y - 1)) ++count; // bottom-left
        if (mineAt(x - 1, y)) ++count; // left
        if (mineAt(x - 1, y + 1)) ++count; // top-left

        return count;
    }

    public void SetGridSprite(BaseGrid grid, int index)
    {
        grid.GridSprite = spriteCollection.number[index];
    }

    // Flood Fill empty elements
    public void FFuncover(int x, int y, bool[,] visited)
    {
        // Coordinates in Range?
        if (x >= 0 && y >= 0 && x < grid.gridSize && y < grid.gridSize)
        {
            // visited already?
            if (visited[x, y])
                return;

            // uncover element
            SetGridSprite(grid.gridArray[x, y], adjacentMines(x, y));

            // close to a mine? then no more work needed here
            if (adjacentMines(x, y) > 0)
                return;

            // set visited flag
            visited[x, y] = true;

            // recursion
            FFuncover(x - 1, y, visited);
            FFuncover(x + 1, y, visited);
            FFuncover(x, y - 1, visited);
            FFuncover(x, y + 1, visited);
        }
    }

    public bool isFinished()
    {
        // Try to find a covered element that is no mine
        foreach (BaseGrid grid in grid.gridArray)
            if (isCovered(grid) && (grid.gridType != BaseGrid.GridType.Mine))
                return false;
        // There are none => all are mines => game won.
        return true;
    }
    // Is it still covered?
    public bool isCovered(BaseGrid grid)
    {
        return grid.GridSprite.name == "defaultSprite";
    }
}