// Vertex-colored lit surface shader used by all generated environment meshes.
Shader "Env/VertexColorLit"
{
    Properties
    {
        _Smoothness ("Smoothness", Range(0,1)) = 0.12
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 250
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows addshadow
        #pragma target 3.0
        struct Input { float4 color : COLOR; };
        half _Smoothness;
        half _Metallic;
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            o.Albedo = IN.color.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Smoothness;
            o.Alpha = 1.0;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
