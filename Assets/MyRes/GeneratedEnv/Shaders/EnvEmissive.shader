// Unlit emissive shader for windows / lantern glass / flames (vertex colored + fog).
Shader "Env/Emissive"
{
    Properties
    {
        _Color ("Tint", Color) = (1,1,1,1)
        _Intensity ("Intensity", Float) = 2.0
    }
    SubShader
    {
        Tags { "Queue" = "Geometry" "RenderType" = "Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            #include "UnityCG.cginc"

            struct appdata { float4 vertex : POSITION; fixed4 color : COLOR; };
            struct v2f { float4 pos : SV_POSITION; fixed4 color : COLOR; UNITY_FOG_COORDS(1) };
            fixed4 _Color;
            half _Intensity;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.color = v.color * _Color;
                UNITY_TRANSFER_FOG(o, o.pos);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 c = fixed4(i.color.rgb * _Intensity, 1.0);
                UNITY_APPLY_FOG(i.fogCoord, c);
                return c;
            }
            ENDCG
        }
    }
    FallBack Off
}
