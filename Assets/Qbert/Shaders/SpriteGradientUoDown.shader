// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/SpriteGradientUoDown" 
{
	 Properties 
	 {
		 [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		 
		 _ColorUp ("Up Color", Color) = (1,1,1,1)
		 _ColorDown ("Down Color", Color) = (1,1,1,1)
		 
		 _OffsetUpDown ("Offset", Range(-1.0, 1.0)) = 0.0

	 }
  
	SubShader 
	{
		Tags {"Queue"="Background"  "IgnoreProjector"="True"}
		LOD 100
  
		ZWrite On
  
		Pass 
		{
			CGPROGRAM
			#pragma vertex vert  
			#pragma fragment frag
			#include "UnityCG.cginc"
  
			fixed4 _ColorUp;
			fixed4 _ColorDown;
			
			fixed  _OffsetUpDown;
  
			 struct v2f 
			 {
				 float4 pos : SV_POSITION;
				 fixed4 col : COLOR;
			 };
  
			 v2f vert (appdata_full v)
			 {
				 v2f o;
				 o.pos = UnityObjectToClipPos (v.vertex);

				 o.col = lerp(_ColorDown ,_ColorUp, v.texcoord.y + _OffsetUpDown );

				 return o;
			 }
        
  
			 float4 frag (v2f i) : COLOR
			 {
				 float4 c = i.col;
				 c.a = 1;
				 return c;
			 }
             ENDCG
         }
     }
}
