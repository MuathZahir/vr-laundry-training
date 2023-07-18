using System.Collections;
using System.Collections.Generic;
using MagicaCloth;
using UnityEngine;

public class ClothColliderSetter : MonoBehaviour
{
    [ContextMenu("Set Colliders")]
    public void SetColliders()
    {
        var colliders = gameObject.GetComponentsInChildren<ColliderComponent>();
        var cloths = gameObject.GetComponentsInChildren<MagicaMeshCloth>();
        
        foreach (var cloth in cloths)
        {
            cloth.ClearCollidersEditor();
            foreach (var collider in colliders)
            {
                cloth.AddColliderEditor(collider);
            }
            cloth.CreateVerifyData();
        }
    }
}
