#version 310 es
precision highp float;

uniform sampler2D iTex;

in vec2 fUv;
in vec4 fColor;

out vec4 oColor;

void main()
{
    oColor = texture(iTex, fUv);
}
