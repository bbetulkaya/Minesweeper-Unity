using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGrid
{
    public string GridName => _gridName;
    public Sprite GridSprite => _gridSprite;
    private string _gridName;
    private Sprite _gridSprite;

    public BaseGrid(string name, Sprite sprite)
    {
        this._gridName = name;
        this._gridSprite = sprite;
    }
}
