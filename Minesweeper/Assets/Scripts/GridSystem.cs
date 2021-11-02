using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class GridSystem : MonoBehaviour
{

    public int gridSize;

    private int _gridSize => gridSize;
    private BaseGrid[,] gridArray;

    [ContextMenu("Create Grid")]
    public void CreateGrid()
    {
        GameObject parentGridObject = new GameObject();
        parentGridObject.name = "Grid Array";
        CreatePrefab(parentGridObject);

        gridArray = new BaseGrid[_gridSize, _gridSize];

        for (int i = 0; i < gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < gridArray.GetLength(1); j++)
            {

                GameObject newGridObject = new GameObject();
                newGridObject.name = "grid_" + i.ToString() + j.ToString();
                newGridObject.AddComponent<SpriteRenderer>();
                newGridObject.transform.SetParent(parentGridObject.transform);

                PrefabUtility.ApplyPrefabInstance(parentGridObject, InteractionMode.UserAction);

                BaseGrid newGrid = new BaseGrid(newGridObject.name, newGridObject.GetComponent<SpriteRenderer>().sprite);
                gridArray[i, j] = newGrid;
            }
        }

        Debug.Log(gridArray[0, 0].GridName);
        Debug.Log(gridArray[0, 0].GridSprite);

    }

    public void CreatePrefab(GameObject gameObject)
    {
        string localPath = "Assets/Prefabs/" + gameObject.name + ".prefab";

        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

        PrefabUtility.SaveAsPrefabAssetAndConnect(gameObject, localPath, InteractionMode.UserAction);
    }

}
