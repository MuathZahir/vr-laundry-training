using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public bool IsOn { get; set; }
    public float SpeedMultiplier { get; set; }
    
    [SerializeField] private float speed, conveyorSpeed;
    [SerializeField] private Vector3 direction;
    [SerializeField] private List<GameObject> onBelt;
    [SerializeField] private MeshRenderer renderer;

    private Material material;
    //private MeshRenderer _meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        material = renderer.material;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!IsOn) return;

        material.mainTextureOffset += new Vector2(-1, 0) * (conveyorSpeed * SpeedMultiplier * Time.deltaTime);
    }

    // Fixed update for physics
    void FixedUpdate()
    {
        if(!IsOn) return;
        
        // For every item on the belt, add force to it in the direction given
        for (int i = 0; i < onBelt.Count; i++)
        {
            onBelt[i].GetComponent<Rigidbody>().AddForce(speed * SpeedMultiplier * direction);
        }
    }

    // When something collides with the belt
    private void OnCollisionEnter(Collision collision)
    {
        onBelt.Add(collision.gameObject);
    }

    // When something leaves the belt
    private void OnCollisionExit(Collision collision)
    {
        onBelt.Remove(collision.gameObject);
    }
}