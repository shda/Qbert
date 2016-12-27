Shader "Unlit/GradientAndTexture"
{
	Properties
    {
        [NoScaleOffset] _MainTex ("Texture", 2D) = "white" {}
		
		_ColorIn ("ColorIn", Color) = (1,1,1,1)
		_ColorOut ("ColorOut", Color) = (1,1,1,1)
        _Lerp ("Lerp", Range(0.0, 1.0)) = 1.0
		_MixColorRadius ("MixColorRadius", Range(0.0, 1.0)) = 1.0
		_MixColorLerp ("MixColorLerp", Range(0.0, 1.0)) = 1.0
		_Distort("Distort", vector) = (0.5, 0.5, 1.0, 1.0)
    }
    SubShader
    {
        Pass
        {
            Tags {"LightMode"="ForwardBase"}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            // compile shader into multiple variants, with and without shadows
            // (we don't care about any lightmaps yet, so skip these variants)
            #pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight
            // shadow helper functions and macros
            #include "AutoLight.cginc"

            struct v2f
            {
                float2 uv : TEXCOORD0;
                SHADOW_COORDS(1) // put shadows data into TEXCOORD1
                fixed3 diff : COLOR0;
                fixed3 ambient : COLOR1;
                float4 pos : SV_POSITION;
            };
			
			float _Lerp;
			float _MixColorRadius;
			float _MixColorLerp;
			float4 _Distort;
			float4 _ColorIn, _ColorOut;
			
            v2f vert (appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                half3 worldNormal = UnityObjectToWorldNormal(v.normal);
                half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
               // o.diff = nl * _LightColor0.rgb;
               // o.ambient = ShadeSH9(half4(worldNormal,1));
              //  TRANSFER_SHADOW(o)
                return o;
            }

            sampler2D _MainTex;
			
			
			half4 getColor (v2f IN) : COLOR 
            {
				float x = length((_Distort.xy - IN.uv.xy) * _Distort.zw);
				
				if( x < _Lerp )
				{

					return lerp(_ColorIn, _ColorOut , x);
				}
				
				
                return _ColorIn;
            }


            fixed4 frag (v2f i) : SV_Target
            {

                fixed4 colorA =  getColor(i); //tex2D(_MainTex, i.uv);
				
				fixed4 colorB = tex2D(_MainTex, i.uv);
				
				/*
				if(colorA.a > 0)
				{
					return colorA;
				}
				
                return colorB;
				*/
				
				return half4((colorA.rgb*colorA.a+colorB.rgb*colorB.a),colorA.a+colorB.a);
            }
            ENDCG
        }

        // shadow casting support
        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
    }
}
