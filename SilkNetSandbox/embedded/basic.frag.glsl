#version 310 es
precision highp float;

uniform sampler2D iTex;

in vec2 fUv;
in vec4 fColor;

out vec4 oColor;

void main()
{
    oColor = vec4(fUv.x,fUv.y,1,1);
    oColor = mix(texture(iTex, fUv), oColor, .1);
}
