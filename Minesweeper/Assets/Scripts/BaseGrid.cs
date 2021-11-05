using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGrid
{
    public enum GridType
    {
        Mine,
        Number,
        Space,
        Default
    }

    public string GridName => _gridName;
    public Sprite GridSprite { get { return _spriteRenderer.sprite; } set { _spriteRenderer.sprite = (Sprite)value; } }
    public Transform GridTransform => _gridTransform;
    public GridType gridType => _gridType;

    private string _gridName;
    // private Sprite _gridSprite;
    private SpriteRenderer _spriteRenderer;
    private Transform _gridTransform;
    private GridType _gridType;

    public BaseGrid(string name, SpriteRenderer spriteRenderer, Transform transform)
    {
        this._gridName = name;
        // this._gridSprite = spriteRenderer.sprite;
        this._gridTransform = transform;
        this._gridType = GridType.Default;
        this._spriteRenderer = spriteRenderer;
    }

    public void SetGridType(GridType type)
    {
        this._gridType = type;
    }

}


