#ifndef CUSTOM_LIGHT_INCLUDED
    #define CUSTOM_LIGHT_INCLUDED
    #define MAX_DIRECTION_LIGHT_COUNT 4

    CBUFFER_START(_CustomLight)
        int _DirectionLightCount;
        float4 _DirectionLightColor[MAX_DIRECTION_LIGHT_COUNT];
        float4 _DirectionLightDirection[MAX_DIRECTION_LIGHT_COUNT];
    CBUFFER_END

    struct Light {
        float3 color;
        float3 direction;
    };

    int GetDirectionLightCount() {
        return _DirectionLightCount;
    }

    Light GetDirectionLight(int index) {
        Light light;
        light.color = _DirectionLightColor[index].rgb;
        light.direction = _DirectionLightDirection[index].xyz;
        return light;
    }

#endif