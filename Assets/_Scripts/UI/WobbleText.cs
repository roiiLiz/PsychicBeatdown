using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextWobble : MonoBehaviour
{
    [Header("Wobble Settings")]
    public bool enableWobble = true;
    [SerializeField]
    private WobbleStyle wobbleStyle = WobbleStyle.LETTER;
    [SerializeField]
    private bool randomShake = false;
    [SerializeField]
    private float randomShakeMaximum = 5f;
    [Space]
    [Space]
    [SerializeField]
    private bool enableColorGradient = false;
    [SerializeField]
    private Gradient colorGradient;
    [Space]
    [Space]
    [SerializeField]
    private float xWobbleFactor = 2.0f;
    [SerializeField]
    private float yWobbleFactor = 3.0f;

    TMP_Text textMesh => GetComponent<TMP_Text>();
    Mesh mesh;
    Vector3[] verts;
    List<int> wordIndexes = new List<int>{0};
    List<int> wordLengths = new List<int>();

    public void EnableWobble(bool isEnabled) => randomShake = isEnabled;

    // Start is called before the first frame update
    void Start()
    {
        string s = textMesh.text;

        for (int i = s.IndexOf(' '); i > -1; i = s.IndexOf(' ', i + 1))
        {
            wordLengths.Add(i - wordIndexes[wordIndexes.Count - 1]);
            wordIndexes.Add(i + 1);
        }

        wordLengths.Add(s.Length - wordIndexes[wordIndexes.Count - 1]);
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;
        verts = mesh.vertices;

        Color[] colors = mesh.colors;

        float wobbleInput = randomShake ? Random.Range(0f, randomShakeMaximum) : Time.unscaledTime;

        switch (wobbleStyle)
        {
            case WobbleStyle.LETTER:
                for (int i = 0; i < textMesh.textInfo.characterCount; i++)
                {
                    TMP_CharacterInfo c = textMesh.textInfo.characterInfo[i];

                    int vIndex = c.vertexIndex;
                    Vector3 offset = Wobble(wobbleInput + i);

                    if (enableColorGradient)
                    {
                        colors[vIndex] = colorGradient.Evaluate(Mathf.Repeat(Time.time + verts[vIndex].x * 0.001f, 1f));
                        colors[vIndex + 1] = colorGradient.Evaluate(Mathf.Repeat(Time.time + verts[vIndex + 1].x * 0.001f, 1f));
                        colors[vIndex + 2] = colorGradient.Evaluate(Mathf.Repeat(Time.time + verts[vIndex + 2].x * 0.001f, 1f));
                        colors[vIndex + 3] = colorGradient.Evaluate(Mathf.Repeat(Time.time + verts[vIndex + 3].x * 0.001f, 1f));
                    }

                    verts[vIndex] += offset;
                    verts[vIndex + 1] += offset;
                    verts[vIndex + 2] += offset;
                    verts[vIndex + 3] += offset;
                }

                break;
            case WobbleStyle.WORD:
                for (int i = 0; i < wordIndexes.Count; i++)
                {
                    int wordIndex = wordIndexes[i];

                    Vector3 offset = Wobble(wobbleInput + i);
                    
                    for (int j = 0; j < wordLengths[i]; j++)
                    {
                        TMP_CharacterInfo c = textMesh.textInfo.characterInfo[wordIndex + j];

                        int vIndex = c.vertexIndex;

                        if (enableColorGradient)
                        {
                            colors[vIndex] = colorGradient.Evaluate(Mathf.Repeat(Time.time + verts[vIndex].x * 0.001f, 1f));
                            colors[vIndex + 1] = colorGradient.Evaluate(Mathf.Repeat(Time.time + verts[vIndex + 1].x * 0.001f, 1f));
                            colors[vIndex + 2] = colorGradient.Evaluate(Mathf.Repeat(Time.time + verts[vIndex + 2].x * 0.001f, 1f));
                            colors[vIndex + 3] = colorGradient.Evaluate(Mathf.Repeat(Time.time + verts[vIndex + 3].x * 0.001f, 1f));
                        }

                        verts[vIndex] += offset;
                        verts[vIndex + 1] += offset;
                        verts[vIndex + 2] += offset;
                        verts[vIndex + 3] += offset;
                    }
                }
                break;
            case WobbleStyle.ALL:
                for (int i = 0; i < verts.Length; i++)
                {
                    Vector3 offset = Wobble(wobbleInput + i);

                    if (enableColorGradient)
                    {
                        colors[i] = colorGradient.Evaluate(Mathf.Repeat(Time.time + verts[i].x * 0.001f, 1f));
                    }

                    verts[i] = verts[i] + offset;
                }
                break;
            default:
                break;
        }
        

        mesh.vertices = enableWobble ? verts : mesh.vertices;
        mesh.colors = enableColorGradient ? colors : mesh.colors;

        textMesh.canvasRenderer.SetMesh(mesh);
    }

    private Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time * xWobbleFactor), Mathf.Cos(time * yWobbleFactor));
    }

    private enum WobbleStyle
    {
        LETTER,
        WORD,
        ALL
    }
}
