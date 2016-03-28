Shader "Custom/Standard_DetailTex" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
		
		_Normal ("NormalMap", 2D) = "" {}
		
		_TilingTex("Tiling texture", 2D) = "white" {}
		_Tint("Emissive tint", color) = (1,1,1) // half3 color value (do not need alpha)
		_Opacity("Detail texture opacity", Range(0,1)) = 0 
		_Strength("Emissive strength", Range (1,10)) = 1
		_Contrast("Contrast", Range(1,3)) = 1 // the contrast is blown out below 1
		
	

		_TilingTexMoveSpeedV("V Move Speed", Range(-5,5)) = 0.5
		_TilingTexMoveSpeedU("U Move Speed", Range(-5,5)) = 0.5
	}

	SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Lambert
	
		sampler2D _MainTex;
		sampler2D _Normal;
		sampler2D _TilingTex;
		half3 _Tint;
		half _Opacity;
		half _Strength;
		half _Contrast;
		
		float _TilingTexMoveSpeedU;
		float _TilingTexMoveSpeedV;

		struct Input {
			float2 uv_MainTex;
			float2 uv_TilingTex;
			float2 uv_Normal;
//			float4 screenPos; //screen projection
		};

		void surf(Input IN, inout SurfaceOutput o) {

//			float2 screenUV = IN.screenPos.xy / IN.screenPos.w;
//          	screenUV *= float2(2,1); //screen projection

			float2 TilingTexMoveScrolledUV = IN.uv_TilingTex; // screen proection ==> replace IN.uv_TilingTex by screenUV
			
			float TilingTexMoveU = _TilingTexMoveSpeedU * _Time;
			float TilingTexMoveV = _TilingTexMoveSpeedV * _Time;
			TilingTexMoveScrolledUV += float2(TilingTexMoveU, TilingTexMoveV);

			half3 c = tex2D(_MainTex, IN.uv_MainTex);
			half d = tex2D(_TilingTex, TilingTexMoveScrolledUV);
			half e = pow(d, _Contrast); 

//			o.Albedo = c * (d.r * pow(d.g, _Opacity) * _Tint); 
			o.Albedo = c * pow(e, _Opacity);
//			o.Emission = lerp (1, d.r * _Tint, _Opacity);
			o.Emission = lerp (0, e, _Opacity) * _Tint * _Strength;
			o.Normal = UnpackNormal(tex2D(_Normal, IN.uv_Normal));

		}

		ENDCG
	}

	Fallback "Diffuse"
}