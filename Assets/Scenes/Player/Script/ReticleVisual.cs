using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleVisual : MonoBehaviour
{
    [Header("Color")]
    public Color normalColor = new Color(0f, 1f, 1f, 1f);
    public Color lockOnColor = new Color(1f, 0.2f, 0.1f, 1f);

    [Header("Reticle Size")]
    public float cornerDistance = 0.8f;
    public float cornerLength = 0.25f;
    public float lineWidth = 0.035f;
    public float centerSize = 0.06f;
    public float circleRadius = 1.1f;

    [Header("Circle")]
    public int circleSegments = 64;

    private LineRenderer[] lines;
    private Material lineMaterial;
    private bool isLockedOn;

    void Start()
    {
        lineMaterial = new Material(
            Shader.Find("Sprites/Default")
        );

        CreateReticle();
        SetColor(normalColor);
    }

    void CreateReticle()
    {
        // 四隅のL字マーカー
        lines = new LineRenderer[10];

        CreateCorner(0, -1, 1);
        CreateCorner(1, 1, 1);
        CreateCorner(2, -1, -1);
        CreateCorner(3, 1, -1);

        // 外側の円
        CreateCircle(4);

        // 中央の十字
        CreateLine(5, new Vector3(-centerSize, 0, 0),
            new Vector3(centerSize, 0, 0));

        CreateLine(6, new Vector3(0, -centerSize, 0),
            new Vector3(0, centerSize, 0));

        // 中央周辺の補助マーカー
        CreateLine(7, new Vector3(-0.2f, 0, 0),
            new Vector3(-0.1f, 0, 0));

        CreateLine(8, new Vector3(0.1f, 0, 0),
            new Vector3(0.2f, 0, 0));

        CreateLine(9, new Vector3(0, 0.1f, 0),
            new Vector3(0, 0.2f, 0));
    }

    void CreateCorner(int index, float x, float y)
    {
        GameObject obj = new GameObject("Corner_" + index);
        obj.transform.SetParent(transform, false);

        LineRenderer line = obj.AddComponent<LineRenderer>();

        line.useWorldSpace = false;
        line.positionCount = 3;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.material = lineMaterial;
        line.numCapVertices = 4;

        float startX = x * cornerDistance;
        float startY = y * cornerDistance;

        line.SetPosition(0, new Vector3(
            startX - x * cornerLength,
            startY, 0));

        line.SetPosition(1, new Vector3(
            startX, startY, 0));

        line.SetPosition(2, new Vector3(
            startX, startY - y * cornerLength, 0));

        lines[index] = line;
    }

    void CreateCircle(int index)
    {
        GameObject obj = new GameObject("OuterCircle");
        obj.transform.SetParent(transform, false);

        LineRenderer line = obj.AddComponent<LineRenderer>();

        line.useWorldSpace = false;
        line.loop = true;
        line.positionCount = circleSegments;
        line.startWidth = lineWidth * 0.6f;
        line.endWidth = lineWidth * 0.6f;
        line.material = lineMaterial;

        for (int i = 0; i < circleSegments; i++)
        {
            float angle = i * Mathf.PI * 2f / circleSegments;

            line.SetPosition(i, new Vector3(
                Mathf.Cos(angle) * circleRadius,
                Mathf.Sin(angle) * circleRadius,
                0
            ));
        }

        lines[index] = line;
    }

    void CreateLine(int index, Vector3 start, Vector3 end)
    {
        GameObject obj = new GameObject("Center_" + index);
        obj.transform.SetParent(transform, false);

        LineRenderer line = obj.AddComponent<LineRenderer>();

        line.useWorldSpace = false;
        line.positionCount = 2;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.material = lineMaterial;

        line.SetPosition(0, start);
        line.SetPosition(1, end);

        lines[index] = line;
    }

    public void SetLockOn(bool locked)
    {
        isLockedOn = locked;

        SetColor(isLockedOn ? lockOnColor : normalColor);
    }

    void SetColor(Color color)
    {
        if (lines == null) return;

        foreach (LineRenderer line in lines)
        {
            if (line == null) continue;

            line.startColor = color;
            line.endColor = color;
        }
    }

    void OnDestroy()
    {
        if (lineMaterial != null)
            Destroy(lineMaterial);
    }
}
