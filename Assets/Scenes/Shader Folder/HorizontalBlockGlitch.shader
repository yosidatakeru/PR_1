Shader "Custom/HorizontalBlockGlitch"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _GlitchIntensity("Glitch Intensity", Range(0, 1)) = 0.4
        _Speed("Speed", Float) = 5.0
        _BlockSize("Block Size", Float) = 30.0
        _Displacement("Displacement", Float) = 0.1
        _Alpha("Alpha", Range(0,1)) = 1.0 // 追加
    }

        SubShader
        {
            Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
            LOD 100
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off

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
                float _BlockSize;
                float _Displacement;
                float _Alpha;

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
                    return frac(sin(dot(co, float2(12.9898, 78.233))) * 43758.5453);
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

                    float yBlock = floor(i.uv.y * _BlockSize);
                    float noise = rand(float2(yBlock, time));
                    float glitch = step(1.0 - _GlitchIntensity, noise);

                    float xOffset = (rand(float2(yBlock * 13.0, time * 0.5)) - 0.5) * 2.0 * _Displacement;
                    float2 offsetUV = i.uv + float2(xOffset * glitch, 0.0);

                    fixed4 col = tex2D(_MainTex, offsetUV);

                    col.a *= _Alpha; // アルファ適用

                    return col;
                }
                ENDCG
            }
        }
}