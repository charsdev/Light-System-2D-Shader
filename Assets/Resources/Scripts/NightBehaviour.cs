using System.Collections.Generic;
using UnityEngine;

public class NightBehaviour : MonoBehaviour
{
    public Material material;
    public Color shadowColour;
    public float radius;
    public float[] ratios1;
    public float[] ratios2;
    public float[] Alphas;
    private Vector4[] vectors;
    private Vector3 pos;
    private LightBehaviour[] Lights;
    public LightItem[] items;

    private void Update()
    {
        Lights = FindObjectsOfType(typeof(LightBehaviour)) as LightBehaviour[];
        material = Resources.Load("Materials/Night", typeof(Material)) as Material;
        System.Array.Resize(ref items, Lights.Length);
        System.Array.Resize(ref ratios1, Lights.Length);
        System.Array.Resize(ref ratios2, Lights.Length);
        System.Array.Resize(ref Alphas, Lights.Length);
        System.Array.Resize(ref vectors, Lights.Length);

        for (int i = 0; i < Lights.Length; i++)
        {
            Lights[i].alphaFade = Lights[i].fadein ?
                Mathf.MoveTowards(Lights[i].alphaFade, 1, 0.5f * Time.deltaTime) :
                Mathf.MoveTowards(Lights[i].alphaFade, 0, 0.1f * Time.deltaTime);

            if (!Lights[i].Indepedient)
            {
                Alphas[i] = Lights[i].alphaFade;
            }
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        for (int i = 0; i < Lights.Length; i++)
        {
            items[i] = Lights[i].light2D;

            if (Lights[i] == null)
            {
                continue;
            }

            pos = Lights[i].transform.position;
            pos.y *= 1;

            Vector4 vl = Camera.main.WorldToViewportPoint(pos);
            vl.z = ratios1[i];
            vl.w = Alphas[i];
            vectors[i] = vl;
        }
        material.SetColor("ShadowColour", shadowColour);
        material.SetFloatArray("_AspectRatios", ratios2);
        material.SetVectorArray("_Lights", vectors);
        Graphics.Blit(source, destination, material);
    }
}
