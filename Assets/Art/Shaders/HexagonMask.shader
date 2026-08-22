Shader "UI/Hexagon Mask"
{
    Properties
    {
        [PerRendererData] _MainTex ("Texture", 2D) = "white" {}

        _Color ("Color", Color) = (1,1,1,1)

        // Unity UI Mask properties
        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _ColorMask ("Color Mask", Float) = 15

        _HexSize ("Hex Size", Range(0.5, 1.0)) = 0.95
        _Softness ("Edge Softness", Range(0.0, 0.05)) = 0.005
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "CanUseSpriteAtlas"="True"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]

        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 color  : COLOR;
                float2 uv     : TEXCOORD0;
            };

            sampler2D _MainTex;

            float4 _Color;

            float _HexSize;
            float _Softness;

            v2f vert(appdata v)
            {
                v2f o;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                o.color = v.color * _Color;

                return o;
            }

            // Flat-top hexagon
            float HexagonMask(float2 uv)
            {
                // Convert UV 0-1 → -1 to 1
                float2 p = uv * 2.0 - 1.0;

                p = abs(p);

                const float SQRT3 = 1.7320508;
                const float HALF_HEIGHT = 0.8660254;

                // Scale hexagon
                p /= _HexSize;

                // Distance from horizontal top/bottom
                float d1 = HALF_HEIGHT - p.y;

                // Distance from diagonal edges
                float d2 = 1.0 - p.x - p.y / SQRT3;

                // Inside when both are positive
                float d = min(d1, d2);

                return d;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float d = HexagonMask(i.uv);

                // Smooth hexagonal edge
                float mask = smoothstep(
                    0.0,
                    _Softness,
                    d
                );

                // IMPORTANT:
                // Pixels outside the hexagon are discarded.
                if (mask <= 0.001)
                    discard;

                fixed4 col = tex2D(_MainTex, i.uv);

                col *= i.color;

                col.a *= mask;

                return col;
            }

            ENDCG
        }
    }
}