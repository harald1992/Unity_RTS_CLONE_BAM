using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


/*
    Creates a generate button in all unity objects that have a class script with AbstractDungeonGenerator or extends that.
*/


[CustomEditor(typeof(AbstractDungeonGenerator), true)]  // adds this editor to the abstractDungeonCreator class, so you get a button in the unity ui
public class RandomDungeonGeneratorEditor : Editor
{
    AbstractDungeonGenerator generator;

    private void Awake()
    {
        generator = (AbstractDungeonGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Create Dungeon"))
        {
            generator.GenerateDungeon();
        }

        if (GUILayout.Button("Clear Dungeon"))
        {
            generator.ClearMap();
        }
    }
}
