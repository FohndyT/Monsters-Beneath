Shader "Custom/ScreenDoorTransparency"
{
    Properties
    {
        _DitheredAlpha ("Dithered Alpha", Range(0,1)) = 1.0
        _BaseColor ("Base Color", Color) = (0,0,0,1)
        _MainTex("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags
        {  "RenderType"="Opaque"  }

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        float _DitheredAlpha;
        float4 _BaseColor;
        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float4 screenPos;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _BaseColor;
            o.Albedo = c.rgb;
            o.Alpha = c.a;

            float Bayer_Matrix[16] =
            {
                1.0 / 17.0,  9.0 / 17.0,  3.0 / 17.0,  11.0 / 17.0,
                13.0 / 17.0, 5.0 / 17.0,  15.0 / 17.0, 7.0 / 17.0,
                4.0 / 17.0,  12.0 / 17.0, 2.0 / 17.0,  10.0 / 17.0,
                16.0 / 17.0, 8.0 / 17.0,  14.0 / 17.0, 6.0 / 17.0
            };
            float2 spos = IN.screenPos.xy / IN.screenPos.w;
            spos *= _ScreenParams.xy;
            int id = (int(spos.x) % 4) * 4 + int(spos.y) % 4;
            clip(_DitheredAlpha - Bayer_Matrix[id]);
        }
        ENDCG
    }
    Fallback "Diffuse"
}