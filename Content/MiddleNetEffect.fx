#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;

sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float4 color = tex2D(SpriteTextureSampler, input.TextureCoordinates) * input.Color;

	float y = input.TextureCoordinates.y;

	if ((y >= 0.10 && y < 0.225) || (y >= 0.325 && y < 0.45) || (y >= 0.55 && y < 0.675) || (y >= 0.775 && y < 0.90))
	{
		color.rgba = 0.0;
	}

	return color;
}

technique SpriteDrawing{
	pass P0{
		PixelShader = compile PS_SHADERMODEL MainPS();
}
}
;