using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Lighting
{
    const string bufferName = "Lighting";

    const int maxDirLightCount = 4;

    static int dirLightCountId = Shader.PropertyToID("_DirectionLightCount");
    static int dirLightColorId = Shader.PropertyToID("_DirectionLightColor");
    static int dirLightDirectionId = Shader.PropertyToID("_DirectionLightDirection");

    static Vector4[] dirLightColors = new Vector4[maxDirLightCount];
    static Vector4[] dirLightDirections = new Vector4[maxDirLightCount];

    CommandBuffer buffer = new CommandBuffer() { name = bufferName };

    CullingResults cullingResults;

    Shadows shadows = new Shadows();

    public void Setup(ScriptableRenderContext context, CullingResults cullingResults, ShadowSettings shadowSettings)
    {
        this.cullingResults = cullingResults;
        buffer.BeginSample(bufferName);
        shadows.Setup(context, cullingResults, shadowSettings);
        SetupLights();
        shadows.Render();
        buffer.EndSample(bufferName);
        context.ExecuteCommandBuffer(buffer);
        buffer.Clear();
    }

    void SetupLights()
    {
        var visibleLights = cullingResults.visibleLights;
        var lightCount = 0;
        for (int i = 0; i < visibleLights.Length; i++)
        {
            var visibleLight = visibleLights[i];
            SetupDirectionalLight(lightCount++, ref visibleLight);
            if (lightCount >= maxDirLightCount)
            {
                break;
            }
        }

        buffer.SetGlobalInt(dirLightCountId, visibleLights.Length);
        buffer.SetGlobalVectorArray(dirLightColorId, dirLightColors);
        buffer.SetGlobalVectorArray(dirLightDirectionId, dirLightDirections);
    }

    void SetupDirectionalLight(int index, ref VisibleLight visibleLight)
    {
        dirLightColors[index] = visibleLight.finalColor;
        dirLightDirections[index] = -visibleLight.localToWorldMatrix.GetColumn(2);
        shadows.ReserveDirectionalShadows(visibleLight.light, index);
    }

    public void Cleanup()
    {
        shadows.Cleanup();
    }
}