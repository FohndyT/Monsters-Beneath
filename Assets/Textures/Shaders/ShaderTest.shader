// https://www.youtube.com/watch?v=gY1Mx4kkZPU&list=PLVccL83FtuKvbJY6zm7AEwWmrtEF18xLF&index=25&ab_channel=DanielIlett

Shader "Unlit/ShaderTest"
{
    Properties
    {
        _TexturePrincipal ("Texture", 2D) = "white" {}
        _CouleurDeBase ("Couleur", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        
        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

        CBUFFER_START(unityPerMaterial)

            float4 _CouleurDeBase;
        
        CBUFFER_END

        TEXTURE2D(_TexturePrincipal);
        SAMPLER(sampler_TexturePrincipal);

        struct SommetsIntrants
        {
            float4 position : Position;
            float2 uv : TEXCOORD0;
        };

        struct SommetsExtrants
        {
            float4 position : SV_Position;
            float2 uv : TEXCOORD0;
        };

        ENDHLSL

        Pass
        {
            HLSLPROGRAM

            #pragma vertex sommets
            #pragma fragment pixels

            SommetsExtrants sommets(SommetsIntrants intrant)
            {
                SommetsExtrants extrant;
                extrant.position = TransformObjectToHClip(intrant.position.xyz);
                extrant.uv = intrant.uv;

                return extrant;
            }

            float4 pixels(SommetsExtrants extrant) : SV_TARGET
            {
                float4 textureDebase = SAMPLE_TEXTURE2D(_TexturePrincipal,sampler_TexturePrincipal,extrant.uv);
                return textureDebase * _CouleurDeBase;
            }
            
            ENDHLSL
        }
    }
}
