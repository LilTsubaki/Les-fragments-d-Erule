using UnityEngine;
using System.Collections;


public class VFX_Shield : MonoBehaviour
{

    public Transform target;
    public GameObject character;

    private Vector3 characterPos = new Vector3(0.0f,0.0f,0.0f);

    private float tilt4, lift4 = 0.0f;
    [Range(-0.1f, 0.1f)] public float lift4Speed = 0.012f;
    [Range(-10.0f,10.0f)]public float revolve4 = 1.1f;

    void Start()
    {
        characterPos = character.transform.localPosition;
    } 
    void FixedUpdate()
    {
        lift4 = Mathf.Sin(Time.time) * lift4Speed;

        transform.Translate(0.0f, lift4, 0.0f, Space.Self);
        transform.RotateAround(characterPos, Vector3.up, revolve4);
        Vector3 look = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.LookAt(look, Vector3.up);
        transform.Rotate(90, 0, 0);
    }
}