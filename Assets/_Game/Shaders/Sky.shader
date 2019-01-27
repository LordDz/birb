Shader "Custom/Sky"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_LessRain ("LessRain", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Feathers ("Feathers", 2D) = "white" {}

		_Peter ("Peter", float) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
		
		//Cull Off - two sided

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
		sampler2D _Feathers;
		sampler2D _LessRain;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
		float _Peter;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
			float t = _Time.y*0.01;
			float2 uv=IN.uv_MainTex;
			float2 scrolluv = uv+float2(t,0);
			float2 scrolluvFast = uv+float2(t*7,0);

			
            fixed4 d = tex2D (_MainTex, scrolluvFast) * _Color;
			fixed4 l = tex2D (_LessRain, scrolluv) * _Color;
			
			
			fixed4 c = float4(0,0,0,1);
			float2 fuv=uv*float2(2,2);
			float fspeed=125.0;
			float f1 = tex2D (_Feathers, fuv+float2(t*-0.5,t*fspeed*1)).r;
			float f2 = tex2D (_Feathers, fuv+float2(t*1.4,t*fspeed*1.2)).g;
			float f3 = tex2D (_Feathers, fuv+float2(t*-2,t*fspeed*1.4)).b;
			float f4 = tex2D (_Feathers, fuv+float2(t,t*fspeed*1.6)).a;
			
			float feathers=saturate(f1+f2+f3+f4);
			c.rgb = lerp(l+float3(0.2,0.2,0.4),d-feathers,_Peter);
            o.Albedo = c.rgb;
            //o.Albedo = f1;
            //Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = d.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
