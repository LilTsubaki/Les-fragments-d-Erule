Shader "Custom/Transparent Diffuse (Mask cutoff)_additivePortalOrnment" {
	Properties{
		_MainTex("Base (R=Lightning1 G=Lightning2 B=Energyays A=Clouds", 2D) = "white" {}
		_Color("Tint", color) = (1,1,1,1)
		_Contrast("Contrast", Range(0,12)) = 0
		_MainTexMoveSpeedU("R_U Move Speed", Range(-50,50)) = 0
		_SecondTexMoveSpeedU("G_U Move Speed", Range(-50,50)) = 0
		_ThirdTexMoveSpeedU("B_U Move Speed", Range(-50,50)) = 0
		_ThirdTexMoveSpeedV("B_V Move Speed", Range(-50,50)) = 0
		_FourthTexMoveSpeedU("A_U Move Speed", Range(-50,50)) = 0
		_FourthTexMoveSpeedV("A_V Move Speed", Range(-50,50)) = 0
		_FifthTexMoveSpeedU("5_U Move Speed", Range(-50,50)) = 0
	}

	SubShader{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		ZWrite Off
			Cull Off
		Blend One One
		BindChannels{
			Bind "Color", color
		}

		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		half4 _Color;
		half _Contrast;
		fixed _MainTexMoveSpeedU;
		fixed _SecondTexMoveSpeedU;
		fixed _ThirdTexMoveSpeedU;
		fixed _ThirdTexMoveSpeedV;
		fixed _FourthTexMoveSpeedU;
		fixed _FourthTexMoveSpeedV;
		fixed _FifthTexMoveSpeedU;

		struct Input {
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutput o) {

			fixed2 MainTexMoveScrolledUV = IN.uv_MainTex;
			fixed2 SecondTexMoveScrolledUV = IN.uv_MainTex;
			fixed2 ThirdTexMoveScrolledUV = IN.uv_MainTex;
			fixed2 FourthTexMoveScrolledUV = IN.uv_MainTex;
			fixed2 FifthTexMoveScrolledUV = IN.uv_MainTex;

			fixed MainTexMoveU = _MainTexMoveSpeedU * _Time;
			fixed SecondTexMoveU = _SecondTexMoveSpeedU * _Time;
			fixed ThirdTexMoveU = _ThirdTexMoveSpeedU * _Time;
			fixed ThirdTexMoveV = _ThirdTexMoveSpeedV * _Time;
			fixed FourthTexMoveU = _FourthTexMoveSpeedU * _Time;
			fixed FourthTexMoveV = _FourthTexMoveSpeedV * _Time;
			fixed FifthTexMoveU = _FifthTexMoveSpeedU * _Time;

			MainTexMoveScrolledUV += fixed2(MainTexMoveU, 0);
			SecondTexMoveScrolledUV += fixed2(SecondTexMoveU, 0);
			ThirdTexMoveScrolledUV += fixed2(ThirdTexMoveU, ThirdTexMoveV);
			FourthTexMoveScrolledUV += fixed2(FourthTexMoveU, FourthTexMoveV);
			FifthTexMoveScrolledUV += fixed2(FifthTexMoveU, 0);

			half c = tex2D(_MainTex, MainTexMoveScrolledUV).r;
			half d = tex2D(_MainTex, SecondTexMoveScrolledUV).g;
			half e = tex2D(_MainTex, ThirdTexMoveScrolledUV).b;
			half f = tex2D(_MainTex, FourthTexMoveScrolledUV).a;
			half g = tex2D(_MainTex, FifthTexMoveScrolledUV).b;

			o.Albedo = (c * 8 + d * 8 + (e * 12) + g *10) * _Color * pow (f, _Contrast);

			//o.Emission = o.Albedo * 15 * (_Color * half4(.9,.2,.4,1));
		}

		ENDCG
	}
}