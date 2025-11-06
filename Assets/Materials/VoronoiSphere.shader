Shader "Custom/NewSurfaceShader"
{
	Properties {
		_MainTex ("Foo", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0
		
		sampler2D _MainTex;
		struct Input {
			float2 uv_MainTex;
		};
		UNITY_INSTANCING_BUFFER_START(Props)
		UNITY_INSTANCING_BUFFER_END(Props)
		
#define hlsl_atan(x,y) atan2(x, y)
#define mod(x,y) ((x)-(y)*floor((x)/(y)))
inline float4 textureLod(sampler2D tex, float2 uv, float lod) {
    return tex2D(tex, uv);
}
// int vector funtions
inline int2 toint2(int x) {
    return int2(x, x);
}
inline int2 toint2(int x, int y) {
    return int2(x, y);
}
inline int3 toint3(int x) {
    return int3(x, x, x);
}
inline int3 toint3(int x, int y, int z) {
    return int3(x, y, z);
}
inline int3 toint3(int2 xy, int z) {
    return int3(xy.x, xy.y, z);
}
inline int3 toint3(int x, int2 yz) {
    return int3(x, yz.x, yz.y);
}
inline int4 toint4(int x, int y, int z, int w) {
    return int4(x, y, z, w);
}
inline int4 toint4(int x) {
    return int4(x, x, x, x);
}
inline int4 toint4(int x, int3 yzw) {
    return int4(x, yzw.x, yzw.y, yzw.z);
}
inline int4 toint4(int2 xy, int2 zw) {
    return int4(xy.x, xy.y, zw.x, zw.y);
}
inline int4 toint4(int3 xyz, int w) {
    return int4(xyz.x, xyz.y, xyz.z, w);
}
inline int4 toint4(int2 xy, int z, int w) {
    return int4(xy.x, xy.y, z, w);
}
// float vector funtions
inline float2 tofloat2(float x) {
    return float2(x, x);
}
inline float2 tofloat2(float x, float y) {
    return float2(x, y);
}
inline float3 tofloat3(float x) {
    return float3(x, x, x);
}
inline float3 tofloat3(float x, float y, float z) {
    return float3(x, y, z);
}
inline float3 tofloat3(float2 xy, float z) {
    return float3(xy.x, xy.y, z);
}
inline float3 tofloat3(float x, float2 yz) {
    return float3(x, yz.x, yz.y);
}
inline float4 tofloat4(float x, float y, float z, float w) {
    return float4(x, y, z, w);
}
inline float4 tofloat4(float x) {
    return float4(x, x, x, x);
}
inline float4 tofloat4(float x, float3 yzw) {
    return float4(x, yzw.x, yzw.y, yzw.z);
}
inline float4 tofloat4(float2 xy, float2 zw) {
    return float4(xy.x, xy.y, zw.x, zw.y);
}
inline float4 tofloat4(float3 xyz, float w) {
    return float4(xyz.x, xyz.y, xyz.z, w);
}
inline float4 tofloat4(float2 xy, float z, float w) {
    return float4(xy.x, xy.y, z, w);
}
inline float2x2 tofloat2x2(float2 v1, float2 v2) {
    return float2x2(v1.x, v1.y, v2.x, v2.y);
}
// EngineSpecificDefinitions
float dot2(float2 x) {
	return dot(x, x);
}
float rand(float2 x) {
    return frac(cos(mod(dot(x, tofloat2(13.9898, 8.141)), 3.14)) * 43758.5);
}
float2 rand2(float2 x) {
    return frac(cos(mod(tofloat2(dot(x, tofloat2(13.9898, 8.141)),
						      dot(x, tofloat2(3.4562, 17.398))), tofloat2(3.14))) * 43758.5);
}
float3 rand3(float2 x) {
    return frac(cos(mod(tofloat3(dot(x, tofloat2(13.9898, 8.141)),
							  dot(x, tofloat2(3.4562, 17.398)),
                              dot(x, tofloat2(13.254, 5.867))), tofloat3(3.14))) * 43758.5);
}
float param_rnd(float minimum, float maximum, float seed) {
	return minimum+(maximum-minimum)*rand(tofloat2(seed));
}
float param_rndi(float minimum, float maximum, float seed) {
	return floor(param_rnd(minimum, maximum + 1.0, seed));
}
float3 rgb2hsv(float3 c) {
	float4 K = tofloat4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
	float4 p = c.g < c.b ? tofloat4(c.bg, K.wz) : tofloat4(c.gb, K.xy);
	float4 q = c.r < p.x ? tofloat4(p.xyw, c.r) : tofloat4(c.r, p.yzx);
	float d = q.x - min(q.w, q.y);
	float e = 1.0e-10;
	return tofloat3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
}
float3 hsv2rgb(float3 c) {
	float4 K = tofloat4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
	float3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
	return c.z * lerp(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
}
static const float o35058526226139_speed = 1.000000000;
static const float4 p_o16223349802338_albedo_color = tofloat4(1.000000000, 1.000000000, 1.000000000, 1.000000000);
static const float p_o16223349802338_metallic = 0.000000000;
static const float p_o16223349802338_roughness = 1.000000000;
static const float p_o16223349802338_emission_energy = 1.000000000;
static const float p_o16223349802338_normal = 1.000000000;
static const float p_o16223349802338_ao = 1.000000000;
static const float p_o16223349802338_depth_scale = 0.500000000;
static const float p_o41721798858222_gradient_pos[3] = {  0.000000000, 0.408843994, 0.899302006  };
static const float4 p_o41721798858222_gradient_col[3] = {  tofloat4(0.480468750, 0.987823486, 1.000000000, 1.000000000), tofloat4(0.614381075, 0.433135986, 0.972656250, 1.000000000), tofloat4(0.307068110, 0.163421631, 0.464843750, 1.000000000)  };
static const float p_o27186706814962_amount = 1.000000000;
static const float p_o27186706814962_center = 1.000000000;
static const float seed_o16706097405391 = 0.000000000;
static const float p_o16706097405391_scale_x = 4.000000000;
static const float p_o16706097405391_scale_y = 4.000000000;
static const float p_o16706097405391_stretch_x = 1.000000000;
static const float p_o16706097405391_stretch_y = 0.670000000;
static const float p_o16706097405391_intensity = 1.000000000;
static const float p_o16706097405391_randomness = 1.000000000;
// #globals: colorize (o41721798858222)
// #globals: voronoi2 (o16706097405391)
// Based on https://www.shadertoy.com/view/ldl3W8
// The MIT License
// Copyright © 2013 Inigo Quilez
float3 iq_voronoi(float2 x, float2 size, float2 stretch, float randomness, float2 seed) {
	float2 n = floor(x);
	float2 f = frac(x);
	float2 mg, mr, mc;
	float md = 8.0;
	for (int j=-1; j<=1; j++)
	for (int i=-1; i<=1; i++) {
		float2 g = tofloat2(float(i),float(j));
		float2 o = randomness*rand2(seed + mod(n + g + size, size));
		float2 c = g + o;
		float2 r = c - f;
		float2 rr = r*stretch;
		float d = dot(rr,rr);
		if (d<md) {
			mc = c;
			md = d;
			mr = r;
			mg = g;
		}
	}
	md = 8.0;
	for (int j=-2; j<=2; j++)
	for (int i=-2; i<=2; i++) {
		float2 g = mg + tofloat2(float(i),float(j));
		float2 o = randomness*rand2(seed + mod(n + g + size, size));
		float2 r = g + o - f;
		float2 rr = (mr-r)*stretch;
		if (dot(rr,rr)>0.00001)
	   		md = min(md, dot(0.5*(mr+r)*stretch, normalize((r-mr)*stretch)));
	}
	return tofloat3(md, mc+n);
}
float4 voronoi(float2 uv, float2 size, float2 stretch, float intensity, float randomness, float seed) {
	uv *= size;
	float3 v = iq_voronoi(uv, size, stretch, randomness, rand2(tofloat2(seed, 1.0-seed)));
	return tofloat4(v.yz, intensity*length((uv-v.yz)*stretch), v.x);
}
float4 o41721798858222_gradient_gradient_fct(float x) {
  if (x < p_o41721798858222_gradient_pos[0]) {
    return p_o41721798858222_gradient_col[0];
  } else if (x < p_o41721798858222_gradient_pos[1]) {
    return lerp(lerp(p_o41721798858222_gradient_col[1], p_o41721798858222_gradient_col[2], (x-p_o41721798858222_gradient_pos[1])/(p_o41721798858222_gradient_pos[2]-p_o41721798858222_gradient_pos[1])), lerp(p_o41721798858222_gradient_col[0], p_o41721798858222_gradient_col[1], (x-p_o41721798858222_gradient_pos[0])/(p_o41721798858222_gradient_pos[1]-p_o41721798858222_gradient_pos[0])), 1.0-0.5*(x-p_o41721798858222_gradient_pos[0])/(p_o41721798858222_gradient_pos[1]-p_o41721798858222_gradient_pos[0]));
  } else if (x < p_o41721798858222_gradient_pos[2]) {
    return lerp(lerp(p_o41721798858222_gradient_col[0], p_o41721798858222_gradient_col[1], (x-p_o41721798858222_gradient_pos[0])/(p_o41721798858222_gradient_pos[1]-p_o41721798858222_gradient_pos[0])), lerp(p_o41721798858222_gradient_col[1], p_o41721798858222_gradient_col[2], (x-p_o41721798858222_gradient_pos[1])/(p_o41721798858222_gradient_pos[2]-p_o41721798858222_gradient_pos[1])), 0.5+0.5*(x-p_o41721798858222_gradient_pos[1])/(p_o41721798858222_gradient_pos[2]-p_o41721798858222_gradient_pos[1]));
  }
  return p_o41721798858222_gradient_col[2];
}
		
		void surf (Input IN, inout SurfaceOutputStandard o) {
	  		float _seed_variation_ = 0.0;
			float2 uv = IN.uv_MainTex;

// #code: voronoi2 (o16706097405391)
float4 o16706097405391_0_xyzw = voronoi((((uv)-tofloat2((_Time.y*o35058526226139_speed), (_Time.y*0.5*o35058526226139_speed)))+p_o27186706814962_amount*(((uv)-tofloat2((_Time.y*o35058526226139_speed), (_Time.y*0.5*o35058526226139_speed))).yx-tofloat2(p_o27186706814962_center))*tofloat2(1.0, 0.0)), tofloat2(p_o16706097405391_scale_x, p_o16706097405391_scale_y), tofloat2(p_o16706097405391_stretch_y, p_o16706097405391_stretch_x), p_o16706097405391_intensity, p_o16706097405391_randomness, (seed_o16706097405391+frac(_seed_variation_)));
// #output0: voronoi2 (o16706097405391)
float o16706097405391_0_1_f = o16706097405391_0_xyzw.z;

// #output0: shear (o27186706814962)
float4 o27186706814962_0_1_rgba = tofloat4(tofloat3(o16706097405391_0_1_f), 1.0);

// #output0: translate (o38143520703889)
float4 o38143520703889_0_1_rgba = o27186706814962_0_1_rgba;

// #output0: colorize (o41721798858222)
float4 o41721798858222_0_1_rgba = o41721798858222_gradient_gradient_fct((dot((o38143520703889_0_1_rgba).rgb, tofloat3(1.0))/3.0));

			o.Albedo = ((o41721798858222_0_1_rgba).rgb).rgb*p_o16223349802338_albedo_color.rgb;
			o.Metallic = 1.0*p_o16223349802338_metallic;
			o.Smoothness = 1.0-1.0*p_o16223349802338_roughness;
			o.Alpha = 1.0;
			o.Normal = tofloat3(0.5)*tofloat3(-1.0, 1.0, -1.0)+tofloat3(1.0, 0.0, 1.0);
		}
		ENDCG
	}
	FallBack "Diffuse"
}



