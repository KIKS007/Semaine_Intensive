// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:33794,y:32664,varname:node_4013,prsc:2|emission-1348-OUT,voffset-2515-OUT,tess-567-OUT;n:type:ShaderForge.SFN_Vector3,id:3813,x:33302,y:32483,varname:node_3813,prsc:2,v1:1,v2:0.9724138,v3:0;n:type:ShaderForge.SFN_Slider,id:567,x:33330,y:33574,ptovrint:False,ptlb:node_567,ptin:_node_567,varname:node_567,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:50,max:100;n:type:ShaderForge.SFN_Sin,id:1488,x:32387,y:33341,varname:node_1488,prsc:2|IN-2277-OUT;n:type:ShaderForge.SFN_Multiply,id:2277,x:32212,y:33341,varname:node_2277,prsc:2|A-1240-OUT,B-208-T;n:type:ShaderForge.SFN_Time,id:208,x:31972,y:33375,varname:node_208,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:1240,x:31937,y:33218,ptovrint:False,ptlb:node_1240,ptin:_node_1240,varname:node_1240,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:5;n:type:ShaderForge.SFN_RemapRange,id:3870,x:32548,y:33324,varname:node_3870,prsc:2,frmn:0,frmx:1,tomn:0,tomx:1|IN-1488-OUT;n:type:ShaderForge.SFN_NormalVector,id:3445,x:32929,y:33490,prsc:2,pt:False;n:type:ShaderForge.SFN_Multiply,id:2515,x:33294,y:33365,varname:node_2515,prsc:2|A-2712-OUT,B-3445-OUT;n:type:ShaderForge.SFN_Add,id:2712,x:33055,y:33185,varname:node_2712,prsc:2|A-9374-OUT,B-295-OUT;n:type:ShaderForge.SFN_Multiply,id:9374,x:32718,y:33280,varname:node_9374,prsc:2|A-5217-OUT,B-3870-OUT;n:type:ShaderForge.SFN_Vector1,id:5217,x:32538,y:33236,varname:node_5217,prsc:2,v1:0.02;n:type:ShaderForge.SFN_Tex2dAsset,id:2291,x:32298,y:32745,ptovrint:False,ptlb:node_2291,ptin:_node_2291,varname:node_2291,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ce798518eb11e4a42a627ecf4d0d8f38,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:5501,x:32829,y:32519,varname:node_5501,prsc:2,tex:ce798518eb11e4a42a627ecf4d0d8f38,ntxv:0,isnm:False|UVIN-9547-UVOUT,TEX-2291-TEX;n:type:ShaderForge.SFN_TexCoord,id:7038,x:32000,y:32382,varname:node_7038,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:9547,x:32549,y:32458,varname:node_9547,prsc:2,spu:0.1,spv:0.1|UVIN-7038-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:671,x:32716,y:32766,varname:node_671,prsc:2,tex:ce798518eb11e4a42a627ecf4d0d8f38,ntxv:0,isnm:False|UVIN-1470-UVOUT,TEX-2291-TEX;n:type:ShaderForge.SFN_Panner,id:1470,x:32563,y:32284,varname:node_1470,prsc:2,spu:-0.1,spv:0|UVIN-7038-UVOUT;n:type:ShaderForge.SFN_Multiply,id:295,x:33198,y:32831,varname:node_295,prsc:2|A-5501-RGB,B-671-RGB,C-8344-OUT;n:type:ShaderForge.SFN_Vector1,id:8344,x:32643,y:32996,varname:node_8344,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Fresnel,id:340,x:33130,y:32383,varname:node_340,prsc:2;n:type:ShaderForge.SFN_Multiply,id:2987,x:33571,y:32407,varname:node_2987,prsc:2|A-340-OUT,B-3813-OUT;n:type:ShaderForge.SFN_Tex2d,id:642,x:32416,y:32098,ptovrint:False,ptlb:node_642,ptin:_node_642,varname:node_642,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:25956dc05f719544c8950646d98c0283,ntxv:0,isnm:False|UVIN-9096-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:7506,x:31660,y:32103,varname:node_7506,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:9096,x:32181,y:32084,varname:node_9096,prsc:2,spu:1,spv:0|UVIN-983-OUT;n:type:ShaderForge.SFN_Add,id:1348,x:33534,y:32642,varname:node_1348,prsc:2|A-2987-OUT,B-642-RGB;n:type:ShaderForge.SFN_Multiply,id:983,x:31882,y:32117,varname:node_983,prsc:2|A-7506-UVOUT,B-5395-OUT;n:type:ShaderForge.SFN_Slider,id:5395,x:31530,y:32303,ptovrint:False,ptlb:node_5395,ptin:_node_5395,varname:node_5395,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:4,max:10;proporder:567-1240-2291-642-5395;pass:END;sub:END;*/

