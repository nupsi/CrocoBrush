Shader "CrocoBrush/ColorReplace"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Original ("Original Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _New ("New Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_Range ("Range", Float) = 0.5
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha 

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            fixed4 _Original;
            fixed4 _New;
			half _Range;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                //if(all(col.rgb >= _Original.rgb))
                //    return _New;
				if(col.r <= _Range) 
					if(col.g <= _Range)
						if(col.b > _Original.b - _Range && col.b < _Original.b + _Range)
							return _New;
				return col;
            }
            ENDCG
        }
    }
}
