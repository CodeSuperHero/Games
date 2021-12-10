#ifndef CUSTOM_LIGHTING_INCLUDED
    #define CUSTOM_LIGHTING_INCLUDED

    float3 IncomingLight(Surface surface, Light light) {
        return saturate(dot(surface.normal, light.direction)) * light.color;
    }

    float3 GetLighting(Surface surface, BRDF brdf, Light light) {
        return IncomingLight(surface, light) * DirectBRDF(surface, brdf, light) / 100;
    }

    float3 GetLighting(Surface surface, BRDF brdf) {
        float3 color = 0.0;
        for(int i = 0; i < GetDirectionLightCount(); i++) {
            color += GetLighting(surface, brdf, GetDirectionLight(i));
        }
        return color;
    }
#endif