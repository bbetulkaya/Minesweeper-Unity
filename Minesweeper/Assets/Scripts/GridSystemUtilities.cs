using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Utilities
{
    public class GridSystemUtilities : MonoBehaviour
    {
        public static void CreatePrefab(GameObject gameObject)
        {
            string localPath = "Assets/Prefabs/" + gameObject.name + ".prefab";

            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

            PrefabUtility.SaveAsPrefabAssetAndConnect(gameObject, localPath, InteractionMode.UserAction);
        }

        public static GameObject CreateGameObject(GameObject parent, string name)
        {
            GameObject newGameObject = new GameObject();
            newGameObject.name = name;
            newGameObject.transform.SetParent(parent.transform);

            PrefabUtility.ApplyPrefabInstance(parent, InteractionMode.UserAction);
            return newGameObject;

        }

        public static void SetSpriteRenderer(GameObject gameObject, Sprite sprite)
        {
            var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
        }
    }
}
