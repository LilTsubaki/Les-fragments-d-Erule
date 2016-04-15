Shader "Custom/Title_E" {
	Properties{
		_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
		_Tint("Tint", color) = (1,1,1,1)
		_EmissiveTex("Emmissive", 2D) = "white" {}
		_EmissionTint("Emissive tint", color) = (1,1,1,1)
		_Strength("Emissive strength", Range(0,1)) = 0
		_Cutoff("Mask cutoff", Range(50,0)) = 0
	}

	SubShader{
		Tags{ "Queue" = "Transparent+99" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		ZWrite Off
		Lighting Off
//		Blend SrcAlpha OneMinusSrcAlpha

		CGPROGRAM
		#pragma surface surf Lambert alpha
		 
		sampler2D _MainTex;
		sampler2D _EmissiveTex;

		half4 _Tint;
		half4 _EmissionTint;
		half _Cutoff;
		half _Strength;

		struct Input {
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutput o) {

			half4 c = tex2D(_MainTex, IN.uv_MainTex);
			half d = tex2D(_EmissiveTex, IN.uv_MainTex);

			o.Albedo = c.rgb * _Tint;
			o.Emission = d * _Strength * _EmissionTint;
			o.Alpha = pow(c.a, _Cutoff);

		}

		ENDCG
	}
}