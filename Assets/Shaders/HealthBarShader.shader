Shader "Custom Shaders/Healthbar"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _HueTex ("Hue Texture", 2D) = "white" {}
        _HealthColor ("Color", Color) = (1,1,1,1)
        _LineColor ("LineColor", Color) = (1,1,1,1)
        _LineAmount ("Lines", Float) = 1.0
        _Speed ("Speed", Float) = 1.0
        _Gradient("Gradient", Float) = 1.0
        _GradientContrast("Gradient Contrast", Vector) = (0,0,0,0)
        _HueShift ("Hue Shift", Range(0,10)) = 0
        _Saturation ("Saturation", Range (0,5)) = 1
        _Brightness ("Brightness", Range (-1 , 1)) = 0
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
            sampler2D _HueTex;
            fixed3 _HealthColor;
            fixed3 _LineColor;
            fixed _LineAmount;
            fixed _Speed;
            fixed _Gradient;
            fixed2 _GradientContrast;
            fixed _HueShift;
            fixed _Brightness;
            fixed _Saturation;
            
            float3 HueShift(float3 color, float1 hueshift)
            {
                float3x3 RGB_YIQ = 
                float3x3 
                (0.299, 0.587, 0.114,
                0.5959, -0.2746, -0.3213,
                0.2115, -0.5227, 0.3112);

                float3x3 YIQ_RGB = 
                float3x3
                (1, 0.956, 0.619,
                1, -0.272, -0.647,
                1, -1.106, 1.703);
                
                float3 YIQ = mul(RGB_YIQ, color);
                float hue = atan2(YIQ.z,YIQ.y) + hueshift;
                float chroma = length(float2(YIQ.y,YIQ.z)) * _Saturation;


                float Y = YIQ.x + _Brightness;
                float I = chroma * cos(hue);
                float Q = chroma * sin(hue);

                float3 shiftedYIQ = float3(Y,I,Q);
                float3 newRGB = mul(YIQ_RGB, shiftedYIQ);

                return newRGB;
            }


            fixed4 frag (v2f i) : SV_Target
            {
                fixed offsetX = _Time.x * _Speed;
                fixed sineWave = (abs(sin((i.uv.x * _LineAmount) + offsetX)));
                sineWave = smoothstep(0.1,0.2,sineWave);
                sineWave *= (smoothstep(0.1,0.6,i.uv.y));
                fixed invertedSineWave = 1 - (smoothstep(0.1,0.2,(i.uv.y * 5.0) - 1.0));
                _LineColor *= invertedSineWave;
                _HealthColor *= sineWave;
                fixed4 col = tex2D(_MainTex, (i.uv));
                fixed tex_hue = tex2D(_HueTex, fixed2((i.uv.x * 0.5) + offsetX, (i.uv.y * 0.1) + offsetX)).r;
                fixed3 colSum = _LineColor + HueShift(_HealthColor, _HueShift + tex_hue);
                col.rgb *= colSum; 
                return col;
            }
            ENDCG
        }
    }
}
