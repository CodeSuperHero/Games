using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class CustomRenderPipline : RenderPipeline
{
    private CameraRenderer renderer = new CameraRenderer();

    bool useDynamicBatching, useGPUInstancing;

    ShadowSettings shadowSettings;

    public CustomRenderPipline(bool useDynamicBatching, bool useGPUInstancing, bool useSRPBatcher, ShadowSettings shadowSettings)
    {
        this.useDynamicBatching = useDynamicBatching;
        this.useGPUInstancing = useGPUInstancing;
        GraphicsSettings.useScriptableRenderPipelineBatching = useSRPBatcher;
        GraphicsSettings.lightsUseLinearIntensity = true;
        this.shadowSettings = shadowSettings;
    }

    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
        foreach (var cam in cameras)
        {
            renderer.Render(context, cam, useDynamicBatching, useGPUInstancing, shadowSettings);
        }
    }
}
