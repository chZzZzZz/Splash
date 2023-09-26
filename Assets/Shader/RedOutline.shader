
Shader "Custom/RedOutline" {
    Properties {
        _OutlineColor ("Outline Color", Color) = (1, 0, 0, 1)
        _OutlineWidth ("Outline Width", Range(0.001, 0.1)) = 0.01
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            uniform float4 _OutlineColor;
            uniform float _OutlineWidth;

            struct appdata {
                float4 vertex : POSITION;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = v.vertex;
                return o;
            }

            float4 frag (v2f i) : SV_Target {
                float4 col = i.color;
                float4 outlineCol = _OutlineColor;

                // ¼ÆËã±ßÔµ¿í¶È
                float outlineWidth = length(ddx(i.vertex) + ddy(i.vertex));
                float alpha = smoothstep(outlineWidth - _OutlineWidth, outlineWidth, 1 - _OutlineWidth);

                // ÔÚ±ßÔµÇøÓò»æÖÆºìÉ«±ß¿ò
                col = lerp(col, outlineCol, alpha);

                return col;
            }
            ENDCG
        }
    }
}
