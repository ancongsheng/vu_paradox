Shader "Unlit/TextureNew"
{
	Properties
	{
		_MainTex ("Base (RGB), Alpha (A)", 2D) = "white" {}
	}
	
	SubShader {
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}
		LOD 100
	
		Pass {
			//Cull Off
			Lighting Off
			ZWrite Off
			
			SetTexture [_MainTex] { combine texture } 
		}
	}
	
}