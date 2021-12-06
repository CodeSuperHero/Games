using UnityEngine;

public class MeshBall : MonoBehaviour
{
    static int baseColorId = Shader.PropertyToID("_BaseColor");

    static int cutoffId = Shader.PropertyToID("_Cutoff");

    [SerializeField]
    Mesh mesh = default;

    [SerializeField]
    Material material = default;


    Matrix4x4[] matrics = new Matrix4x4[1023];

    Vector4[] baseColors = new Vector4[1023];

    MaterialPropertyBlock block;

    private void Awake()
    {
        for (int i = 0; i < matrics.Length; i++)
        {
            matrics[i] = Matrix4x4.TRS(Random.insideUnitSphere * 10f,
            Quaternion.Euler(Random.value * 360f, Random.value * 360, Random.value * 360),
            Vector3.one * Random.Range(0.5f, 1.5f));

            baseColors[i] = new Vector4(Random.value, Random.value, Random.value,
            Random.Range(0.5f, 1f));
        }
    }

    private void Update()
    {
        if (block == null)
        {
            block = new MaterialPropertyBlock();
            block.SetVectorArray(baseColorId, baseColors);
        }

        Graphics.DrawMeshInstanced(mesh, 0, material, matrics, 1023, block);
    }
}
