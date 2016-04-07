Shader "Custom/Standard_controls" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGBA)", 2D) = "white" {}
		_AContrast("Albedo Contrast", Range(-50,50)) = 0
		_Strength("Mask Falloff", Range(0, 2)) = 1
		_Offset("Mask offset", Range(0,5)) = 0
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		Zwrite On
		Cull Back
		Lighting Off

		CGPROGRAM

		#pragma surface surf Lambert 
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half4 _Color;
		half _AContrast, _Strength, _Offset;

		void surf(Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex.xy * _Strength + _Offset);
			o.Albedo = c.rgb *  _Color * pow(c.rgb, _AContrast);
		}
		ENDCG
	}
	FallBack "Diffuse"
}
