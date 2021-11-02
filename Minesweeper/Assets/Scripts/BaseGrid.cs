using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGrid
{
    public string GridName => _gridName;
    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    private string _gridName;
    private SpriteRenderer _spriteRenderer;

    public BaseGrid(string name, SpriteRenderer spriteRenderer)
    {
        this._gridName = name;
        this._spriteRenderer = spriteRenderer;
    }
}
