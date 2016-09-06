Shader "Custom/LerpColorSurface"
 {
	Properties 
	{
		//_MainTex ("Base (RGB)", 2D) = "white" {}

		_ColorIn ("ColorIn", Color) = (1,1,1,1)
		_ColorOut ("ColorOut", Color) = (1,1,1,1)
        _Lerp ("Lerp", Range(0.0, 1.0)) = 1.0
	}

	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 250

			CGPROGRAM
			#pragma surface surf Lambert noforwardadd

			sampler2D _MainTex;
			sampler2D _BumpMap;
			
			float _Lerp;
			float4 _ColorIn, _ColorOut;

			struct Input 
			{
				float2 uv_MainTex;
			};

			void surf (Input IN, inout SurfaceOutput o)
			{
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
				o.Albedo = lerp(_ColorIn, _ColorOut,  _Lerp);
			}
			ENDCG  
	}

FallBack "Mobile/Diffuse"
}
