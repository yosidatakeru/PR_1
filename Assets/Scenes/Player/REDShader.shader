Shader "Custom/REDShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
    }
        SubShader
    {
         Tags { "Queue" = "Overlay" } // 常に手前に描画
        Pass
        {
            ZTest Always    // 他のオブジェクトの奥にあっても描画
            ZWrite Off      // 深度バッファに書き込まない
            Cull Off        // 両面描画
            Blend SrcAlpha OneMinusSrcAlpha // 透過を有効化

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return fixed4(1.0, 0.0, 0.0, 1.0); 
            }

            ENDCG
        }
    }
}
