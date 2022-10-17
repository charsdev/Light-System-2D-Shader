Shader "Custom/Light"
{
	Properties
	{
		_MainTex ("Diffuse Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		_ContrastFactor ("Contrast Factor", Float) = 1.0
		_ColorFactor ("Color Factor", Float) = 0.5
		_IntensityFactor ("Intensity Variation Factor", Float) = 0.5
	}
    
	SubShader
	{
		Tags
		{ 
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"ForceNoShadowCasting" = "True"
		}
		
        Pass 
		{
            Blend One One
            ZWrite Off
            AlphaTest Greater 0.0  
			CGPROGRAM
		     
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			// User-specified properties
			uniform sampler2D _MainTex;

			uniform fixed4 _Color;
			uniform float _ContrastFactor;
			uniform float _ColorFactor;
			uniform float _IntensityFactor;

			struct VertexInput
			{
				float4 vertex : POSITION;
				float4 uv : TEXCOORD0;
				fixed4 color : COLOR;
			};
	
			struct VertexOutput
			{
				float4 pos : SV_POSITION;
				float4 uv : TEXCOORD0;
				fixed4 color : COLOR;
				float intensity : TEXCOORD1;
			};

			VertexOutput vert(VertexInput input)
			{
				VertexOutput output;
				output.pos = UnityObjectToClipPos(input.vertex);
				output.uv = input.uv;

				output.color = input.color;
				output.intensity = _IntensityFactor * _ContrastFactor * cos(_Time.z) * sin(_Time.w) * _CosTime.w + 1.0 * _ContrastFactor;
				return output;
			}

			float4 frag(VertexOutput input) : COLOR
			{
				float4 diffuseColor = tex2D(_MainTex, input.uv);
				diffuseColor.rgb = diffuseColor.rgb * _Color.rgb * input.color.rgb;
				diffuseColor.rgb *= diffuseColor.a * _Color.a * input.color.a;
				diffuseColor *= input.intensity;
				return float4(diffuseColor);
			}
			ENDCG
		}
	}
}