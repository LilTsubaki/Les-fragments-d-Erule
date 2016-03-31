Shader "Custom/LightPBR_ColorSwap" {
	Properties {
		_MainTex ("Albedo (RGB, Mask1)", 2D) = "white" {}
		_Masks("Masks (R = Mask1, G = Mask2, B = Mask3, A = [BODY ONLY] Mask4)", 2D) = "masks" {}

		_M1Tint ("Skin/ShoulderPad (Mask1)", color) = (1,1,1,0)
		_M2Tint("Hair/Runes tint (Mask2)", color) = (1,1,1,0)
		_M3Tint("Eyes/RingBelt tint (Mask3)", color) = (1,1,1,0)
		_M4Tint("[BODY ONLY] Fabric tint (Mask4)", color) = (1,1,1,0)

		_Normal ("Normal map ", 2D) = "normal" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
//		_Metallic ("Metallic", Range(0,1)) = 0.0

		_TilingTex("Detail texture", 2D) = "white" {}
		_Tint("Emissive tint", color) = (1,1,1) // half3 color value (do not need alpha)
		_Opacity("Detail tex. opacity", Range(0,1)) = 0 
		_Strength("Emissive strength", Range (1,100)) = 1
		_Contrast("Contrast", Range(1,3)) = 1 // the contrast is blown out below 1

		_TilingTexMoveSpeedV("V Move Speed", Range(-5,5)) = 0.5
		_TilingTexMoveSpeedU("U Move Speed", Range(-5,5)) = 0.5

	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM

		#pragma surface surf Standard fullforwardshadows


		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Masks;
		sampler2D _Normal;
		sampler2D _TilingTex;

		half4 _M1Tint;
		half4 _M2Tint;
		half4 _M3Tint;
		half4 _M4Tint;

		half3 _Tint;
		half _Opacity;
		half _Strength;
		half _Contrast;

		float _TilingTexMoveSpeedU;
		float _TilingTexMoveSpeedV;

		half _Glossiness;
//		half _Metallic;

		struct Input {
			float2 uv_MainTex;
			float2 uv_Masks;
			float2 uv_Normal;
			float2 uv_TilingTex;
		};

		void surf (Input IN, inout SurfaceOutputStandard o) {

//  		float2 screenUV = IN.screenPos.xy / IN.screenPos.w;
//          screenUV *= float2(2,1); //screen projection

			float2 TilingTexMoveScrolledUV = IN.uv_TilingTex; // screen proection ==> replace IN.uv_TilingTex by screenUV
			
			float TilingTexMoveU = _TilingTexMoveSpeedU * _Time;
			float TilingTexMoveV = _TilingTexMoveSpeedV * _Time;
			TilingTexMoveScrolledUV += float2(TilingTexMoveU, TilingTexMoveV);

			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			//fixed4 d = tex2D(_Normal, IN.uv_Normal);		=>	Can't get the alpha channel of the  NM to work properly 
			fixed4 d = tex2D(_Masks, IN.uv_Masks);
			half e = tex2D(_TilingTex, TilingTexMoveScrolledUV);
			half f = pow(e, _Contrast); 

			half3 m1tint = c.rgb * (d.r * _M1Tint);
			half3 m2tint = c.rgb * (d.g * _M2Tint);
			half3 m3tint = c.rgb * (d.b * _M3Tint);
			half3 m4tint = c.rgb * (d.a * _M4Tint);

			o.Albedo = c.rgb * (1 - d.r) * (1 - d.g) * (1 - d.b) * (1 - d.a) + m1tint + m2tint + m3tint + m4tint;
			o.Albedo *= lerp(1, f, _Opacity);
//			o.Albedo *= pow(f, _Opacity);
//			o.Albedo *= d.r * pow(d.g, _Opacity) * _Tint; 

			o.Emission = lerp (0, f, _Opacity) * _Tint * _Strength;
//			o.Emission = lerp (1, d.r * _Tint, _Opacity);

			o.Normal = UnpackNormal(tex2D(_Normal, IN.uv_Normal));

			//o.Metallic = _Metallic;

			o.Smoothness = _Glossiness;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
