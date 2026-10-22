
Shader "Custom/ToonOcean"
{
    Properties
    {
        _DeepColor ("Deep Ocean", Color) = (0.015, 0.12, 0.32, 1)
        _MidColor ("Ocean Blue", Color) = (0.02, 0.38, 0.68, 1)
        _LightColor ("Wave Highlight", Color) = (0.22, 0.78, 0.9, 1)
        _FoamColor ("Wave Crest", Color) = (0.75, 0.96, 1, 1)

        _ToonShadow ("Toon Shadow Threshold", Range(0, 1)) = 0.3
        _ToonLight ("Toon Light Threshold", Range(0, 1)) = 0.7
        _CrestThreshold ("Crest Threshold", Range(0, 1)) = 0.72
        _CrestStrength ("Crest Strength", Range(0, 1)) = 0.45
    }

    SubShader
    {
        Tags
        {
            "RenderPipeline" = "UniversalPipeline"
            "RenderType" = "Opaque"
            "Queue" = "Geometry"
        }

        Pass
        {
            Name "OceanForward"
            Tags { "LightMode" = "UniversalForward" }

            Cull Back
            ZWrite On
            ZTest LEqual

            HLSLPROGRAM

            #pragma vertex Vert
            #pragma fragment Frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            CBUFFER_START(UnityPerMaterial)
                half4 _DeepColor;
                half4 _MidColor;
                half4 _LightColor;
                half4 _FoamColor;
                float _ToonShadow;
                float _ToonLight;
                float _CrestThreshold;
                float _CrestStrength;
            CBUFFER_END

            float4 _OceanPlayerXZ;
            float4 _OceanWaveParams;
            float4 _OceanDetailParams;

            struct Attributes
            {
                float4 positionOS : POSITION;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 positionWS : TEXCOORD0;
                float3 normalWS : TEXCOORD1;
                float height : TEXCOORD2;
                float fineWave : TEXCOORD3;
            };

            // ëÂÇ´Ç»îgÇ∆ÇªÇÃî˜ï™ÇåvéZ
            float WaveHeight(
                float2 p,
                float time,
                out float2 derivative)
            {
                float h = 0;
                derivative = 0;

                // ï˚å¸Ç∆îgí∑ÇïœÇ¶ÇƒîgÇèdÇÀÇÈ
                float2 d1 = normalize(float2(1.0, 0.35));
                float k1 = 6.2831853 / _OceanWaveParams.y;
                float a1 = _OceanWaveParams.x;
                float q1 = dot(p, d1) * k1
                         + time * _OceanWaveParams.z;

                h += sin(q1) * a1;
                derivative += cos(q1) * a1 * k1 * d1;

                float2 d2 = normalize(float2(-0.45, 1.0));
                float k2 = 6.2831853 / (_OceanWaveParams.y * 0.62);
                float a2 = _OceanWaveParams.x * 0.25;
                float q2 = dot(p, d2) * k2
                         - time * _OceanWaveParams.z * 0.75;

                h += sin(q2) * a2;
                derivative += cos(q2) * a2 * k2 * d2;

                return h;
            }

            float FineWave(
                float2 p,
                float time,
                out float2 derivative)
            {
                float2 d1 = normalize(float2(1.0, 0.6));
                float2 d2 = normalize(float2(-0.7, 1.0));

                float k = 6.2831853 / max(_OceanDetailParams.y, 0.1);

                float q1 = dot(p, d1) * k
                         + time * _OceanDetailParams.z;

                float q2 = dot(p, d2) * k * 1.37
                         - time * _OceanDetailParams.z * 1.2;

                float a = _OceanDetailParams.x;

                derivative =
                    cos(q1) * a * k * d1
                    + cos(q2) * a * k * 1.37 * d2;

                return (
                    sin(q1) + sin(q2) * 0.5
                ) * a;
            }

            Varyings Vert(Attributes input)
            {
                Varyings output;

                float3 worldPos = TransformObjectToWorld(
                    input.positionOS.xyz);

                float time = _OceanWaveParams.w;

                float2 largeDerivative;
                float big = WaveHeight(
                    worldPos.xz, time, largeDerivative);

                float distanceToPlayer = distance(
                    worldPos.xz, _OceanPlayerXZ.xy);

                // âìÇ≠Ç≈ÇÕç◊Ç©Ç¢îgÇèôÅXÇ…è¡Ç∑
                float detailFade = 1.0 - smoothstep(
                    _OceanDetailParams.w,
                    _OceanDetailParams.w + 120.0,
                    distanceToPlayer);

                float2 fineDerivative;
                float fine = FineWave(
                    worldPos.xz, time, fineDerivative);

                worldPos.y += big + fine * detailFade;

                // îgÇÃåXÇ´Ç©ÇÁãﬂéóñ@ê¸ÇãÅÇﬂÇÈ
                float2 slope =
                    largeDerivative
                    + fineDerivative * detailFade;

                output.normalWS = normalize(
                    float3(-slope.x, 1.0, -slope.y));

                output.positionWS = worldPos;
                output.positionHCS = TransformWorldToHClip(
                    worldPos);

                output.height = big;
                output.fineWave = fine * detailFade;

                return output;
            }

            half4 Frag(Varyings input) : SV_Target
            {
                float3 normalWS = normalize(input.normalWS);

                Light mainLight = GetMainLight();

                float ndotl = saturate(dot(
                    normalWS, mainLight.direction));

                // 3íiäKÇÃÉgÉDÅ[Éìí≤ÉâÉCÉeÉBÉìÉO
                half3 color = _DeepColor.rgb;

                color = lerp(
                    color,
                    _MidColor.rgb,
                    step(_ToonShadow, ndotl));

                color = lerp(
                    color,
                    _LightColor.rgb,
                    step(_ToonLight, ndotl));

                // îgÇÃçÇÇ¢ïîï™Ç…ñæÇÈÇ¢ÉAÉNÉZÉìÉgÇâ¡Ç¶ÇÈ
                float crestSignal =
                    input.height * 0.5
                    + input.fineWave * 1.5;

                float crest = smoothstep(
                    _CrestThreshold,
                    _CrestThreshold + 0.12,
                    crestSignal);

                color = lerp(
                    color,
                    _FoamColor.rgb,
                    crest * _CrestStrength);

                color *= mainLight.color;

                return half4(color, 1.0);
            }

            ENDHLSL
        }
    }
}
