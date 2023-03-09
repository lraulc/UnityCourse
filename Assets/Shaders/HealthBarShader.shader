Shader "Custom Shaders/Healthbar"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _HealthColor ("Color", Color) = (1,1,1,1)
        _DarkColor ("Color", Color) = (1,1,1,1)
        _LineAmount ("Lines", Float) = 1.0
        _Speed ("Speed", Float) = 1.0
        _Gradient("Gradient", Float) = 1.0
        _GradientContrast("Gradient Contrast", Vector) = (0,0,0,0)
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            fixed4 _HealthColor;
            fixed _LineAmount;
            fixed _Speed;
            fixed _Gradient;
            fixed2 _GradientContrast;
            fixed4 _DarkColor;
            

            fixed4 frag (v2f i) : SV_Target
            {
                fixed offsetX = _Time.x * _Speed;
                fixed sineWave = (abs(sin((i.uv.x * _LineAmount) + offsetX)));
                sineWave = smoothstep(0.1,0.2,sineWave);
                sineWave *= (smoothstep(0.1,0.6,i.uv.y));
                fixed invertedSineWave = 1 - (smoothstep(0.1,0.2,(i.uv.y * 5.0) - 1.0));
                _DarkColor *= invertedSineWave;
                _HealthColor *= sineWave;
                // _HealthColor += invertedSineWave;   
                fixed4 colSum = _DarkColor + _HealthColor; 
                fixed4 col = colSum * (tex2D(_MainTex, i.uv));
                return col;
            }
            ENDCG
        }
    }
}
