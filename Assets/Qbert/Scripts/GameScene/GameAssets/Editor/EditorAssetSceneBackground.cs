using System.Collections;
using System.Collections.Generic;
using Assets.Qbert.Scripts.LoadScene;
using UnityEditor;
using UnityEngine;

//[CustomPropertyDrawer(typeof(BackgroundsAsset))]
[CustomPropertyDrawer(typeof(BackgroundsAsset.SceneBackground))]
//[CustomEditor(typeof(BackgroundsAsset.SceneBackground))]
public class EditorAssetSceneBackground : PropertyDrawer
{
    private BackgroundsAsset backgroundsAsset;

    public override void OnGUI(Rect rect, SerializedProperty prop, GUIContent label)
    {
        backgroundsAsset = (BackgroundsAsset) prop.serializedObject.targetObject;

        Rect drawRect = rect;
        drawRect.height = EditorGUIUtility.singleLineHeight;

        SerializedProperty image = prop.FindPropertyRelative("image");
        SerializedProperty animation = prop.FindPropertyRelative("animation");

        string[] variableName = prop.propertyPath.Split('.');
        SerializedProperty pgSelect = prop.serializedObject.FindProperty(variableName[0]);

        

        /*
        BackgroundsAsset.SceneBackground sbNew = 
            (BackgroundsAsset.SceneBackground) pgSelect.serializedObject.targetObject;
        */
        BackgroundsAsset.SceneBackground sb = new BackgroundsAsset.SceneBackground();

        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.BeginVertical();
            {
                string selectImage = SetSelect(image, drawRect, backgroundsAsset.prefImages);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

                string selectAnimation = SetSelect(animation, drawRect, backgroundsAsset.prefAnimations);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

                drawRect.width = drawRect.width - 20;

                float widthSelect = drawRect.width;

                if (GUI.Button(drawRect, "Select"))
                {
                    sb.animation = selectAnimation;
                    sb.image = selectImage;

                    LoadBackgroundAnimation.instance.LoadBackground(sb);
                }

                drawRect.width = 20;
                drawRect.x += widthSelect;

                var saveColor = GUI.backgroundColor;

                GUI.backgroundColor = Color.red;

                if (GUI.Button(drawRect, "X"))
                {
                    for (int i = 0; i < pgSelect.arraySize; i++)
                    {
                        var index = pgSelect.GetArrayElementAtIndex(i);
                        if (prop.displayName == index.displayName)
                        {
                            backgroundsAsset.RemoveOrderBy(i);
                        }
                    }
                }
                GUI.backgroundColor = saveColor;

                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();
    }

    private string SetSelect(SerializedProperty image, Rect drawRect , Transform[] transforms)
    {
        List<GUIContent> names = new List<GUIContent>(100);

        foreach (var animat in transforms)
        {
            names.Add(new GUIContent(animat.name));
        }

        string sV = image.stringValue;

        int indexSelect = names.FindLastIndex(x => x.text == sV);

        int indexNew = EditorGUI.Popup(drawRect, new GUIContent(image.name),
            indexSelect, names.ToArray());

        if (indexNew != indexSelect)
        {
            image.stringValue = names[indexNew].text;
        }

        return image.stringValue;
    }

    public override float GetPropertyHeight(SerializedProperty prop, GUIContent label)
    {
        return 4*EditorGUIUtility.singleLineHeight;
    }
}
