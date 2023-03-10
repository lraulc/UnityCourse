Shader "Custom Shaders/ColorFlicker"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1.0,1.0,1.0, 1.0)
        _Speed ("Animation Speed", Float) = 0.0
    }
    SubShader
    {

        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work

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

            sampler2D _MainTex;
            fixed4 _Color;
            fixed _Speed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 flickerAnim(v2f i ,float4 color, float1 animSpeed)
            {
                float time = abs(sin(_Time.x * animSpeed));
                color.rgb *= time;
                return color;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed Alpha = tex2D(_MainTex, i.uv).a;
                fixed4 col = tex2D(_MainTex, i.uv).rgba;
                fixed colR = tex2D(_MainTex, i.uv).b;
                col.rgb = col + flickerAnim(i,_Color, _Speed);
                col.rgb *= Alpha;
                return col.rgba;
            }
            ENDCG
        }
    }
}
