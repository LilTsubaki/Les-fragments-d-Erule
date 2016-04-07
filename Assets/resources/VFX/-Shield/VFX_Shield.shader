Shader "VFX/Shield" {
	Properties{
		_MainTex("Base (R=Center G=Bubbles B=Glitches A=CenterAlpha", 2D) = "white" {}
		_Color("Tint", color) = (1,1,1,1)
		//_MainTexMoveSpeedU("L1 U PanSpeed", Range(-50,50)) = 0
		_SecondTexMoveSpeedU("Bubbles U PanSpeed", Range(-50,50)) = 0
		_SecondTexMoveSpeedV("Bubbles V PanSpeed", Range(-50,50)) = 0
		_ThirdTexMoveSpeedU("Glitches U PanSpeed", Range(-50,50)) = 0
		_ThirdTexMoveSpeedV("Glitches V PanSpeed", Range(-50,50)) = 0
		_Opacity("Opacity", Range(0, 1)) = 0
//		_FourthTexMoveSpeedU("Alpha U PanSpeed", Range(-50,50)) = 0
//		_FourthTexMoveSpeedV("Alpha V PanSpeed", Range(-50,50)) = 0
	}

	SubShader{
		Tags{ "Queue" = "Transparent+99" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		ZWrite Off
		Cull Off
		Lighting Off
		Blend One One
		//Blend SrcAlpha OneMinusSrcAlpha
//		BindChannels{
//			Bind "Color", color
//		}

		CGPROGRAM
		#pragma surface surf Lambert //alpha

		sampler2D _MainTex;
		half4 _Color;
		half _Opacity;

		fixed _MainTexMoveSpeedU;
		fixed _SecondTexMoveSpeedU;
		fixed _SecondTexMoveSpeedV;
		fixed _ThirdTexMoveSpeedU;
		fixed _ThirdTexMoveSpeedV;
//		fixed _FourthTexMoveSpeedU;
//		fixed _FourthTexMoveSpeedV;

		struct Input {
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutput o) {

			fixed2 MainTexMoveScrolledUV = IN.uv_MainTex;
			fixed2 SecondTexMoveScrolledUV = IN.uv_MainTex;
			fixed2 ThirdTexMoveScrolledUV = IN.uv_MainTex;
//			fixed2 FourthTexMoveScrolledUV = IN.uv_MainTex;

			fixed MainTexMoveU = _MainTexMoveSpeedU * _Time;
			fixed SecondTexMoveU = _SecondTexMoveSpeedU * _Time;
			fixed SecondTexMoveV = _SecondTexMoveSpeedV * _Time;
			fixed ThirdTexMoveU = _ThirdTexMoveSpeedU * _Time;
			fixed ThirdTexMoveV = _ThirdTexMoveSpeedV * _Time;
//			fixed FourthTexMoveU = _FourthTexMoveSpeedU * _Time;
//			fixed FourthTexMoveV = _FourthTexMoveSpeedV * _Time;

			MainTexMoveScrolledUV += fixed2(MainTexMoveU, 0);
			SecondTexMoveScrolledUV += fixed2(SecondTexMoveU, SecondTexMoveV);
			ThirdTexMoveScrolledUV += fixed2(ThirdTexMoveU, ThirdTexMoveV);
//			FourthTexMoveScrolledUV += fixed2(FourthTexMoveU, FourthTexMoveV);

			half c = tex2D(_MainTex, IN.uv_MainTex).r;
			half d = tex2D(_MainTex, SecondTexMoveScrolledUV).g;
			half f = tex2D(_MainTex, ThirdTexMoveScrolledUV).b;
 			half g = tex2D(_MainTex, IN.uv_MainTex).a; //replace IN.uv_MainTex by FourthTexMoveScrolledUV if panning desired

			o.Albedo = ((c * 1) * (1 - g) + (f * 1 * g) + _Opacity) * _Color;
			//o.Alpha = (c * 8 + d * 8 + (f * 12) + e * 10) * pow(g, _Contrast);

			//o.Emission = o.Albedo * 15 * (_Color * half4(.9,.2,.4,1));
		}

		ENDCG
	}
}