Shader "Custom/Collision"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
		_Intensity("Intensity", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
		
		Blend SrcColor One

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        float4 _Color;
		float _Intensity;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            float4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			float2 uv = IN.uv_MainTex;
			float s=0.1;
			float p=3;
			float s2=1;
			float uvc = pow(saturate(abs(uv.x*s-s/2)*2+abs(uv.y*s-s/2)*2),p)*1/s*pow(p,p);
			float e=0.925;
			float edge = saturate(abs(uv.x-0.5)*2-e)*e/1*50*2+saturate(abs(uv.y-0.5)*2-e)*e/1*50*2;
			//c.rgb=lerp(_Color,_Color*uvc,saturate(uvc));
			//c.rgb=edge;
			//c.rgb=lerp(_Color,_Color*edge*_Intensity,saturate(edge));
			//c.rgb=((_Color-saturate(edge))+_Color*edge);
			float ratio=0.7;
			float se=saturate(edge);
			//c.rgb=(saturate(ratio-se)+saturate(se-ratio))*_Color;
			c.rgb=(saturate(ratio-se)+saturate(se))*_Color*_Intensity;
			
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = 0.5; //c.a
        }
        ENDCG
    }
    FallBack "Diffuse"
}
