using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    // Public variables
    [Header("Grid Size")]
    public int gridSize;


    private int _gridSize => gridSize;
    private BaseGrid[,] gridArray;

    [ContextMenu("Create Grid")]
    public void CreateGrid()
    {
        GameObject parentGridObject = new GameObject();
        parentGridObject.name = "Grid Array";

        gridArray = new BaseGrid[_gridSize, _gridSize];

        for (int i = 0; i < gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < gridArray.GetLength(1); j++)
            {

                GameObject newGridObject = new GameObject();
                newGridObject.name = "grid_" + i.ToString() + j.ToString();
                newGridObject.AddComponent<SpriteRenderer>();
                newGridObject.transform.SetParent(parentGridObject.transform);

                BaseGrid newGrid = new BaseGrid(newGridObject.name, newGridObject.GetComponent<SpriteRenderer>());

                gridArray[i, j] = newGrid;
            }
        }

        Debug.Log(gridArray[0, 0].GridName);
        Debug.Log(gridArray[0, 0].SpriteRenderer);

    }

}
