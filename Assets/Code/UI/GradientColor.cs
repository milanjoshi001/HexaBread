using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Graphic))]
public class GradientColor : BaseMeshEffect
{
    public Color Top;
    public Color Bottom;

    public override void ModifyMesh(VertexHelper vh)
    {
        if (!IsActive())
            return;

        UIVertex vertex = new UIVertex();

        float topY = float.MinValue;
        float bottomY = float.MaxValue;

        // Find bounds
        for (int i = 0; i < vh.currentVertCount; i++)
        {
            vh.PopulateUIVertex(ref vertex, i);
            topY = Mathf.Max(topY, vertex.position.y);
            bottomY = Mathf.Min(bottomY, vertex.position.y);
        }

        float height = topY - bottomY;

        // Apply gradient
        for (int i = 0; i < vh.currentVertCount; i++)
        {
            vh.PopulateUIVertex(ref vertex, i);

            float t = Mathf.InverseLerp(bottomY, topY, vertex.position.y);
            vertex.color *= Color.Lerp(Bottom, Top, t);

            vh.SetUIVertex(vertex, i);
        }
    }
}