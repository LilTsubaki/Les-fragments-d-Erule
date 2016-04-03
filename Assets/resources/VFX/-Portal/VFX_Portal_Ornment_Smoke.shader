Shader "VFX/Portals_Ornment_Smoke" {
	Properties{
		_Smoke("Smoke", 2D) = "white" {}
		_Color("Tint", color) = (1,1,1,1)
		_Contrast("Contrast", Range(0,12)) = 0
		_SmokeMoveSpeedU("Smoke U PanSpeed", Range(-50,50)) = 0 
		_SmokeMoveSpeedV("Smoke V PanSpeed", Range(-50,50)) = 0
	}

	SubShader{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		ZWrite Off
		Cull Off
		Blend SrcAlpha OneMinusSrcAlpha
//		BindChannels{
//			Bind "Color", color
//		}

		CGPROGRAM
		#pragma surface surf Lambert
	
		sampler2D _Smoke;
		half4 _Color;
		half _Contrast;
		fixed _SmokeMoveSpeedU;
		fixed _SmokeMoveSpeedV;

		struct Input {
			float2 uv_Smoke;
		};

		void surf(Input IN, inout SurfaceOutput o) {

			fixed2 SmokeMoveScrolledUV = IN.uv_Smoke;

			fixed SmokeMoveU = _SmokeMoveSpeedU * _Time;
			fixed SmokeMoveV = _SmokeMoveSpeedV * _Time;

			SmokeMoveScrolledUV += fixed2(SmokeMoveU, SmokeMoveV);

			half c = tex2D(_Smoke, SmokeMoveScrolledUV).r;
 			half d = tex2D(_Smoke, IN.uv_Smoke).a;

			o.Albedo = c * _Color;

			o.Alpha =  pow(d, _Contrast);
		}

		ENDCG
	}
}