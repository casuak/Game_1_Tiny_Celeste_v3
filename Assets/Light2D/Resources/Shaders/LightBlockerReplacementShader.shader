Shader "Light2D/Internal/LightBlockerReplacementShader"
{
	SubShader
	{
		Tags { "RenderType" = "LightBlocker" }

		Pass
			{
				Name "LightBlock"
				Lighting Off
				ZWrite Off
				Blend One OneMinusSrcAlpha

			CGPROGRAM
				#pragma vertex SpriteVert
				#pragma fragment SpriteFragCustom
				#pragma target 2.0
				#pragma multi_compile_instancing
				#pragma multi_compile _ PIXELSNAP_ON
				#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
				#include "UnitySprites.cginc"

				half4 _LightBlockColor;
				half _SpriteBlend;

				fixed4 SpriteFragCustom(v2f IN) : SV_Target
				{
					fixed4 c = lerp(1,SpriteFrag(IN),_SpriteBlend);
					c *= _LightBlockColor;
					return c;
				}
			ENDCG
			}
	}

	Fallback Off
}
