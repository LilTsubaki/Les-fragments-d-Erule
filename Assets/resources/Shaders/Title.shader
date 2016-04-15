Shader "Custom/Title" {
	Properties{
		_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
		_Strength("Emissive strength", Range(0,1)) = 0
		_MaskTex("Dissolve mask", 2D) = "white" {}
		_Cutoff("Mask cutoff", Range(0,150)) = 0
	}

	SubShader{
		Tags{ "Queue" = "Transparent+99" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		Cull Off
		ZWrite Off
		ZTest Always
		Lighting Off
//		Blend SrcAlpha OneMinusSrcAlpha

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
			o.Alpha = c.a * pow(mask, _Cutoff);

		}

		ENDCG
	}
}