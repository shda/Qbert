using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Qbert.Scripts.GameScene.AnimationToTime;
using Assets.Qbert.Scripts.LoadScene;
using UnityEditor;

[CustomEditor(typeof(AnimationToTime))]
public class AnimationToTimeEditor : Editor
{
    private AnimationToTime script;
    private SerializedProperty animator;
    private SerializedProperty animationName;

    private string[] animations;

    public int index = 0;

    private void OnEnable()
    {
        

        
    }


    private void UpdatesStrings()
    {
        if (script.animator != null)
        {
            var clips = script.animator.runtimeAnimatorController.animationClips;

            List<string> names = new List<string>();

            foreach (var c in clips)
            {
                if (names.Contains(c.name))
                    continue;
                names.Add(c.name);
            }

            animations = names.ToArray();
        }
    }

    public override void OnInspectorGUI()
    {
        script = target as AnimationToTime;

        UpdatesStrings();

        if (animations != null)
        {
            index = Array.IndexOf(animations, animationName);
        }

        animationName = serializedObject.FindProperty("animationName");

        DrawDefaultInspector();

        if (script != null && animations != null && animations.Length > 0)
        {
            serializedObject.Update();
            index = EditorGUILayout.Popup(index, animations);

            if (index < animations.Length && index >= 0)
            {
                animationName.stringValue = animations[index];
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
