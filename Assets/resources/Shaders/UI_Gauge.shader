Shader "Custom/UI_Gauge" {
	Properties{
		_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
		_Strength("Emissive strength", Range(0,1)) = 0
		_MaskTex("Dissolve mask", 2D) = "white" {}
		_Cutoff("Mask cutoff", Range(1,0)) = 0
	}

		SubShader{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		ZWrite On
		Lighting Off
		Blend SrcAlpha OneMinusSrcAlpha

		CGPROGRAM
		#pragma surface surf Lambert alpha

		sampler2D _MainTex;
		sampler2D _MaskTex;

		half _Cutoff;
		half _Strength;

		struct Input {
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutput o) {

			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			half mask = tex2D(_MaskTex, IN.uv_MainTex);

			o.Albedo = c.rgb;
			o.Emission = _Strength * c.r;
			o.Alpha = c.a;
			clip((1-mask) - _Cutoff);

		}

		ENDCG
		}
}