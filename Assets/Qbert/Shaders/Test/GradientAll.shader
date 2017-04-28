// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/GradientAll" 
{
	Properties
	 {
         [NoScaleOffset] _MainTex ("Sprite Texture", 2D) = "white" {}
         _ColorTop ("Top Color", Color) = (1,1,1,1)
         _ColorMid ("Mid Color", Color) = (1,1,1,1)
         _ColorBot ("Bot Color", Color) = (1,1,1,1)
         _Middle ("Middle", Range(0.001, 0.999)) = 1
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
	 
			 fixed4 _ColorTop;
			 fixed4 _ColorMid;
			 fixed4 _ColorBot;
			 float  _Middle;
	 
			 struct v2f 
			 {
				 float4 pos : SV_POSITION;
				 float4 texcoord : TEXCOORD0;
			 };
	 
			 v2f vert (appdata_full v) 
			 {
				 v2f o;
				 o.pos = UnityObjectToClipPos (v.vertex);
				 o.texcoord = v.texcoord;
				 return o;
			 }
			 
			 sampler2D _MainTex;
	 
			 fixed4 frag (v2f i) : COLOR
			 {
				 fixed4 colorA = lerp(_ColorBot, _ColorMid, i.texcoord.y / _Middle)
					* step(i.texcoord.y, _Middle);
				 
				 colorA += lerp(_ColorMid, _ColorTop, (i.texcoord.y - _Middle) / (1 - _Middle))
					* step(_Middle, i.texcoord.y);
					
				 fixed4 colorB = tex2D(_MainTex, i.texcoord);
				 
				 return half4((colorA.rgb*colorA.a+colorB.rgb*colorB.a),colorA.a+colorB.a);
				 //c.a = 1;
				 //return c;
			 }
			 ENDCG
         }
     }
}
