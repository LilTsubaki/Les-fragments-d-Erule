Shader "Custom/Transparent Diffuse (Mask cutoff)_waterSplash" {
	Properties{
		_MainTex("Base (RGB), Mask (A)", 2D) = "white" {}

		_MainTexMoveSpeedU("U Move Speed", Range(-50,50)) = 0.5
		_MainTexMoveSpeedV("V Move Speed", Range(-50,50)) = 0.5
	}

	SubShader{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		Cull Off

		CGPROGRAM
		#pragma surface surf Lambert alpha
		 
		sampler2D _MainTex;

		fixed _MainTexMoveSpeedU;
		fixed _MainTexMoveSpeedV;

		struct Input {
			float2 uv_MainTex;
			float2 uv_MaskTex;
		};

		void surf(Input IN, inout SurfaceOutput o) {

			fixed2 MainTexMoveScrolledUV = IN.uv_MainTex;

			fixed MainTexMoveU = _MainTexMoveSpeedU * _Time;
			fixed MainTexMoveV = _MainTexMoveSpeedV * _Time;

			MainTexMoveScrolledUV += fixed2(MainTexMoveU, MainTexMoveV);

			//half4 mask = tex2D(_MaskTex, fixed2(IN.uv_MaskTex.x, MainTexMoveV));
			half4 tex = tex2D(_MainTex, MainTexMoveScrolledUV);
			half mask = tex2D(_MainTex, fixed2(MainTexMoveU, IN.uv_MainTex.y)).a;

			o.Albedo = tex.rgb;
			o.Alpha = mask;
		}

		ENDCG
	}
	Fallback "Diffuse"
}