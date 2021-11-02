using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class GridSystem : MonoBehaviour
{
    public int gridSize;
    public Sprite defaultSprite;


    [Header("Sprite Distance")]
    [Range(0f, 5f)]
    public float distanceX;
    [Range(0f, 5f)]
    public float distanceY;

    private int _gridSize => gridSize;
    private BaseGrid[,] _gridArray;


    [ContextMenu("Create Grid")]
    public void CreateGrid()
    {
        _gridArray = new BaseGrid[_gridSize, _gridSize];

        GameObject parentGridObject = new GameObject();
        parentGridObject.name = "Grid Array";
        CreatePrefab(parentGridObject);

        CreateGridItem(parentGridObject);
        ShowGrid();
    }

    public void CreateGridItem(GameObject parent)
    {
        for (int i = 0; i < _gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < _gridArray.GetLength(1); j++)
            {
                string newGridName = "grid_" + i.ToString() + j.ToString();
                GameObject gameObject = CreateGameObject(parent, newGridName);

                BaseGrid newGrid = new BaseGrid(gameObject.name, gameObject.GetComponent<SpriteRenderer>().sprite, gameObject.transform);
                _gridArray[i, j] = newGrid;
            }
        }
    }
    
    public void CreatePrefab(GameObject gameObject)
    {
        string localPath = "Assets/Prefabs/" + gameObject.name + ".prefab";

        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

        PrefabUtility.SaveAsPrefabAssetAndConnect(gameObject, localPath, InteractionMode.UserAction);
    }

    public GameObject CreateGameObject(GameObject parent, string name)
    {
        GameObject newGameObject = new GameObject();
        newGameObject.name = name;
        var spriteRenderer = newGameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = defaultSprite;
        newGameObject.transform.SetParent(parent.transform);

        PrefabUtility.ApplyPrefabInstance(parent, InteractionMode.UserAction);
        return newGameObject;

    }
    public void ShowGrid()
    {
        float width = (_gridArray.GetLength(0)) * distanceX;

        float currentX = 0;
        float currentY = 0;

        for (int i = 0; i < _gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < _gridArray.GetLength(1); j++)
            {
                if (currentX != width)
                {
                    Vector3 position = _gridArray[i, j].GridTransform.position;
                    position = new Vector3(currentX, currentY, 0);
                    _gridArray[i, j].GridTransform.position = position;

                    currentX = currentX + distanceX;

                }
                else
                {
                    currentX = 0;
                    currentY = currentY - distanceY;

                    Vector3 position = _gridArray[i, j].GridTransform.position;
                    position = new Vector3(0, currentY, 0);
                    _gridArray[i, j].GridTransform.position = position;

                    currentX = currentX + distanceX;

                }
            }
        }
    }
}
