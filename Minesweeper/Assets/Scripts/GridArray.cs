using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// custom namespaces
using Utilities;
public class GridArray : MonoBehaviour
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
        Utilities.GridSystemUtilities.CreatePrefab(parentGridObject);

        CreateGridItem(parentGridObject);
        ShowGrid();
    }

    private void CreateGridItem(GameObject parent)
    {
        for (int i = 0; i < _gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < _gridArray.GetLength(1); j++)
            {
                string newGridName = "grid_" + i.ToString() + j.ToString();
                GameObject gameObject = Utilities.GridSystemUtilities.CreateGameObject(parent, newGridName);
                Utilities.GridSystemUtilities.SetSpriteRenderer(gameObject, defaultSprite);
                
                BaseGrid newGrid = new BaseGrid(gameObject.name, gameObject.GetComponent<SpriteRenderer>().sprite, gameObject.transform);
                _gridArray[i, j] = newGrid;
            }
        }
    }

    private void ShowGrid()
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
