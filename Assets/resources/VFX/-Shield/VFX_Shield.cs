using UnityEngine;
using System.Collections;


public class VFX_Shield : MonoBehaviour
{
    public GameObject character;

    private float tilt4, lift4 = 0.0f;
    [Range(-0.01f, 0.01f)] public float lift4Speed = 0.004f;
    [Range(-5.0f,5.0f)]public float revolve4 = 1.1f;

    void Start()
    {

    }

    void FixedUpdate()
    {
        lift4 = Mathf.Sin(Time.time) * lift4Speed;

        transform.Translate(0.0f, lift4, 0.0f, Space.Self);
        transform.RotateAround(character.transform.position, Vector3.up, revolve4);
    }
}