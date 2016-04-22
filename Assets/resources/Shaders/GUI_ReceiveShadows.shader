Shader "Custom/ GUI_ReceiveShadows" {
	Properties {
		_Color ("NULL Color", color) = (1,1,1,1)
	}
	SubShader{
		Tags { "Queue" = "Transparent+99" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		ZWrite Off
		ZTest Greater
		Lighting On
		//Blend SrcAlpha OneMinusSrcAlpha
		Blend One OneMinusSrcAlpha

		CGPROGRAM

		#pragma surface surf Lambert alpha fullforwardshadows

		#pragma target 3.0

		half4 _Color;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {

			o.Albedo = _Color; 
			o.Alpha = 1;

		}
		ENDCG
	}
}
