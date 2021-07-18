// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/BlackAndWhite"
{
    Properties{
    _MainTex("MainTex",2D)="white"{}
    }

    SubShader{
      Pass{
      ZTest Always
      Cull Off
      ZWrite Off

      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      #include "UnityCG.cginc"
      sampler2D _MainTex;
      fixed _GrayFactor;

      struct v2f{
        float4 pos:SV_POSITION;
        half2 uv:TEXCOORD0;
      };


      v2f vert(appdata_img v){
        v2f o;
        o.pos=UnityObjectToClipPos(v.vertex);
        o.uv=v.texcoord;
        return o;
      }

      fixed4 frag(v2f i):SV_Target
      {
        fixed4 renderTex=tex2D(_MainTex,i.uv);
        //Extract brightness
        fixed luminance=0.2125*renderTex.r+0.7154*renderTex.g+0.0721*renderTex.b;
        //Create 0 saturation color
        fixed3 luminanceColor=fixed3(luminance,luminance,luminance);
        //Interpolate the color
        fixed3 finalColor=lerp(renderTex.rgb,luminanceColor,_GrayFactor);
        return fixed4(finalColor,1.0);
      }

      ENDCG
      }
    }
}
