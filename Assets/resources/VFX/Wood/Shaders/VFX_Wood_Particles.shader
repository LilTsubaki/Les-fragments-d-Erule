Shader "Custom/VFX_Wood_Particles"
{
	Properties
	{
		_VFX_Particles("Texture", 2D) = "white" {}
		/*_Cutoff("Alpha cutoff", Range(0,1)) = 0.5*/
	}
	SubShader
	{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" "IgnoreProjector" = "True" }
		//ZWrite Off

		Pass
		{
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
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			/*float _Cutoff;*/
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 c = tex2D(_MainTex, i.uv);
				/*clip(c.a - _Cutoff);*/
				return c;
			}
			ENDCG
		}
	}
}
