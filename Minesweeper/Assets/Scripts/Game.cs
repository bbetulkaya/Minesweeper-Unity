using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GridSettings gridSettings;
    GridCreator grid;

    void Start()
    {
        grid = GetComponent<GridCreator>();
        grid.SetGridSettings(gridSettings);
        grid.CreateGrid();

        // for (int i = 0; i < 10; i++)
        // {
        //     PlaceMines();
        // }
    }

    // void PlaceMines()
    // {
    //     int x = Random.Range(0, 9);
    //     int y = Random.Range(0, 9);

    //     if (grid[x, y] == null)
    //     {
    //         GameObject mine = new GameObject();
    //         grid[x, y] = mine;

    //         Debug.Log("(" + x + "," + y + ")");
    //     }
    //     else
    //     {
    //         PlaceMines();
    //     }
    // }

}
