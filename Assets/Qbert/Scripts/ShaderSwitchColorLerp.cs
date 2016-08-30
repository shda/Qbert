using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class ShaderSwitchColorLerp : MonoBehaviour
{
    public Renderer render;

    public Color start;
    public Color end;
    public float speedLerp;
    public float value;

    private float _valueLerp;
    public float valueLerp
    {
        set
        {
            _valueLerp = value;
            render.material.SetFloat("_Lerp", value);
        }
        get { return _valueLerp; }
    }

    public void SetColorSrart(Color color)
    {
        render.material.SetColor("_ColorIn", color);
        start = color;
    }

    public void SetColorEnd(Color color)
    {
        render.material.SetColor("_ColorOut", color);
        end = color;
    }


    void Awake()
    {
        _valueLerp = render.material.GetFloat("_Lerp");
        SetColorSrart(start);
        SetColorEnd(end);
    }

    public void Update()
    {
        float valueStep = value - valueLerp;
        valueLerp += valueStep * Time.deltaTime * speedLerp;
    }
}
