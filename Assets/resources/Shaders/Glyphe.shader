Shader "Custom/Glyphe"
{
	Properties
	{
		_MainTex("Texture (RGBA)", 2D) = "white" {}
		_Color("Tint", color) = (1,1,1,1)
	}

	CGINCLUDE	
	#include "UnityCG.cginc"

	struct appdata{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
	};

	struct v2f{
		float2 uv : TEXCOORD0;
		float4 vertex : SV_POSITION;
	};

	sampler2D _MainTex;

	half4 _Color;
	float4 _MainTex_ST;
	
	v2f vert (appdata v){
		v2f o;
		o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
		o.uv = TRANSFORM_TEX(v.uv, _MainTex);
		return o;
	}
	ENDCG

	SubShader{
		Tags { "RenderType"="Transparent+99" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		Lighting Off

		Pass{
			Name "BASE"
			ZTest Less
			Blend SrcAlpha OneMinusSrcAlpha

			SetTexture[_MainTex]{ Combine texture, primary }
		}

		Pass{
			Name "OVERLAY"
			ZTest Greater
			Blend One One

			SetTexture[_MainTex]{ Combine texture * previous }
		}

	}
}
