Shader "Custom/LightPBR_ColorSwap" {
	Properties {
		_MainTex ("Albedo (RGB, Mask1)", 2D) = "white" {}
		_Masks("BodyMasks (R = Mask1, G = Mask2, B = Mask3, A = [BODY ONLY] Mask4)", 2D) = "masks" {}

		_M1Tint ("Skin/ShoulderPad (Mask1)", color) = (1,1,1,0)
		_M2Tint("Hair/Runes tint (Mask2)", color) = (1,1,1,0)
		_M3Tint("Eyes/RingBelt tint (Mask3)", color) = (1,1,1,0)
		_M4Tint("[BODY ONLY] Fabric tint (Mask4)", color) = (1,1,1,0)

		_Normal ("Normal map ", 2D) = "normal" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0

	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM

		#pragma surface surf Standard fullforwardshadows


		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Normal;
		sampler2D _Masks;

		struct Input {
			float2 uv_MainTex;
			float2 uv_Normal;
			float2 uv_Masks;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _M1Tint;
		fixed4 _M2Tint;
		fixed4 _M3Tint;
		fixed4 _M4Tint;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			//fixed4 d = tex2D(_Normal, IN.uv_Normal);		=>	Can't get the alpha channel of the  NM to work properly 
			fixed4 d = tex2D(_Masks, IN.uv_Masks);

			half3 m1tint = c.rgb * (d.r * _M1Tint);
			half3 m2tint = c.rgb * (d.g * _M2Tint);
			half3 m3tint = c.rgb * (d.b * _M3Tint);
			half3 m4tint = c.rgb * (d.a * _M4Tint);
			o.Albedo = c.rgb * (1 - d.r) * (1 - d.g) * (1 - d.b) * (1 - d.a) + m1tint + m2tint + m3tint + m4tint;

			o.Normal = UnpackNormal(tex2D(_Normal, IN.uv_Normal));

			o.Metallic = _Metallic;

			o.Smoothness = _Glossiness;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
