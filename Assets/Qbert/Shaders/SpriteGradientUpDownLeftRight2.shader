// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/SpriteGradientUpDownLeftRight2" 
{
	 Properties 
	 {
		 [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		 
		 _ColorUp ("Up Color", Color) = (1,1,1,1)
		 _ColorDown ("Down Color", Color) = (1,1,1,1)
		 _ColorLeft ("Left Color", Color) = (1,1,1,1)
		 _ColorRight ("Right Color", Color) = (1,1,1,1)
		
		 
		 _OffsetUpDown ("Offset", Range(-1.0, 1.0)) = 0.0
		 _OffsetLeftRight ("Offset", Range(-1.0, 1.0)) = 0.0

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
			fixed4 _ColorLeft;
			fixed4 _ColorRight;
			
			fixed  _OffsetUpDown;
			fixed  _OffsetLeftRight;
  
			 struct v2f 
			 {
				 float4 pos : SV_POSITION;
				 fixed4 col : COLOR;
			 };
  
			 v2f vert (appdata_full v)
			 {
				 v2f o;
				 o.pos = UnityObjectToClipPos (v.vertex);
				 
				 fixed4 col1 = lerp(_ColorDown ,_ColorUp, v.texcoord.y + _OffsetUpDown );
				 col1 = lerp(col1 ,_ColorLeft, v.texcoord.x + _OffsetLeftRight );
				 col1 = lerp(col1 ,_ColorRight, v.texcoord.x + _OffsetLeftRight );
				 
				 o.col = col1;
				 //o.col = lerp(_ColorDown ,_ColorUp, v.texcoord.y + _OffsetUpDown );
				// o.col = lerp(_ColorRight ,_ColorLeft, v.texcoord.x + _OffsetLeftRight );
				 
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
