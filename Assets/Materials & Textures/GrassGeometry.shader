// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Grass" {
Properties {
      _MainColor ("Main Color", Color) = (0.5,0.5,0.5,0.0)
      _MainTex ("Main(RGB)", 2D) = "white" {}
      _SeconTex ("Second(RGB)", 2D) = "white" {}
      _MaskTex ("Mask(A)", 2D) = "white" {}
      _BumpMap ("Bumpmap(RGB)", 2D) = "bump" {}
      _RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
      _RimPower ("Rim Power", Range(0.5,20.0)) = 5.0
    }
    SubShader {
      Tags { "RenderType" = "Opaque" }
      CGPROGRAM
      #pragma surface surf Lambert
      struct Input {
          float2 uv_MainTex;
          float2 uv_SeconTex;
          float2 uv_MaskTex;
          float2 uv_BumpMap;
          float3 viewDir;
      };
      float4 _MainColor;
      sampler2D _MainTex;
      sampler2D _SeconTex;
      sampler2D _MaskTex;
      sampler2D _BumpMap;
      float4 _RimColor;
      float _RimPower;
     
      void surf (Input IN, inout SurfaceOutput o) {
          half4 main = tex2D (_MainTex, IN.uv_MainTex);
          half4 sec = tex2D (_SeconTex, IN.uv_SeconTex);
          half4 mask = tex2D (_MaskTex, IN.uv_MaskTex);
         
          o.Albedo = lerp(main.rgb, sec.rgb, mask.a);
          o.Albedo *= _MainColor;
          o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
          half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
          o.Emission = _RimColor.rgb * pow (rim, _RimPower);
      }
      ENDCG
    }
    Fallback "Diffuse"
  }