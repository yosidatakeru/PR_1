
Shader "Custom/GlitchShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _GlitchIntensity("Glitch Intensity", Range(0, 1)) = 0.2
        _Speed("Speed", Float) = 5.0
    }

        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 100

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"

                sampler2D _MainTex;
                float4 _MainTex_ST;
                float _GlitchIntensity;
                float _Speed;

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                float rand(float2 co)
                {
                    return frac(sin(dot(co.xy, float2(12.9898, 78.233))) * 43758.5453);
                }

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    float time = _Time.y * _Speed;

                // çrÇ¢ÉmÉCÉYÇÃÇΩÇﬂÇ… UV ÇëeÇ≠Ç∑ÇÈ
                float2 noiseUV = floor(i.uv * 20.0); // Å© Ç±Ç±Ç≈ëeÇ≥í≤êÆ
                float glitchLine = step(1.0 - _GlitchIntensity, rand(float2(time, noiseUV.y)));
                float glitchAmount = glitchLine * (rand(noiseUV + time) - 0.5) * 0.05;

                float2 uvOffset = i.uv + float2(glitchAmount, 0.0);

                // å≥ÇÃêFÇ∆ÉOÉäÉbÉ`êFÇï‚ä‘
                fixed4 originalCol = tex2D(_MainTex, i.uv);
                fixed4 glitchCol = tex2D(_MainTex, uvOffset);
                fixed4 col = lerp(originalCol, glitchCol, glitchLine);

                return col;
            }
            ENDCG
        }
        }
}