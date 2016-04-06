Shader "Custom/ VFX_GroundMultiply" {
	Properties {
		_MainTex ("Grayscale TilingTex", 2D) = "white" {}
		_Mask ("Grayscale Mask", 2D) = "white" {}
		_MaskContrast("Mask contrast", Range(0.2,3)) = 1 // the contrast is blown out below 1
		_Tint("Emissive tint", color) = (1,1,1) // half3 color value (do not need alpha)
		_Opacity("Opacity", Range(0,1)) = 0 
		_Strength("Emissive strength", Range (1,100)) = 1
		_Contrast("Contrast", Range(1,3)) = 1 // the contrast is blown out below 1

		_TilingTexMoveSpeedV("V Move Speed", Range(-5,5)) = 0.5
		_TilingTexMoveSpeedU("U Move Speed", Range(-5,5)) = 0.5

	}
	SubShader {
		Tags { "RenderType"="Transparent+99" }
		Blend DstColor Zero
		ZWrite Off
		Lighting Off

		CGPROGRAM

		#pragma surface surf Lambert alpha

		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Mask;

		half3 _Tint;
		half _Opacity;
		half _Strength;
		half _Contrast;
		half _MaskContrast;

		float _TilingTexMoveSpeedU;
		float _TilingTexMoveSpeedV;

		struct Input {
			float2 uv_MainTex;
			float2 uv_Mask;
		};

		void surf (Input IN, inout SurfaceOutput o) {

			float2 TilingTexMoveScrolledUV = IN.uv_MainTex; // screen proection ==> replace IN.uv_TilingTex by screenUV
			
			float TilingTexMoveU = _TilingTexMoveSpeedU * _Time;
			float TilingTexMoveV = _TilingTexMoveSpeedV * _Time;
			TilingTexMoveScrolledUV += float2(TilingTexMoveU, TilingTexMoveV);

			half c = tex2D (_MainTex, TilingTexMoveScrolledUV); 
			half e = tex2D (_Mask, IN.uv_Mask);
			half d = pow(c, _Contrast);
			 
			o.Albedo = d * _Tint * _Strength;
			//o.Albedrro += lerp(1, d, _Opacity);
			o.Emission = lerp (0, d, _Opacity) * _Tint * _Strength;

			o.Alpha = pow(e, _MaskContrast) * _Opacity;

		}
		ENDCG
	}
	FallBack "Diffuse"
}
