// Simple stylised night sky: vertical gradient + soft moon, no textures required.
Shader "Env/NightSky"
{
    Properties
    {
        _TopColor ("Top Color", Color) = (0.012, 0.02, 0.045, 1)
        _HorizonColor ("Horizon Color", Color) = (0.05, 0.075, 0.13, 1)
        _GroundColor ("Below Horizon Color", Color) = (0.01, 0.012, 0.02, 1)
        _MoonColor ("Moon Color", Color) = (0.75, 0.80, 0.95, 1)
        _MoonDir ("Moon Direction", Vector) = (0.28, 0.32, -0.90, 0)
        _MoonSize ("Moon Sharpness", Float) = 700
    }
    SubShader
    {
        Tags { "Queue" = "Background" "RenderType" = "Background" "PreviewType" = "Skybox" }
        Cull Off
        ZWrite Off
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata { float4 vertex : POSITION; };
            struct v2f { float4 pos : SV_POSITION; float3 dir : TEXCOORD0; };

            fixed4 _TopColor, _HorizonColor, _GroundColor, _MoonColor;
            float4 _MoonDir;
            float _MoonSize;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.dir = v.vertex.xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 d = normalize(i.dir);
                float up = saturate(d.y);
                fixed3 col = lerp(_HorizonColor.rgb, _TopColor.rgb, pow(up, 0.55));
                if (d.y < 0.0)
                    col = lerp(_HorizonColor.rgb, _GroundColor.rgb, saturate(-d.y * 6.0));

                float3 md = normalize(_MoonDir.xyz);
                float mdv = saturate(dot(d, md));
                float disc = pow(mdv, _MoonSize) * 1.9;
                float halo = pow(mdv, 42.0) * 0.16;
                float glow = pow(mdv, 6.0) * 0.03;
                col += _MoonColor.rgb * (disc + halo + glow);

                // faint high cloud bands
                float band = sin(d.x * 6.0 + d.z * 3.0) * sin(d.y * 22.0 + d.x * 4.0);
                col += _HorizonColor.rgb * saturate(band) * 0.05;

                return fixed4(col, 1.0);
            }
            ENDCG
        }
    }
    FallBack Off
}
