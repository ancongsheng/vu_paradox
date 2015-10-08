Shader "Unlit/DecalBG" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_DecalColor ( "Decal Color", Color ) = (0,0,0,0.75)
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_DecalTex ("Decal (RGBA)", 2D) = "black" {}
}

SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 100
	
    CGPROGRAM
        #pragma surface surf Unlight

        sampler2D _MainTex;
        sampler2D _DecalTex;
        fixed4 _Color;
        fixed4 _DecalColor;

        struct Input {
	        float2 uv_MainTex;
	        float2 uv_DecalTex;
        };

          half4 LightingUnlight (SurfaceOutput s, half3 lightDir, half atten) {
              half4 c;
              c.rgb = s.Albedo;
              c.a = s.Alpha;
              return c;
          }

        void surf (Input IN, inout SurfaceOutput o) {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
	        half4 decal = tex2D(_DecalTex, IN.uv_DecalTex);
	        decal.rgb *= _DecalColor.rgb;
	        c.rgb = lerp (c.rgb, decal.rgb, decal.a * _DecalColor.a);
	        c *= _Color;
	        o.Albedo = c.rgb;
	        o.Alpha = c.a;
        }
    ENDCG
}

Fallback "Diffuse"
}
