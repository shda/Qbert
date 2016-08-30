using UnityEngine;
using System.Collections;
using UnityEditor;

/*
[CustomPropertyDrawer(typeof(Round.GemeplayObjectConfig))]
public class ConfigurationInspector : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty propertys, GUIContent label)
    {
        Rect contentPosition = EditorGUI.PrefixLabel(position, GUIContent.none);
        EditorGUI.LabelField(contentPosition, propertys.displayName);

        float labelWidth = 140.0f;
       // contentPosition.x += labelWidth;

        foreach (var property in propertys)
        {
            SerializedProperty actionType = property as SerializedProperty;
            EditorGUI.LabelField(contentPosition, propertys.displayName);
            //contentPosition.x += 30.0f;
            
           // contentPosition.width = 60.0f;

            EditorGUI.PropertyField(contentPosition, actionType, GUIContent.none);
            //contentPosition.y += 50.0f;
           // contentPosition.x += 50.0f;
        }
        
    }
}
*/