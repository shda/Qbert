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

        string selectImage = SetSelect(image, drawRect , backgroundsAsset.prefImages);
        drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        string selectAnimation = SetSelect(animation, drawRect, backgroundsAsset.prefAnimations);
        drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        if (GUI.Button(drawRect ,  "Select"))
        {
            BackgroundsAsset.SceneBackground sb = new BackgroundsAsset.SceneBackground();
            sb.animation = selectAnimation;
            sb.image = selectImage;

            LoadBackgroundAnimation.instance.LoadBackground(sb);
        }
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
