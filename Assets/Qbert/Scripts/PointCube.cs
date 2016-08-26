using UnityEngine;
using System.Collections;

[System.Serializable]
public struct PointCube
{
    public int line;
    public int position;

    public PointCube(int line, int position)
    {
        this.line = line;
        this.position = position;
    }

    public static bool operator ==(PointCube a, PointCube b)
    {
        return a.position == b.position && a.line == b.line;
    }

    public static bool operator !=(PointCube a, PointCube b)
    {
        return !(a == b);
    }

    public new string ToString()
    {
        return line + " - " + position;
    }
}
