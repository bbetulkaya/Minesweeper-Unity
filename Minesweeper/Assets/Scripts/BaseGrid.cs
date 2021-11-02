using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGrid
{
    public string GridName => _gridName;
    public Sprite GridSprite => _gridSprite;
    public Transform GridTransform => _gridTransform;
    private string _gridName;
    private Sprite _gridSprite;
    private Transform _gridTransform;

    public BaseGrid(string name, Sprite sprite, Transform transform)
    {
        this._gridName = name;
        this._gridSprite = sprite;
        this._gridTransform = transform;
    }
}
