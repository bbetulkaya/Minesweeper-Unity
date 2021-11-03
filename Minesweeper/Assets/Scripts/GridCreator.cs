using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// custom namespaces
using Utilities;
public class GridCreator : MonoBehaviour
{
    private int _gridSize;
    private BaseGrid[,] _gridArray;

    private float _distanceX;
    private float _distanceY;

    private Sprite _defaultSprite;
    private int _numberOfMines;

    public void SetGridSettings(GridSettings settings)
    {
        _gridSize = settings.gridSize;
        _distanceX = settings.distanceX;
        _distanceY = settings.distanceY;
        _defaultSprite = settings.defaultSprite;
        _numberOfMines = settings.numberOfMines;

        _gridArray = new BaseGrid[_gridSize, _gridSize];
    }

    public void CreateGrid()
    {
        _gridArray = new BaseGrid[_gridSize, _gridSize];

        GameObject parentGridObject = new GameObject();
        parentGridObject.name = "Grid Array";
        Utilities.GridSystemUtilities.CreatePrefab(parentGridObject);

        CreateGridItem(parentGridObject);
        PlaceMines();
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
                Utilities.GridSystemUtilities.SetSpriteRenderer(gameObject, _defaultSprite);

                BaseGrid newGrid = new BaseGrid(gameObject.name, gameObject.GetComponent<SpriteRenderer>().sprite, gameObject.transform);
                _gridArray[i, j] = newGrid;
            }
        }
    }

    private void PlaceMines()
    {

        for (int i = 0; i <_numberOfMines; i++)
        {
            int randX = Random.Range(0, _gridSize);
            int randY = Random.Range(0, _gridSize);

            _gridArray[randX, randY].SetGridType(BaseGrid.GridType.Mine);
            Debug.Log(_gridArray[randX, randY].gridType);

        }

    }

    private void ShowGrid()
    {
        float width = (_gridArray.GetLength(0)) * _distanceX;

        float currentX = 0;
        float currentY = 0;

        for (int i = 0; i < _gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < _gridArray.GetLength(1); j++)
            {
                Debug.Log(_gridArray[i, j].gridType);
                if (currentX != width)
                {
                    Vector3 position = _gridArray[i, j].GridTransform.position;
                    position = new Vector3(currentX, currentY, 0);
                    _gridArray[i, j].GridTransform.position = position;

                    currentX = currentX + _distanceX;


                }
                else
                {
                    currentX = 0;
                    currentY = currentY - _distanceY;

                    Vector3 position = _gridArray[i, j].GridTransform.position;
                    position = new Vector3(0, currentY, 0);
                    _gridArray[i, j].GridTransform.position = position;

                    currentX = currentX + _distanceX;

                }
            }
        }
    }
}
