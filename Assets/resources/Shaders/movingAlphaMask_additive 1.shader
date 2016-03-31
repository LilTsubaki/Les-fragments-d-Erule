Shader "Custom/Transparent Diffuse (Mask cutoff)_AdditivePortal" {
	Properties{
		_MainTex("Base (RG)", 2D) = "white" {}
		_Color("Tint", color) = (1,1,1,1)
		_MainTexMoveSpeedU("R_U Move Speed", Range(-50,50)) = 0.5
		//_MainTexMoveSpeedV("V Move Speed", Range(-50,50)) = 0.5
		_SecondTexMoveSpeedU("G_U Move Speed", Range(-50,50)) = 0.5
	}

	SubShader{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		ZWrite Off
		Blend One One

		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		/*half _Cutoff;
		half _Blur;*/
		half4 _Color;
		fixed _MainTexMoveSpeedU;
		//fixed _MainTexMoveSpeedV;
		fixed _SecondTexMoveSpeedU;

		struct Input {
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutput o) {

			// Moving

			fixed2 MainTexMoveScrolledUV = IN.uv_MainTex;
			fixed2 SecondTexMoveScrolledUV = IN.uv_MainTex;

			fixed MainTexMoveU = _MainTexMoveSpeedU * _Time;
			//fixed MainTexMoveV = _MainTexMoveSpeedV * _Time;

			fixed SecondTexMoveU = _SecondTexMoveSpeedU * _Time;

			MainTexMoveScrolledUV += fixed2(MainTexMoveU, IN.uv_MainTex.y);
			SecondTexMoveScrolledUV += fixed2(SecondTexMoveU, IN.uv_MainTex.y);

			half3 e = tex2D(_MainTex, MainTexMoveScrolledUV);

			half c = e.r;
			half d = e.g;

			o.Albedo = (tex2D(_MainTex, MainTexMoveScrolledUV).r + tex2D(_MainTex, SecondTexMoveScrolledUV).g) * _Color;
		}

	ENDCG
	}

Fallback "Transparent/Diffuse"
}