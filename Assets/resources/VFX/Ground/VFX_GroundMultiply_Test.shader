Shader "Custom/ GUI_ReceiveShadows" {
	Properties {
		_MainTex ("NULL Texture", 2D) = "white" {}
	}
	SubShader {
		Tags { "Queue" = "Transparent+99" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		ZWrite Off
		Cull Off
		ColorMask A
		Blend SrcAlpha OneMInusSrcAlpha

		CGPROGRAM

		#pragma surface surf Lambert alpha fullforwardshadows

		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {

			half c = tex2D (_MainTex, IN.uv_MainTex); 
			o.Alpha = 0;

		}
		ENDCG
	}
}
