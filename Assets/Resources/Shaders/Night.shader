Shader "Lights/Night"
{
	Properties
	{
		_MainTex ("Base(RGB)", 2D) = "white" {}
		_ColorFactor ("Tint", Color) = (1, 1, 1, 1)
	}

	SubShader
	{
        Pass 
        {
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"
			uniform sampler2D _MainTex;
			uniform sampler2D _MainTex_ST;      

			uniform float4 ShadowColour;
			uniform float4 _Lights[4];
			uniform float _AspectRatios[4];

			fixed4 frag(v2f_img input) : COLOR
			{
				float4 main = tex2D(_MainTex, input.uv) * ShadowColour;
				float2 ratio = 0;
				float delta = 0;

				for (int i = 0; i < 4; i++) 
				{
					ratio = float2(1, 1 / _AspectRatios[i]);
					float ray = length((_Lights[i].xy - input.uv.xy) * ratio);
					delta += smoothstep(_Lights[i].z, 0, ray) * _Lights[i].w;
				}

				main.rgb = lerp(main.rgb, tex2D(_MainTex, input.uv), delta);
				return fixed4(main);
			}
			ENDCG
        }
	}
}