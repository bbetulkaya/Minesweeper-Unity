using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGrid
{
    public enum GridType
    {
        Mine,
        Number,
        Space
    }

    public string GridName => _gridName;
    public Sprite GridSprite => _gridSprite;
    public Transform GridTransform => _gridTransform;
    public GridType gridType => _gridType;

    private string _gridName;
    private Sprite _gridSprite;
    private Transform _gridTransform;
    private GridType _gridType;

    public BaseGrid(string name, Sprite sprite, Transform transform)
    {
        this._gridName = name;
        this._gridSprite = sprite;
        this._gridTransform = transform;
        this._gridType = GridType.Space;
    }

    public void SetGridType(GridType type)
    {
        this._gridType = type;
    }
}


