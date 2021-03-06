using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// custom namespaces
using Utilities;
public class GridCreator : MonoBehaviour
{
    public BaseGrid[,] gridArray => _gridArray;
    public BaseGrid[] mineArray => _mineArray;
    public int gridSize => _gridSize;
    private int _gridSize;
    private BaseGrid[,] _gridArray;
    private BaseGrid[] _mineArray;

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

    public bool CreateGrid()
    {
        bool isGridCreated;

        _gridArray = new BaseGrid[_gridSize, _gridSize];

        GameObject parentGridObject = new GameObject();
        parentGridObject.name = "Grid Array";
        Utilities.GridSystemUtilities.CreatePrefab(parentGridObject);

        if (CreateGridItem(parentGridObject) && PlaceMines())
        {
            DisplayGrid();
            isGridCreated = true;
        }
        else
        {
            isGridCreated = false;
        }

        return isGridCreated;

    }

    private bool CreateGridItem(GameObject parent)
    {
        int numberOfGrid = 0;
        for (int i = 0; i < _gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < _gridArray.GetLength(1); j++)
            {
                string newGridName = i.ToString() + j.ToString();
                GameObject gameObject = Utilities.GridSystemUtilities.CreateGameObject(parent, newGridName);
                Utilities.GridSystemUtilities.SetSpriteRenderer(gameObject, _defaultSprite);
                gameObject.AddComponent<BoxCollider2D>();

                BaseGrid newGrid = new BaseGrid(gameObject.name, gameObject.GetComponent<SpriteRenderer>(), gameObject.transform);
                _gridArray[i, j] = newGrid;
                numberOfGrid++;
            }
        }

        if (numberOfGrid == _gridSize * _gridSize)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool PlaceMines()
    {
        _mineArray = new BaseGrid[_numberOfMines];
        int mineCount = 0;
        
        for (int i = 0; i < _numberOfMines; i++)
        {
            int randX = Random.Range(0, _gridSize);
            int randY = Random.Range(0, _gridSize);

            if (_gridArray[randX, randY].gridType == BaseGrid.GridType.Default)
            {
                _gridArray[randX, randY].SetGridType(BaseGrid.GridType.Mine);
                _mineArray[mineCount] = _gridArray[randX, randY];
                mineCount++;
            }
            else
            {
                i--;
            }
        }

        if (mineCount == _numberOfMines)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void DisplayGrid()
    {
        float width = (_gridArray.GetLength(0)) * _distanceX;

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
