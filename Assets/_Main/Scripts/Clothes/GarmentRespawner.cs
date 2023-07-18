using System;
using System.Collections;
using UnityEngine;

public class GarmentRespawner : MonoBehaviour
{
    [SerializeField] private GameObject root;
    [SerializeField] private GameObject heldCloth;
    [SerializeField] private GameObject idleCloth;
    [SerializeField] private bool local;
    [SerializeField] private float respawnBelow;
    [SerializeField] private bool isGarment = true;

    private Transform _initialParent;
    private Vector3 _initialPosition;
    
    private void Start()
    {
        SetSpawnPosition();
    }

    private void Update()
    {
        if(root.transform.position.y <= respawnBelow)
            Respawn();
    }

    public void SetSpawnPosition()
    {
        _initialParent = transform.parent;
        _initialPosition = local ? transform.localPosition : transform.position;
    }
    
    public void Respawn()
    {
        transform.SetParent(_initialParent, true);
        
        if (local)
            transform.localPosition = _initialPosition;
        else
            transform.position = _initialPosition;

        if (!isGarment) return;
        root.transform.localPosition = Vector3.zero;
        
        var rb = root.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        
        idleCloth.SetActive(true);
        heldCloth.SetActive(false);
    }
}
