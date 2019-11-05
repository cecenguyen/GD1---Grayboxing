// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/80Neon"
{
	Properties
	{
		[NoScaleOffset]_GridTexture("Grid Texture", 2D) = "white" {}
		_Scale("Scale", Float) = 1
		_EmissionColor("Emission Color", Color) = (1,1,1,1)
		_NeonStrobe("Neon Strobe", Range( 0 , 1)) = 1
		[NoScaleOffset]_OffsetNoise("Offset Noise", 2D) = "white" {}
		_OffsetNoiseScale("Offset Noise Scale", Float) = 0
		_OffsetHeight("Offset Height", Range( 0 , 1)) = 0
		_OffsetPanSpeed("Offset Pan Speed", Vector) = (0.1,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _OffsetNoise;
		uniform float2 _OffsetPanSpeed;
		uniform float _OffsetNoiseScale;
		uniform float _OffsetHeight;
		uniform float _NeonStrobe;
		uniform sampler2D _GridTexture;
		uniform float _Scale;
		uniform float4 _EmissionColor;


		float3 HSVToRGB( float3 c )
		{
			float4 K = float4( 1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0 );
			float3 p = abs( frac( c.xxx + K.xyz ) * 6.0 - K.www );
			return c.z * lerp( K.xxx, saturate( p - K.xxx ), c.y );
		}


		float3 RGBToHSV(float3 c)
		{
			float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
			float4 p = lerp( float4( c.bg, K.wz ), float4( c.gb, K.xy ), step( c.b, c.g ) );
			float4 q = lerp( float4( p.xyw, c.r ), float4( c.r, p.yzx ), step( p.x, c.r ) );
			float d = q.x - min( q.w, q.y );
			float e = 1.0e-10;
			return float3( abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
		}

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float2 appendResult30 = (float2(_OffsetNoiseScale , _OffsetNoiseScale));
			float2 uv_TexCoord23 = v.texcoord.xy * appendResult30;
			float2 panner22 = ( _Time.y * _OffsetPanSpeed + uv_TexCoord23);
			float3 ase_vertexNormal = v.normal.xyz;
			float3 ase_vertex3Pos = v.vertex.xyz;
			v.vertex.xyz = ( ( ( tex2Dlod( _OffsetNoise, float4( panner22, 0, 0.0) ) * float4( ase_vertexNormal , 0.0 ) ) * _OffsetHeight ) + float4( ase_vertex3Pos , 0.0 ) ).rgb;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 appendResult14 = (float2(_Scale , _Scale));
			float2 uv_TexCoord15 = i.uv_texcoord * appendResult14;
			float3 hsvTorgb6 = RGBToHSV( ( ( 1.0 - tex2D( _GridTexture, uv_TexCoord15 ).r ) * _EmissionColor ).rgb );
			float3 hsvTorgb7 = HSVToRGB( float3(( ( _NeonStrobe * _Time.y ) + hsvTorgb6.x ),hsvTorgb6.y,hsvTorgb6.z) );
			o.Albedo = hsvTorgb7;
			o.Emission = hsvTorgb7;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17101
767;580;972;421;214.648;403.0046;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;13;-1286.241,-239.7031;Inherit;False;Property;_Scale;Scale;1;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-841.1543,226.1249;Inherit;False;Property;_OffsetNoiseScale;Offset Noise Scale;5;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;14;-1118.222,-246.1303;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;15;-950.6468,-267.3392;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;30;-590.1543,217.1249;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;23;-409.1315,196.0827;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;29;-356.2524,313.6877;Inherit;False;Property;_OffsetPanSpeed;Offset Pan Speed;7;0;Create;True;0;0;False;0;0.1,0;0.1,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;17;-346.1363,439.714;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-670.1299,-331.3983;Inherit;True;Property;_GridTexture;Grid Texture;0;1;[NoScaleOffset];Create;True;0;0;False;0;None;f17e501a189e8b547819caea619ece10;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;2;-398.6906,-29.96724;Inherit;False;Property;_EmissionColor;Emission Color;2;0;Create;True;0;0;False;0;1,1,1,1;1,0.06326783,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;5;-339.4503,-305.5936;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;22;-152.1504,295.1028;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.1,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NormalVertexDataNode;20;433.8719,527.7596;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;21;324.714,271.1622;Inherit;True;Property;_OffsetNoise;Offset Noise;4;1;[NoScaleOffset];Create;True;0;0;False;0;None;4a6c8636bb9ad79478ca490064dd4238;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;10;282.1802,-242.3615;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;180.8674,-353.0641;Inherit;False;Property;_NeonStrobe;Neon Strobe;3;0;Create;True;0;0;False;0;1;0.496;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-79.88679,-304.6561;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RGBToHSVNode;6;170.4423,-88.32094;Inherit;True;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;713.0223,325.196;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;27;716.134,436.9769;Inherit;False;Property;_OffsetHeight;Offset Height;6;0;Create;True;0;0;False;0;0;0.222;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;483.38,-300.5615;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;25;969.3834,514.3024;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;9;624.4802,-148.1614;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;1021.08,326.9143;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.HSVToRGBNode;7;822.975,-61.21708;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleAddOpNode;24;1219.682,328.1699;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1378.857,-85.34406;Float;False;True;2;ASEMaterialInspector;0;0;Standard;Custom/80Neon;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Absolute;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;14;0;13;0
WireConnection;14;1;13;0
WireConnection;15;0;14;0
WireConnection;30;0;31;0
WireConnection;30;1;31;0
WireConnection;23;0;30;0
WireConnection;1;1;15;0
WireConnection;5;0;1;1
WireConnection;22;0;23;0
WireConnection;22;2;29;0
WireConnection;22;1;17;0
WireConnection;21;1;22;0
WireConnection;3;0;5;0
WireConnection;3;1;2;0
WireConnection;6;0;3;0
WireConnection;19;0;21;0
WireConnection;19;1;20;0
WireConnection;11;0;12;0
WireConnection;11;1;10;0
WireConnection;9;0;11;0
WireConnection;9;1;6;1
WireConnection;26;0;19;0
WireConnection;26;1;27;0
WireConnection;7;0;9;0
WireConnection;7;1;6;2
WireConnection;7;2;6;3
WireConnection;24;0;26;0
WireConnection;24;1;25;0
WireConnection;0;0;7;0
WireConnection;0;2;7;0
WireConnection;0;11;24;0
ASEEND*/
//CHKSM=2D0933B9FCB993342539F0A5B049506AFE68DDF9