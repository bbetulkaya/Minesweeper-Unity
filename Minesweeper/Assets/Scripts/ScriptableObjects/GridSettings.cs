using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "ScriptableObjects/GridSettings", order = 1)]
public class GridSettings : ScriptableObject
{
    public int gridSize;
    public int numberOfMines;
    public Sprite defaultSprite;


    [Header("Sprite Distance")]
    [Range(0f, 5f)]
    public float distanceX;
    [Range(0f, 5f)]
    public float distanceY;
}