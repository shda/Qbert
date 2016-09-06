Shader "Custom/TestSurf"
 {
	Properties 
	{
		_ColorIn ("ColorIn", Color) = (1,1,1,1)
		_ColorOut ("ColorOut", Color) = (1,1,1,1)
        _Lerp ("Lerp", Range(0.0, 1.0)) = 1.0
		_MixColorRadius ("MixColorRadius", Range(0.0, 1.0)) = 1.0
		_MixColorLerp ("MixColorLerp", Range(0.0, 1.0)) = 1.0
		_Distort("Distort", vector) = (0.5, 0.5, 1.0, 1.0)
		
		_Center ("Center", Vector) = (0,0,0,0)
		_Radius ("Radius", Float) = 0.5
	}

	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 250

			CGPROGRAM
			#pragma surface surf Lambert

			sampler2D _MainTex;
            float _Lerp;
			float _MixColorRadius;
			float _MixColorLerp;
			float4 _Distort;
			float4 _ColorIn, _ColorOut;
			
			float3 _Center;
			float _Radius;

			struct Input 
			{
				float2 uv_MainTex;
				float3 localPos;
			};
			
			void surf (Input IN, inout SurfaceOutput o)
			{
				//float d = distance(_Center, IN.localPos);
				/*
				float dN = 1 - saturate(d / _Radius);
				
				if (dN > 0.25 && dN < 0.5)
					o.Albedo = half3(1,1,1);
				else
					o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
				*/
				
				float d = distance(_Center, IN.localPos);
				
				if( IN.uv_MainTex.y < _Lerp )
				{
					o.Albedo =  _ColorOut ;
				}
				else
				{
					o.Albedo = _ColorIn;
				}
				
				//float x = length((_Distort.xy - IN.uv_MainTex) * _Distort.zw);
				
				//fixed4 c = tex2D(_MainTex, IN.uv_MainTex);

				/*
				if( x < _Lerp )
				{
				
					if( x > _Lerp - _MixColorRadius )
					{
						 o.Albedo = lerp(_ColorOut , _ColorIn, x);
					}
				
                    o.Albedo =  _ColorOut ;
				}
				else
				{
					o.Albedo = _ColorIn;
				}
				*/
			}

			/*
			void surf (Input IN, inout SurfaceOutput o)
			{
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
				o.Albedo = lerp(_ColorIn, _ColorOut,  _Lerp);
			}
			*/
			ENDCG  
	}

FallBack "Mobile/Diffuse"
}
