// Shader created with Shader Forge v1.25 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.25;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:33006,y:32736,varname:node_4013,prsc:2|emission-853-OUT,alpha-6667-OUT;n:type:ShaderForge.SFN_TexCoord,id:4305,x:31294,y:31659,varname:node_4305,prsc:2,uv:0;n:type:ShaderForge.SFN_ValueProperty,id:1728,x:31173,y:31885,ptovrint:False,ptlb:node_1728,ptin:_node_1728,varname:node_1728,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_Multiply,id:7753,x:31534,y:31689,varname:node_7753,prsc:2|A-4305-UVOUT,B-1728-OUT;n:type:ShaderForge.SFN_Panner,id:265,x:31742,y:31689,varname:node_265,prsc:2,spu:-0.2,spv:-0.2|UVIN-7753-OUT;n:type:ShaderForge.SFN_Tex2d,id:7356,x:31923,y:31689,ptovrint:False,ptlb:node_7356,ptin:_node_7356,varname:node_7356,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:352b587d370152542860712673796b0c,ntxv:0,isnm:False|UVIN-265-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:4602,x:31255,y:32232,varname:node_4602,prsc:2,uv:0;n:type:ShaderForge.SFN_ValueProperty,id:6401,x:31166,y:32457,ptovrint:False,ptlb:node_1728_copy,ptin:_node_1728_copy,varname:_node_1728_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_Multiply,id:9604,x:31495,y:32262,varname:node_9604,prsc:2|A-4602-UVOUT,B-6401-OUT;n:type:ShaderForge.SFN_Panner,id:8146,x:31703,y:32262,varname:node_8146,prsc:2,spu:0.3,spv:0.3|UVIN-9604-OUT;n:type:ShaderForge.SFN_Tex2d,id:9065,x:31884,y:32262,ptovrint:False,ptlb:node_7356_copy,ptin:_node_7356_copy,varname:_node_7356_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:5eb2aa4c2260b334f90bf47ba20606ba,ntxv:0,isnm:False|UVIN-8146-UVOUT;n:type:ShaderForge.SFN_Multiply,id:1100,x:32244,y:32169,varname:node_1100,prsc:2|A-7356-RGB,B-9065-RGB,C-3736-R;n:type:ShaderForge.SFN_ComponentMask,id:5493,x:32387,y:32520,varname:node_5493,prsc:2,cc1:2,cc2:-1,cc3:-1,cc4:-1|IN-1100-OUT;n:type:ShaderForge.SFN_Vector3,id:3698,x:32013,y:32464,varname:node_3698,prsc:2,v1:0,v2:0.462069,v3:1;n:type:ShaderForge.SFN_Add,id:853,x:32499,y:32201,varname:node_853,prsc:2|A-1100-OUT,B-3698-OUT;n:type:ShaderForge.SFN_Tex2d,id:3736,x:31557,y:31950,ptovrint:False,ptlb:node_3736,ptin:_node_3736,varname:node_3736,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b68013fec0d3c8b468b050ee9b21a21b,ntxv:0,isnm:False;n:type:ShaderForge.SFN_FragmentPosition,id:9897,x:31910,y:32811,varname:node_9897,prsc:2;n:type:ShaderForge.SFN_Distance,id:821,x:32241,y:32779,varname:node_821,prsc:2|A-7675-OUT,B-9897-XYZ;n:type:ShaderForge.SFN_OneMinus,id:440,x:32508,y:32951,varname:node_440,prsc:2|IN-821-OUT;n:type:ShaderForge.SFN_Multiply,id:6667,x:32717,y:32920,varname:node_6667,prsc:2|A-5493-OUT,B-440-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8706,x:31844,y:32557,ptovrint:False,ptlb:x,ptin:_x,varname:node_8706,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:303,x:31844,y:32633,ptovrint:False,ptlb:y,ptin:_y,varname:node_303,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:163,x:31844,y:32713,ptovrint:False,ptlb:z,ptin:_z,varname:node_163,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Append,id:7675,x:32038,y:32633,varname:node_7675,prsc:2|A-8706-OUT,B-303-OUT,C-163-OUT;proporder:1728-7356-6401-9065-3736-8706-303-163;pass:END;sub:END;*/

Shader "Shader Forge/champ de force" {
    Properties {
        _node_1728 ("node_1728", Float ) = 3
        _node_7356 ("node_7356", 2D) = "white" {}
        _node_1728_copy ("node_1728_copy", Float ) = 3
        _node_7356_copy ("node_7356_copy", 2D) = "white" {}
        _node_3736 ("node_3736", 2D) = "white" {}
        _x ("x", Float ) = 0
        _y ("y", Float ) = 1
        _z ("z", Float ) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
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
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float _node_1728;
            uniform sampler2D _node_7356; uniform float4 _node_7356_ST;
            uniform float _node_1728_copy;
            uniform sampler2D _node_7356_copy; uniform float4 _node_7356_copy_ST;
            uniform sampler2D _node_3736; uniform float4 _node_3736_ST;
            uniform float _x;
            uniform float _y;
            uniform float _z;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_9345 = _Time + _TimeEditor;
                float2 node_265 = ((i.uv0*_node_1728)+node_9345.g*float2(-0.2,-0.2));
                float4 _node_7356_var = tex2D(_node_7356,TRANSFORM_TEX(node_265, _node_7356));
                float2 node_8146 = ((i.uv0*_node_1728_copy)+node_9345.g*float2(0.3,0.3));
                float4 _node_7356_copy_var = tex2D(_node_7356_copy,TRANSFORM_TEX(node_8146, _node_7356_copy));
                float4 _node_3736_var = tex2D(_node_3736,TRANSFORM_TEX(i.uv0, _node_3736));
                float3 node_1100 = (_node_7356_var.rgb*_node_7356_copy_var.rgb*_node_3736_var.r);
                float3 emissive = (node_1100+float3(0,0.462069,1));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,(node_1100.b*(1.0 - distance(float3(_x,_y,_z),i.posWorld.rgb))));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
