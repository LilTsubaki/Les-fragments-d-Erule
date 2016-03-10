Shader "Custom/LightPBR_ColorSwap" {
	Properties {
		_MainTex ("Albedo (RGB, Mask1)", 2D) = "white" {}
		_Masks("MixMap (R = Mask2, G = Mask3, B = Mask4)", 2D) = "masks" {}
		_M1Tint ("Fabric tint (Mask1)", color) = (1,1,1,0)
		_M2Tint("Hair tint (Mask2)", color) = (1,1,1,0)
		_M3Tint("Eyes tint (Mask3)", color) = (1,1,1,0)
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

		void surf (Input IN, inout SurfaceOutputStandard o) {
			
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			//fixed4 d = tex2D(_Normal, IN.uv_Normal);			Can't get alpha channel of the  NM to work properly 
			fixed4 d = tex2D(_Masks, IN.uv_Masks);
			half3 m1tint = c.rgb * (c.a * _M1Tint);
			half3 m2tint = c.rgb * (d.r * _M2Tint);
			half3 m3tint = c.rgb * (d.g * _M3Tint);
			o.Albedo = c.rgb * (1 - c.a) * (1 - d.r) * (1 - d.g) + m1tint + m2tint + m3tint;
			o.Normal = UnpackNormal(tex2D(_Normal, IN.uv_Normal));
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
