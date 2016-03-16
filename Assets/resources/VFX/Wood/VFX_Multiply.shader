Shader "Custom/VFX_Multiply_VcolBlended1" {
	Properties {
		_MainTex ("VFX Texture", 2D) = "" {}
	}
    SubShader {
        Tags {Queue=Transparent}
		Blend DstColor Zero
		ZWrite Off
        Cull Back
	 Pass {
			SetTexture[_MainTex]{ Combine texture, primary }
        }
    }
}