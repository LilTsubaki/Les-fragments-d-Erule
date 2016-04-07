Shader "VFX/Shield" {
	Properties{
		_MainTex("Base (R=Center G=Bubbles B=Glitches A=CenterAlpha", 2D) = "white" {}
		_Color("Tint", color) = (1,1,1)
		_SecondTexMoveSpeedU("Bubbles U PanSpeed", Range(-50,50)) = 0
		_SecondTexMoveSpeedV("Bubbles V PanSpeed", Range(-50,50)) = 0
		_ThirdTexMoveSpeedU("Glitches U PanSpeed", Range(-50,50)) = 0
		_ThirdTexMoveSpeedV("Glitches V PanSpeed", Range(-50,50)) = 0
		_Opacity("Opacity", Range(0,1)) = 0
		_Contrast("Contrast", Range(0.1, 5)) = 1
		_EmisStrength("Emissive Strength", Range(0,10)) = 1
		_Glossiness("Glossiness", Range(0, 1)) = 0
		_Metallic("Metallic", Range(0, 1)) = 0
//		_FourthTexMoveSpeedU("Alpha U PanSpeed", Range(-50,50)) = 0
//		_FourthTexMoveSpeedV("Alpha V PanSpeed", Range(-50,50)) = 0
	}

	SubShader{
		Tags{ "Queue" = "Geometry" }
		LOD 200
		ZWrite On
		Lighting Off
		//Cull Off
		//Blend One One
		//Blend SrcAlpha OneMinusSrcAlpha
//		BindChannels{
//			Bind "Color", color
//		}

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows //alpha

		sampler2D _MainTex;
		half3 _Color;
		half _Opacity;
		half _Contrast;
		half _EmisStrength;
		half _Glossiness;
		half _Metallic;

		fixed _SecondTexMoveSpeedU;
		fixed _SecondTexMoveSpeedV;
		fixed _ThirdTexMoveSpeedU;
		fixed _ThirdTexMoveSpeedV;
//		fixed _FourthTexMoveSpeedU;
//		fixed _FourthTexMoveSpeedV;

		struct Input {
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o) {

			fixed2 SecondTexMoveScrolledUV = IN.uv_MainTex;
			fixed2 ThirdTexMoveScrolledUV = IN.uv_MainTex;
//			fixed2 FourthTexMoveScrolledUV = IN.uv_MainTex;

			fixed SecondTexMoveU = _SecondTexMoveSpeedU * _Time;
			fixed SecondTexMoveV = _SecondTexMoveSpeedV * _Time;
			fixed ThirdTexMoveU = _ThirdTexMoveSpeedU * _Time;
			fixed ThirdTexMoveV = _ThirdTexMoveSpeedV * _Time;
//			fixed FourthTexMoveU = _FourthTexMoveSpeedU * _Time;
//			fixed FourthTexMoveV = _FourthTexMoveSpeedV * _Time;

			SecondTexMoveScrolledUV += fixed2(SecondTexMoveU, SecondTexMoveV);
			ThirdTexMoveScrolledUV += fixed2(ThirdTexMoveU, ThirdTexMoveV);
//			FourthTexMoveScrolledUV += fixed2(FourthTexMoveU, FourthTexMoveV);

			half c = tex2D(_MainTex, IN.uv_MainTex).r;
			half d = tex2D(_MainTex, SecondTexMoveScrolledUV).g;
			half f = tex2D(_MainTex, ThirdTexMoveScrolledUV).b;
 			half g = tex2D(_MainTex, IN.uv_MainTex).a; //replace IN.uv_MainTex by FourthTexMoveScrolledUV if panning desired

			o.Albedo = (c * (1 - g) + d * .6 + (f * g) + _Opacity) * _Color * _EmisStrength;
			o.Emission = o.Albedo * ((pow(c, _Contrast) * _EmisStrength)) * f;
			o.Metallic = _Metallic + d * .6;
			o.Smoothness = _Glossiness;
			o.Metallic = clamp(o.Metallic, 0, 1);
			o.Alpha = 1;
			//o.Alpha = (c * 8 + d * 8 + (f * 12) + e * 10) * pow(g, _Contrast);

			//o.Emission = o.Albedo * 15 * (_Color * half4(.9,.2,.4,1));
		}

		ENDCG
	}
}