using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Lighting
{
    const string bufferName = "Lighting";

    const int maxDirLightCount = 4;

    static int dirLightCountId = Shader.PropertyToID("_DirectionLightDirection");
    static int dirLightColorId = Shader.PropertyToID("_DirectionLightColor");
    static int dirLightDirectionId = Shader.PropertyToID("_DirectionLightDirection");

    static Vector4[] dirLightColors = new Vector4[maxDirLightCount];
    static Vector4[] dirLightDirections = new Vector4[maxDirLightCount];

    CommandBuffer buffer = new CommandBuffer() { name = bufferName };

    CullingResults cullingResults;


    public void Setup(ScriptableRenderContext context, CullingResults cullingResults)
    {
        this.cullingResults = cullingResults;
        buffer.BeginSample(bufferName);
        SetupLights();
        buffer.EndSample(bufferName);
        context.ExecuteCommandBuffer(buffer);
        buffer.Clear();
    }

    void SetupLights()
    {
        var visibleLights = cullingResults.visibleLights;

    }

    void SetupDirectionLight(int index, VisibleLight visibleLight)
    {
        dirLightColors[index] = visibleLight.finalColor;
        dirLightDirections[index] = -visibleLight.localToWorldMatrix.GetColumn(2);
    }
}