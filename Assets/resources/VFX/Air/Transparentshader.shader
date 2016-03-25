Shader "Custom/Transparentshader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGBA)", 2D) = "white" {}
		_AContrast ("Albedo Contrast", Range (-50,50)) = 0
		_MContrast ("Mask Contrast", Range (-50,50)) = 0
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
		half _AContrast, _MContrast;

		half _AContrast, _MContrast;

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb  * _Color * pow (c.rgb, _AContrast);
			o.Alpha = c.a * pow (c.a, _MContrast);
		}
		ENDCG
	}
	FallBack "Diffuse"
}
