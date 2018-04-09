Shader "Unlit/NewShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	    _SecondTex ("Texture2", 2D) = "white"{}
		_LerpValue ("Transition float", Range(0,100)) = 50
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{   // that needs more vertexs ( if the pc isnt crap)
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};
			//main texture
			sampler2D _MainTex;
			float4 _MainTex_ST;
			//crappy texture 
			sampler2D _SecondTex;
			// / by 100 for better results (already done below, but is a magic number)
			float _LerpValue;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = lerp(tex2D(_MainTex, i.uv), tex2D(_SecondTex, i.uv),_LerpValue/100);
				
			    

				return col;
			}
			ENDCG
		}
	}
}
