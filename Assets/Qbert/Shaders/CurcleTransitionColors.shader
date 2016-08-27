Shader "Custom/CurcleTransitionColors" 
{
	 Properties
    {
		_ColorIn ("ColorIn", Color) = (1,1,1,1)
		_ColorOut ("ColorOut", Color) = (1,1,1,1)
        _Lerp ("Lerp", Range(0.0, 1.0)) = 1.0
		_MixColorRadius ("MixColorRadius", Range(0.0, 1.0)) = 1.0
		_MixColorLerp ("MixColorLerp", Range(0.0, 1.0)) = 1.0
		_Distort("Distort", vector) = (0.5, 0.5, 1.0, 1.0)
     }

    SubShader
    {
        LOD 200

        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
        }

        Pass
        {
            Cull Off 
            Lighting Off
            ZWrite Off
            Offset -1, -1
            Fog { Mode Off }
            ColorMask RGB
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _Lerp;
			float _MixColorRadius;
			float _MixColorLerp;
			float4 _Distort;
			float4 _ColorIn, _ColorOut;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0; 
            };

            struct v2f
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.texcoord = v.texcoord;
                return o;
            }

            half4 frag (v2f IN) : COLOR 
            {
				float x = length((_Distort.xy - IN.texcoord.xy) * _Distort.zw);
				
				if( x < _Lerp )
				{

					if( x > _Lerp - _MixColorRadius )
					{
						 return lerp(_ColorOut , _ColorIn, x);
					}
                    return  _ColorOut ;
				}
				
				
                return _ColorIn;
            }
            ENDCG
        }
    }
}
