using UnityEngine;

[ExecuteInEditMode]
public class LightAreaBehaviour : MonoBehaviour
{
    public Material material;
    public LightBehaviour LightParent;
    void Awake()
    {
        material = new Material(Shader.Find("Custom/Light"));
        GetComponent<SpriteRenderer>().material = material;
        LightParent = transform.parent.GetComponent<LightBehaviour>();
    }

    public void Update()
    {
        material.SetColor("_Color", LightParent.light2D.pointLightColour);
        material.SetFloat("_ContrastFactor", LightParent.light2D.contrastFactorPoint);
        material.SetFloat("_ColorFactor", LightParent.light2D.colorFactorPoint);
        material.SetFloat("_IntensityFactor", LightParent.light2D.intensityVariationFactor);
    }
}
