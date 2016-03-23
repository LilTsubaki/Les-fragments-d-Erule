Shader "Custom/VFX_Wood_GroundCracks" {
	Properties {
		_MainTex ("VFX Texture", 2D) = "" {}
	}
    SubShader {
        Tags {Queue=Transparent}
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off
        Cull Back
	/*	BindChannels{
			Bind "vertex", vertex
			Bind "texCoord", texCoord
			Bind "color", color
		}*/
	 Pass {
			SetTexture[_MainTex]{ Combine texture, primary * previous}
        }
    }
}