Shader "Custom/Transparentshader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGBA)", 2D) = "white" {}
	}
	SubShader {
		Tags { "Queue"="Transparent+99" "IgnoreProjector"="True"  "RenderType"="Transparent" }
		Zwrite On
		Cull Back
		Lighting Off

		CGPROGRAM

		#pragma surface surf Lambert alpha
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb  * _Color;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
