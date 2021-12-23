Shader "Custom/Crosshair"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1,1,1,1)
        _Radius("Radius", Float) = 0.1
        _Thickness("Thickness", Float) = 0.1
        _ThicknessDark("ThicknessDark", Float) = 0.1
    }

        SubShader
        {
            Tags
            {
                "Queue" = "Transparent"
                "IgnoreProjector" = "True"
                "RenderType" = "Transparent"
                "PreviewType" = "Plane"
                "CanUseSpriteAtlas" = "True"
            }

            Cull Off
            Lighting Off
            ZWrite Off
            ZTest[unity_GUIZTestMode]
            Blend SrcAlpha OneMinusSrcAlpha

            Pass
            {
                Name "Default"
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma target 2.0

                #include "UnityCG.cginc"
                #include "UnityUI.cginc"

                struct appdata_t
                {
                    float4 vertex   : POSITION;
                    float4 color    : COLOR;
                    float2 texcoord : TEXCOORD0;
                    UNITY_VERTEX_INPUT_INSTANCE_ID
                };

                struct v2f
                {
                    float4 vertex   : SV_POSITION;
                    fixed4 color : COLOR;
                    float2 texcoord  : TEXCOORD0;
                    float4 worldPosition : TEXCOORD1;
                    UNITY_VERTEX_OUTPUT_STEREO
                };

                sampler2D _MainTex;
                fixed4 _Color;
                float4 _ClipRect;
                float4 _MainTex_ST;

                float _Radius;
                float _Thickness;
                float _ThicknessDark;

                v2f vert(appdata_t v)
                {
                    v2f OUT;
                    UNITY_SETUP_INSTANCE_ID(v);
                    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                    OUT.worldPosition = v.vertex;
                    OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

                    OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

                    OUT.color = v.color * _Color;
                    return OUT;
                }

                fixed4 frag(v2f IN) : SV_Target
                {
                    // "normalize" values
                    float radius = _Radius * 0.001;
                    float thickness = _Thickness * 0.001;
                    float thicknessDark = (_Thickness + _ThicknessDark) * 0.001;

                    // set output color to transparent
                    float4 color = float4(1, 1, 1, 0);

                    // get distance from center
                    float d = distance(float2(0.5, 0.5), IN.texcoord);

                    // apply dark
                    if (radius - thicknessDark <= d && d <= radius + thicknessDark)
                    {
                        color = float4(0, 0, 0, 1);
                    }

                    // apply light
                    if (radius - thickness <= d && d <= radius + thickness)
                    {
                        color = float4(1, 1, 1, 1);
                    }

                    return color;
                }

            ENDCG
            }
        }
}