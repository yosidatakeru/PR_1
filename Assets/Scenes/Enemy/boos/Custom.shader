Shader "Custom/Custom"
{
   
   Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _GridSize ("Grid Size", Float) = 302
        _Height ("Height", Float) = 1
        _RandomScale ("Random Scale", Float) = 1
        _GradientPower ("Gradient Power", Float) = 1
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
            "Queue"="Geometry"
        }

        Pass
        {
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float4 _Color;
            float _GridSize;
            float _Height;
            float _RandomScale;
            float _GradientPower;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float randomValue : TEXCOORD1;
            };

            float Random(float2 p)
            {
                p = frac(p * float2(123.34, 456.21));
                p += dot(p, p + 45.32);

                return frac(p.x * p.y);
            }

            v2f vert(appdata v)
            {
                v2f o;

                float2 gridUV = v.uv * _GridSize;

                float2 cell = floor(gridUV);

                float randomValue = Random(cell);

                float gradient = v.uv.x;

                gradient = pow(gradient, _GradientPower);

                float height =
                    randomValue *
                    _RandomScale *
                    gradient;

                float3 position = v.vertex.xyz;

                position.y += height * _Height;

                o.vertex = UnityObjectToClipPos(float4(position, 1));

                o.uv = v.uv;
                o.randomValue = randomValue;

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 color = _Color.rgb;

                return float4(color, 1);
            }

            ENDHLSL
        }
    }
}
