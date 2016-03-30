Shader "Custom/Transparent Diffuse (Mask cutoff)_main water" {
	Properties{
		_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
		_Cutoff("Mask cutoff", Range(1,0)) = 0
		_MaskTex("Mask", 2D) = "white" {}
	}

	SubShader{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		CGPROGRAM
		#pragma surface surf Lambert alpha

		sampler2D _MainTex;
		sampler2D _MaskTex;
		half _Cutoff;

		struct Input {
			float2 uv_MainTex;
			float2 uv_MaskTex;
		};

		void surf(Input IN, inout SurfaceOutput o) {

			// Alpha cutoff

			half mask = tex2D(_MaskTex, IN.uv_MaskTex);
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);

			clip(mask - _Cutoff * 1.004);

			o.Albedo = c.rgb*3;
			o.Alpha = c.a;
		}

		ENDCG
	}

	Fallback "Transparent/Diffuse"
}