using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float Speed;

    private void Update()
    {
        transform.Translate(
            new Vector2(
                Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical")
            ) * Time.deltaTime * Speed
        );
    }
}
