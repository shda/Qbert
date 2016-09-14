using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;


//[CustomPropertyDrawer(typeof(CubeMap))]
public class MapConfiguration : PropertyDrawer
{
    private float hight = 0;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var obj = property.serializedObject.targetObject;

        //var mapAsset = obj as MapAsset;
        //mapAsset.UpdateFromInspector();

        foreach (SerializedProperty a in property)
        {
           // Debug.Log(a.name);
            position.y += 20;
            position.height = 20.0f;
            EditorGUI.PropertyField(position, a , new GUIContent(a.name));
        }
    }


    private void DrawCubes()
    {
        
    }


    
    public override float GetPropertyHeight(UnityEditor.SerializedProperty property, UnityEngine.GUIContent label)
    {
        //return base.GetPropertyHeight(property, label);

        return 230;
    }
    
}
