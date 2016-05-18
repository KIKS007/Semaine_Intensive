Shader "Transparent/Lightmapped/VertexLit" {
Properties {
    _Color ("Main Color", Color) = (1,1,1,1)
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _LightMap ("Lightmap (RGB)", 2D) = "black" {}
    _FallbackColor ("Fallback Color", Color) = (1,1,1,1)
}
 
// ------------------------------------------------------------------
// Three texture cards (Radeons, GeForce3/4Ti and up)
 
Category {
    ZWrite Off
    Alphatest Greater 0
    Tags { "Queue" = "Transparent" }
    Blend SrcAlpha OneMinusSrcAlpha
    ColorMask RGB
 
SubShader {
    Fog { Color [_AddFog] }
 
    Pass {
        Name "BASE"
        Material {
            Diffuse [_Color]
        }
        Lighting On
 
        BindChannels {
            Bind "Vertex", vertex
            Bind "normal", normal
            Bind "texcoord1", texcoord0 // lightmap uses 2nd uv
            Bind "texcoord1", texcoord1 // lightmap uses 2nd uv
            Bind "texcoord", texcoord2 // main uses 1st uv
        }
       
        SetTexture [_LightMap] {
            constantColor [_Color]
            combine texture * constant
        }
        SetTexture [_LightMap] {
            constantColor (0.5,0.5,0.5,0.5)
            combine previous * constant + primary
        }
        SetTexture [_MainTex] {
            combine texture * previous DOUBLE, texture * primary
        }
    }
}
 
// ------------------------------------------------------------------
// Dual texture cards - no lighting
 
SubShader {
    Fog { Color [_AddFog] }
 
    // Always drawn base pass: texture * lightmap
    Pass {
        Name "BASE"
        Tags {"LightMode" = "Always"}
        Color [_PPLAmbient]
        BindChannels {
            Bind "Vertex", vertex
            Bind "normal", normal
            Bind "texcoord1", texcoord0 // lightmap uses 2nd uv
            Bind "texcoord", texcoord1 // main uses 1st uv
        }
        SetTexture [_LightMap] {
            constantColor [_Color]
            combine texture * constant
        }
        SetTexture [_MainTex] {
            combine texture * previous, texture * primary
        }
    }  
}
 
// ------------------------------------------------------------------
// Single texture cards - no lightmap
 
SubShader {
    Fog { Color [_AddFog] }
 
    // Always drawn base pass: texture * lightmap
    Pass {
        Name "BASE"
        Tags {"LightMode" = "Always"}
        Color [_PPLAmbient]
 
        SetTexture [_MainTex] {
            constantColor[_FallbackColor]
            combine texture * constant, texture * constant
        }
    }  
}
 
}
 
Fallback off
 
}
 