Shader "VFX/Portals_Ornment" {
	Properties{
		_MainTex("Base (R=Lightning1 G=Lightning2 B=Sparks A=Alpha", 2D) = "white" {}
		_Smoke("Smoke", 2D) = "white" {}
		_Color("Tint", color) = (1,1,1,1)
		_Contrast("Contrast", Range(0,12)) = 0
		_MainTexMoveSpeedU("L1 U PanSpeed", Range(-50,50)) = 0
		_SecondTexMoveSpeedU("L2 U PanSpeed", Range(-50,50)) = 0
		_Third1TexMoveSpeedU("OuterSparks U PanSpeed", Range(-50,50)) = 0
		_Third2TexMoveSpeedU("InnerSparks U PanSpeed", Range(-50,50)) = 0
		_Third2TexMoveSpeedV("InnerSparks V PanSpeed", Range(-50,50)) = 0
//		_FourthTexMoveSpeedU("Alpha U PanSpeed", Range(-50,50)) = 0
//		_FourthTexMoveSpeedV("Alpha V PanSpeed", Range(-50,50)) = 0
	}

	SubShader{
		Tags{ "Queue" = "Transparent+99" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		ZWrite Off
			Cull Off
		Blend One One
//		BindChannels{
//			Bind "Color", color
//		}

		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		sampler2D _Smoke;
		half4 _Color;
		half _Contrast;
		fixed _MainTexMoveSpeedU;
		fixed _SecondTexMoveSpeedU;
		fixed _Third1TexMoveSpeedU;
		fixed _Third2TexMoveSpeedU;
		fixed _Third2TexMoveSpeedV;
//		fixed _FourthTexMoveSpeedU;
//		fixed _FourthTexMoveSpeedV;

		struct Input {
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutput o) {

			fixed2 MainTexMoveScrolledUV = IN.uv_MainTex;
			fixed2 SecondTexMoveScrolledUV = IN.uv_MainTex;
			fixed2 Third1TexMoveScrolledUV = IN.uv_MainTex;
			fixed2 Third2TexMoveScrolledUV = IN.uv_MainTex;
//			fixed2 FourthTexMoveScrolledUV = IN.uv_MainTex;

			fixed MainTexMoveU = _MainTexMoveSpeedU * _Time;
			fixed SecondTexMoveU = _SecondTexMoveSpeedU * _Time;
			fixed Third1TexMoveU = _Third1TexMoveSpeedU * _Time;
			fixed Third2TexMoveU = _Third2TexMoveSpeedU * _Time;
			fixed Third2TexMoveV = _Third2TexMoveSpeedV * _Time;
//			fixed FourthTexMoveU = _FourthTexMoveSpeedU * _Time;
//			fixed FourthTexMoveV = _FourthTexMoveSpeedV * _Time;

			MainTexMoveScrolledUV += fixed2(MainTexMoveU, 0);
			SecondTexMoveScrolledUV += fixed2(SecondTexMoveU, 0);
			Third1TexMoveScrolledUV += fixed2(Third1TexMoveU, 0);
			Third2TexMoveScrolledUV += fixed2(Third2TexMoveU, Third2TexMoveV);
//			FourthTexMoveScrolledUV += fixed2(FourthTexMoveU, FourthTexMoveV);

			half c = tex2D(_MainTex, MainTexMoveScrolledUV).r;
			half d = tex2D(_MainTex, SecondTexMoveScrolledUV).g;
			half e = tex2D(_MainTex, Third1TexMoveScrolledUV).b;
			half f = tex2D(_MainTex, Third2TexMoveScrolledUV).b;
 			half g = tex2D(_MainTex, IN.uv_MainTex).a; //replace IN.uv_MainTex by FourthTexMoveScrolledUV if panning desired

			o.Albedo = (c * 8 + d * 8 + (f * 12) + e *10) * _Color * pow (g, _Contrast);

			//o.Emission = o.Albedo * 15 * (_Color * half4(.9,.2,.4,1));
		}

		ENDCG
	}
}