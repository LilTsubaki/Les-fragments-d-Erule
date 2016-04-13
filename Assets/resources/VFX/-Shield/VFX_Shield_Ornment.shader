Shader ">VFX/Shield_Ornment" {
	Properties{
		_Texture("Texture", 2D) = "white" {}
		_Mask("Mask", 2D) = "white" {}
		_Color("Tint", color) = (1,1,1,1)
		_Strength("Strength", Range(0,50)) = 0
		_Contrast("Contrast", Range(0,12)) = 0
		_SmokeMoveSpeedU("Smoke U PanSpeed", Range(-50,50)) = 0 
		_SmokeMoveSpeedV("Smoke V PanSpeed", Range(-50,50)) = 0
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
		#pragma surface surf Lambert noshadows
	
		sampler2D _Texture;
		sampler2D _Mask;

		half4 _Color;
		half _Strength;
		half _Contrast;

		fixed _SmokeMoveSpeedU;
		fixed _SmokeMoveSpeedV;

		struct Input {
			float2 uv_Texture;
			float2 uv_Mask;
		};

		void surf(Input IN, inout SurfaceOutput o) {

			fixed2 SmokeMoveScrolledUV = IN.uv_Texture;

			fixed SmokeMoveU = _SmokeMoveSpeedU * _Time;
			fixed SmokeMoveV = _SmokeMoveSpeedV * _Time;

			SmokeMoveScrolledUV += fixed2(SmokeMoveU, SmokeMoveV);

			half c = tex2D(_Texture, SmokeMoveScrolledUV);
 			half d = tex2D(_Mask, IN.uv_Mask);

			//o.Albedo = c * _Color;
			o.Albedo = pow(d, _Contrast) * c * _Color * _Strength;

			//o.Alpha =  pow(d, _Contrast) * c * _Strength;
		}

		ENDCG
	}
}