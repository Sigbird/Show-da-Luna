// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "YupiStudios/Luna/ColorableObjectsShader" {
	Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
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
        Fog { Mode Off }
        Blend One OneMinusSrcAlpha

        Pass
        {
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile DUMMY PIXELSNAP_ON
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                half2 texcoord  : TEXCOORD0;                
            };

            fixed4 _Color;
            
            
            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color * _Color;
                #ifdef PIXELSNAP_ON
                OUT.vertex = UnityPixelSnap (OUT.vertex);
                #endif

                return OUT;
            }

            sampler2D _MainTex;
			uniform float4 _MainTex_TexelSize;
			
			float GetMaxDiff(v2f IN, int space) 
			{
				fixed4 n = tex2D(_MainTex,IN.texcoord+float2(0,_MainTex_TexelSize.y*space));
                fixed4 s = tex2D(_MainTex,IN.texcoord+float2(0,-_MainTex_TexelSize.	y*space));
                fixed4 e = tex2D(_MainTex,IN.texcoord+float2(_MainTex_TexelSize.x*space,0));
                fixed4 w = tex2D(_MainTex,IN.texcoord+float2(-_MainTex_TexelSize.x*space,0));
                fixed4 ne = tex2D(_MainTex,IN.texcoord+float2(_MainTex_TexelSize.x*space,_MainTex_TexelSize.y*space));
                fixed4 nw = tex2D(_MainTex,IN.texcoord+float2(-_MainTex_TexelSize.x*space,_MainTex_TexelSize.y*space));
                fixed4 se = tex2D(_MainTex,IN.texcoord+float2(_MainTex_TexelSize.x*space,-_MainTex_TexelSize.y*space));
                fixed4 sw = tex2D(_MainTex,IN.texcoord+float2(-_MainTex_TexelSize.x*space,-_MainTex_TexelSize.y*space));
                
                
                
                float diff1 = abs(n.r - s.r) + abs(n.g - s.g) + abs(n.b - s.b);
                float diff2 = abs(e.r - w.r) + abs(e.g - w.g) + abs(e.b - w.b);
                float diff3 = abs(ne.r - sw.r) + abs(ne.g - sw.g) + abs(ne.b - sw.b);
                float diff4 = abs(nw.r - se.r) + abs(nw.g - se.g) + abs(nw.b - se.b);
                
                float max_diff = 0;
                
                if (diff1 > diff2 && diff1 > diff3 && diff1 > diff4)
                	max_diff = diff1;
                else if (diff2 > diff3 && diff2 > diff4)
                	max_diff = diff2;
                else if (diff3 > diff4)
                	max_diff = diff3;
				else
                	max_diff = diff4;
                	
				return max_diff;
			}
			
            fixed4 frag(v2f IN) : SV_Target
            {
                fixed4 c = tex2D(_MainTex, IN.texcoord);
                
                float max_diff = GetMaxDiff(IN,1);
                float max_diff2 = GetMaxDiff(IN,2);
                	                
                fixed diffa = abs(c.r - c.g);
                fixed diffb = abs(c.r - c.b);
                fixed diffc = abs(c.g - c.b);                
                
                float threshold = 0.8;
                
                if (diffa < 0.10 && diffb < 0.10 && diffc < 0.10)
					c = c * IN.color;
				else if (max_diff > threshold || max_diff2 > threshold)				
					c = c * IN.color;
                c.rgb *= c.a;

                return c;
            }
        ENDCG
        }
    }
	FallBack "Sprites/Default"
}
