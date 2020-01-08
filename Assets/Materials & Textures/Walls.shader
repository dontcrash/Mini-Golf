
/*
Shader "Custom/Walls"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
    }
 
    SubShader
    {
        Tags
        {
            "IgnoreProjector" = "True"
            "RenderType"      = "Opaque"
        }
 
        LOD 200
 
        // extra pass that renders to depth buffer only
        Pass
        {
            ZWrite On
            ColorMask 0
        }
 
        UsePass "Transparent/Diffuse/FORWARD"
    }
    FallBack "Diffuse"
}
*/

Shader "Custom/Walls" {
Properties
    {
        _Color("Color",Color) = (0,0,1,0.1)
    }
    SubShader
    {
Tags {"Queue"="Transparent" "IgnoreProjector"="true" "RenderType"="Transparent"}
ZWrite Off Blend SrcAlpha OneMinusSrcAlpha Cull Off

        LOD 100

        Pass
        {
            Stencil {
                Ref 0
                Comp Equal
                Pass IncrSat 
                Fail IncrSat 
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            fixed4 _Color;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = _Color;
                return col;
            }
            ENDCG
        }
    }
}