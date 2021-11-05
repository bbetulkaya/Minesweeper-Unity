using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "ScriptableObjects/SpriteCollection", order = 1)]
public class SpriteCollection : ScriptableObject
{
    public Sprite defaultSprite;
    public Sprite mine;
    public Sprite space;
    public Sprite[] number;
}