Shader "Shader Forge/balle" {
    Properties {
        _node_567 ("node_567", Range(0, 100)) = 50
        _node_1240 ("node_1240", Float ) = 5
        _node_2291 ("node_2291", 2D) = "white" {}
        _node_642 ("node_642", 2D) = "white" {}
        _node_5395 ("node_5395", Range(0, 10)) = 4
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "Tessellation.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 5.0
            #pragma glsl
            uniform float4 _TimeEditor;
            uniform float _node_567;
            uniform float _node_1240;
            uniform sampler2D _node_2291; uniform float4 _node_2291_ST;
            uniform sampler2D _node_642; uniform float4 _node_642_ST;
            uniform float _node_5395;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_208 = _Time + _TimeEditor;
                float4 node_6062 = _Time + _TimeEditor;
                float2 node_9547 = (o.uv0+node_6062.g*float2(0.1,0.1));
                float4 node_5501 = tex2Dlod(_node_2291,float4(TRANSFORM_TEX(node_9547, _node_2291),0.0,0));
                float2 node_1470 = (o.uv0+node_6062.g*float2(-0.1,0));
                float4 node_671 = tex2Dlod(_node_2291,float4(TRANSFORM_TEX(node_1470, _node_2291),0.0,0));
                v.vertex.xyz += (((0.02*(sin((_node_1240*node_208.g))*1.0+0.0))+(node_5501.rgb*node_671.rgb*0.2))*v.normal);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            #ifdef UNITY_CAN_COMPILE_TESSELLATION
                struct TessVertex {
                    float4 vertex : INTERNALTESSPOS;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                    float2 texcoord0 : TEXCOORD0;
                };
                struct OutputPatchConstant {
                    float edge[3]         : SV_TessFactor;
                    float inside          : SV_InsideTessFactor;
                    float3 vTangent[4]    : TANGENT;
                    float2 vUV[4]         : TEXCOORD;
                    float3 vTanUCorner[4] : TANUCORNER;
                    float3 vTanVCorner[4] : TANVCORNER;
                    float4 vCWts          : TANWEIGHTS;
                };
                TessVertex tessvert (VertexInput v) {
                    TessVertex o;
                    o.vertex = v.vertex;
                    o.normal = v.normal;
                    o.tangent = v.tangent;
                    o.texcoord0 = v.texcoord0;
                    return o;
                }
                float Tessellation(TessVertex v){
                    return _node_567;
                }
                float4 Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
                    float tv = Tessellation(v);
                    float tv1 = Tessellation(v1);
                    float tv2 = Tessellation(v2);
                    return float4( tv1+tv2, tv2+tv, tv+tv1, tv+tv1+tv2 ) / float4(2,2,2,3);
                }
                OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
                    OutputPatchConstant o = (OutputPatchConstant)0;
                    float4 ts = Tessellation( v[0], v[1], v[2] );
                    o.edge[0] = ts.x;
                    o.edge[1] = ts.y;
                    o.edge[2] = ts.z;
                    o.inside = ts.w;
                    return o;
                }
                [domain("tri")]
                [partitioning("fractional_odd")]
                [outputtopology("triangle_cw")]
                [patchconstantfunc("hullconst")]
                [outputcontrolpoints(3)]
                TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
                    return v[id];
                }
                [domain("tri")]
                VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
                    VertexInput v = (VertexInput)0;
                    v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
                    v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
                    v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
                    v.texcoord0 = vi[0].texcoord0*bary.x + vi[1].texcoord0*bary.y + vi[2].texcoord0*bary.z;
                    VertexOutput o = vert(v);
                    return o;
                }
            #endif
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 node_6062 = _Time + _TimeEditor;
                float2 node_9096 = ((i.uv0*_node_5395)+node_6062.g*float2(1,0));
                float4 _node_642_var = tex2D(_node_642,TRANSFORM_TEX(node_9096, _node_642));
                float3 emissive = (((1.0-max(0,dot(normalDirection, viewDirection)))*float3(1,0.9724138,0))+_node_642_var.rgb);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            
            CGPROGRAM
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "Tessellation.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 5.0
            #pragma glsl
            uniform float4 _TimeEditor;
            uniform float _node_567;
            uniform float _node_1240;
            uniform sampler2D _node_2291; uniform float4 _node_2291_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_208 = _Time + _TimeEditor;
                float4 node_9530 = _Time + _TimeEditor;
                float2 node_9547 = (o.uv0+node_9530.g*float2(0.1,0.1));
                float4 node_5501 = tex2Dlod(_node_2291,float4(TRANSFORM_TEX(node_9547, _node_2291),0.0,0));
                float2 node_1470 = (o.uv0+node_9530.g*float2(-0.1,0));
                float4 node_671 = tex2Dlod(_node_2291,float4(TRANSFORM_TEX(node_1470, _node_2291),0.0,0));
                v.vertex.xyz += (((0.02*(sin((_node_1240*node_208.g))*1.0+0.0))+(node_5501.rgb*node_671.rgb*0.2))*v.normal);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            #ifdef UNITY_CAN_COMPILE_TESSELLATION
                struct TessVertex {
                    float4 vertex : INTERNALTESSPOS;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                    float2 texcoord0 : TEXCOORD0;
                };
                struct OutputPatchConstant {
                    float edge[3]         : SV_TessFactor;
                    float inside          : SV_InsideTessFactor;
                    float3 vTangent[4]    : TANGENT;
                    float2 vUV[4]         : TEXCOORD;
                    float3 vTanUCorner[4] : TANUCORNER;
                    float3 vTanVCorner[4] : TANVCORNER;
                    float4 vCWts          : TANWEIGHTS;
                };
                TessVertex tessvert (VertexInput v) {
                    TessVertex o;
                    o.vertex = v.vertex;
                    o.normal = v.normal;
                    o.tangent = v.tangent;
                    o.texcoord0 = v.texcoord0;
                    return o;
                }
                float Tessellation(TessVertex v){
                    return _node_567;
                }
                float4 Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
                    float tv = Tessellation(v);
                    float tv1 = Tessellation(v1);
                    float tv2 = Tessellation(v2);
                    return float4( tv1+tv2, tv2+tv, tv+tv1, tv+tv1+tv2 ) / float4(2,2,2,3);
                }
                OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
                    OutputPatchConstant o = (OutputPatchConstant)0;
                    float4 ts = Tessellation( v[0], v[1], v[2] );
                    o.edge[0] = ts.x;
                    o.edge[1] = ts.y;
                    o.edge[2] = ts.z;
                    o.inside = ts.w;
                    return o;
                }
                [domain("tri")]
                [partitioning("fractional_odd")]
                [outputtopology("triangle_cw")]
                [patchconstantfunc("hullconst")]
                [outputcontrolpoints(3)]
                TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
                    return v[id];
                }
                [domain("tri")]
                VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
                    VertexInput v = (VertexInput)0;
                    v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
                    v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
                    v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
                    v.texcoord0 = vi[0].texcoord0*bary.x + vi[1].texcoord0*bary.y + vi[2].texcoord0*bary.z;
                    VertexOutput o = vert(v);
                    return o;
                }
            #endif
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
