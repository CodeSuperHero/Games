using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Rendering/Custom Render Pipline")]
public class CustomRenderPiplineAsset : RenderPipelineAsset
{
    [SerializeField]
    bool useDynamicBatching = true, useGPUInstancing = true, useSRPBatcher = true;

    [SerializeField]
    ShadowSettings shadowSettings = default;

    protected override RenderPipeline CreatePipeline()
    {
        return new CustomRenderPipline(useDynamicBatching, useGPUInstancing, useSRPBatcher, shadowSettings);
    }
}
