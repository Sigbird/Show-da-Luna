// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "YupiStudios/Luna/SpriteGaugeShader" {

	 Properties { 
		 _MainTex ("Base (RGB)", 2D) = "white" {}
		 _GaugeFullTex ("Base (RGB)", 2D) = "white" {} // Textura Gauge Cheia
		 _StartOffset ("Start Offset", Range(0,1.0)) = 0.3    // Ponto de inicio da troca de textura
		 _EndOffset ("End Offset", Range(0,1.0)) = 0.2    // Ponto termino de troca de textura
		 _CutOffPrevious ("CutOff Previous", Range(0,1.0)) = 0.5    // Ponto de troca de textura
		 _CutOffFinal ("CutOff Final", Range(0,1.0)) = 0.5    // Ponto de troca de textura
		 _PreviousColor ("Red Previous Color", Color) = (1,1,1,1)
		 [MaterialToggle] _HGauge ("Horizontal Gauge", Int) = 0		 
	 }

	SubShader { 

		Tags
		{
			"Queue"="Transparent"		
			"RenderType"="Transparent"		
			"IgnoreProjector"="True"
			"CanUseSpriteAtlas"="True"
		}
		

		ZTest Always Lighting Off Cull Off ZWrite Off Fog { Mode off }
		Blend SrcAlpha OneMinusSrcAlpha

		Pass {		
		
			

			CGPROGRAM 
			#pragma vertex vert 
			#pragma fragment frag 
			#pragma fragmentoption ARB_precision_hint_fastest

			sampler2D _MainTex;
			sampler2D _GaugeFullTex;
			half _CutOffFinal; 
			half _CutOffPrevious; 
			half _StartOffset;
			half _EndOffset;
			half _PreviousDarkness;
			half4 _PreviousColor;
			half _HGauge; 
			 
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
		        float2 texcoord : TEXCOORD0;
		       
			};
			
			v2f vert( appdata_t v ) { 
			
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(v.vertex);
				
				OUT.color = v.color;
								
				OUT.texcoord = v.texcoord;
				
				return OUT; 
			}

			half4 frag (v2f i) : COLOR {		
				half4 original = tex2D(_MainTex, i.texcoord);
				half4 full = tex2D(_GaugeFullTex, i.texcoord);
				
				half4 res;
				fixed fillTexture = (_EndOffset-_StartOffset)*_CutOffFinal;
				fixed fillTexturePrev = (_EndOffset-_StartOffset)*_CutOffPrevious;
				
				if (_HGauge == 1)
				{
					if (ceil ( (i.texcoord.x) * 1000) > (_StartOffset+fillTexture)*1000)
					{					
						if (ceil ( (i.texcoord.x) * 1000) <= (_StartOffset+fillTexturePrev)*1000)
						{
							res = full;
							res *= _PreviousColor;
						}
						else
						{
							res = original;
						}
					}
					else					
						res = full;					
				}
				else
				{				
					if (ceil ( (i.texcoord.y) * 1000) > (_StartOffset+fillTexture)*1000)					
						res = original;					
					else					
						res = full;
				}
				
				res.rgb *= full.a;

				return res;
			} 
			
			ENDCG 
			
		} // Pass

	}
	
	
	FallBack "Diffuse"

}