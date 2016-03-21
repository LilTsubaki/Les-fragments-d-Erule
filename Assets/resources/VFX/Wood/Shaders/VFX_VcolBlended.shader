Shader "Custom/VFX_Multiply_VcolBlended" {
//	Properties{
//		_MainTex("VFX Texture", 2D) = "" {}
//	}
//		SubShader{
//			Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
//			//ZWrite Off
//			Cull Back
//			Blend SrcAlpha OneMinusSrcAlpha
//				/*BindChannels{
//					Bind "vertex", vertex
//					Bind "texCoord", texCoord
//					Bind "color", color
//				}*/
//			Pass{
//			SetTexture[_MainTex]{ combine texture, primary }
//			}
//	}
//}
		Properties{
			/*_Color("Main Color", Color) = (1,1,1,1)*/
			_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
			/*_Cutoff("Mask cutoff", Range(0,1)) = 0*/
			MaskTex("Mask (RGB=unused, A=mask)", 2D) = "white" {}
			_MaskTexMoveSpeedU("U Move Speed", Range(-100,100)) = 0
			_MaskTexMoveSpeedV("V Move Speed", Range(-100,100)) = -20
		}

			SubShader{
			Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
			LOD 200

			CGPROGRAM
#pragma surface surf Lambert alpha

			sampler2D _MainTex;
		sampler2D _MaskTex;
		/*fixed4 _Color;*/
		/*fixed _Cutoff;*/
		fixed _MaskTexMoveSpeedU;
		fixed _MaskTexMoveSpeedV;

		struct Input {
			float2 uv_MainTex;
			float2 uv_MaskTex;
		};

		void surf(Input IN, inout SurfaceOutput o) {

			// Moving

			fixed2 MaskTexMoveScrolledUV = IN.uv_MaskTex;

			fixed MaskTexMoveU = _MaskTexMoveSpeedU * _Time;
			fixed MaskTexMoveV = _MaskTexMoveSpeedV * _Time;

			MaskTexMoveScrolledUV += fixed2(MaskTexMoveU, MaskTexMoveV);

			// Alpha cutoff

			fixed4 mask = tex2D(_MaskTex, MaskTexMoveScrolledUV);
			//clip(mask.a - _Cutoff*1.004); // 1.004 = 1+(1/255) to make sure also white is clipped

			fixed3 c = tex2D(_MainTex, IN.uv_MainTex)/* * _Color*/;
			o.Albedo = c.rgb;
			o.Alpha = mask.a;
		}

		ENDCG
		}

			Fallback "Transparent/Diffuse"
	}