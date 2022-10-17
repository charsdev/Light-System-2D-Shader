using UnityEngine;

[ExecuteInEditMode]
public class LightBehaviour : MonoBehaviour
{
    public Material material;
    public LightItem light2D;
    private float fade;
    public bool fadein;
    public bool Indepedient;
    public float alphaFade;

    private void Awake()
    {
        material = new Material(Shader.Find("Custom/Light"));
        GetComponent<SpriteRenderer>().material = material;
    }

    public void Update()
    {
        fade = fadein ?
             Mathf.MoveTowards(fade, 0.5f, 0.15f * Time.deltaTime) :
             Mathf.MoveTowards(fade, 0, 0.1f * Time.deltaTime);

        if (!Indepedient)
        {
            light2D.contrastFactorPoint = fade;
        }

        material.SetColor("_Color", light2D.pointLightColour);
        material.SetFloat("_ContrastFactor", light2D.contrastFactorPoint);
        material.SetFloat("_ColorFactor", light2D.colorFactorPoint);
        material.SetFloat("_IntensityFactor", light2D.intensityVariationFactor);
    }

    public void OnTriggerStay2D(Collider2D other) => fadein = other.gameObject.tag == "Player";
    public void OnTriggerExit2D(Collider2D other) => fadein = false;
}
