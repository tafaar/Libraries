using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Listens for OnValidate callback from Unity Editor to update an array from a singleton according to the folder contents

public class OnValidate : MonoBehaviour
{
    [SerializeField]
    RoomContents[] roomContents;

#if UNITY_EDITOR
    public string contentsFolder = "Rooms/Contents";

    void OnValidate()
    {
        string fullPath = $"{Application.dataPath}/{contentsFolder}";
        if (!System.IO.Directory.Exists(fullPath))
        {
            return;
        }

        var folders = new string[] { $"Assets/{contentsFolder}" };
        var guids = AssetDatabase.FindAssets("t:GameObject", folders);

        var newContents = new GameObject[guids.Length];

        bool mismatch;
        if (newContents == null)
        {
            mismatch = true;
            roomContents = newContents;
        }
        else
        {
            mismatch = newContents.Length != roomContents.Length;
        }

        for (int i = 0; i < newContents.Length; i++)
        {
            var path = AssetDatabase.GUIDToAssetPath(guids[i]);
            newContents[i] = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            mismatch |= (i < roomContents.Length && newContents[i] != newContents[i]);
        }

        if (mismatch)
        {
            roomContents = newContents;
            Debug.Log($"{name} sprite list updated.");
        }
    }
#endif
}
