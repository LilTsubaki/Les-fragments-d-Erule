Shader "Custom/Transparent Diffuse (Mask cutoff)" {
	Properties{
		_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
		/*_Cutoff("Mask cutoff", Range(0,1)) = 0*/
		_MaskTex("Mask", 2D) = "white" {}
		_MainTexMoveSpeedU("U Move Speed", Range(0,100)) = 0.5
		_MainTexMoveSpeedV("V Move Speed", Range(0,100)) = 0.5
	}

	SubShader{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Lambert alpha

		sampler2D _MainTex;
		sampler2D _MaskTex;
		/*fixed _Cutoff;*/
		fixed _MainTexMoveSpeedU;
		fixed _MainTexMoveSpeedV;

		struct Input {
			float2 uv_MainTex;
			float2 uv_MaskTex;
		};

		void surf(Input IN, inout SurfaceOutput o) {

			// Moving

			fixed2 MainTexMoveScrolledUV = IN.uv_MainTex;

			fixed MainTexMoveU = _MainTexMoveSpeedU * _Time;
			fixed MainTexMoveV = _MainTexMoveSpeedV * _Time;

			MainTexMoveScrolledUV += fixed2(MainTexMoveU, MainTexMoveV);

			// Alpha cutoff

			fixed mask = tex2D(_MaskTex, IN.uv_MaskTex);
			//clip(mask.a - _Cutoff*1.004); // 1.004 = 1+(1/255) to make sure also white is clipped

			fixed4 c = tex2D(_MainTex, MainTexMoveScrolledUV);
			o.Albedo = c.rgb*2 ;
			o.Alpha = mask.r * (1 - c.a) ;
		}

		ENDCG
	}

	Fallback "Transparent/Diffuse"
}