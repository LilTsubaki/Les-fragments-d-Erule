Shader "Custom/VFX_Multiply_VcolBlended" {
	Properties{
		_MainTex("VFX Texture", 2D) = "" {}
	}
		SubShader{
			Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
			//ZWrite Off
			Cull Back
			Blend SrcAlpha OneMinusSrcAlpha
				BindChannels{
					Bind "vertex", vertex
					Bind "texCoord", texCoord
					Bind "color", color
				}
			Pass{
			SetTexture[_MainTex]{ combine texture, primary }
			}
	}
}