using System;
using UnityEngine;
using System.Collections;

[System.AttributeUsage(System.AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class InfoAttribute : Attribute
{
    public string textInfo;
    public InfoAttribute(string text)
    {
        this.textInfo = text;
    }
}
