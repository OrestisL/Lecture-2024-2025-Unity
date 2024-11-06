using UnityEngine;
using System;
public abstract class BaseInteractable : MonoBehaviour
{
    public LayerMask PlayerLayer;
    public float InteractRadius = 2.0f;
    public GameObject CanvasPrefab;

    private GameObject _prefabInstance;
    public abstract void Interact();

    private void FixedUpdate()
    {
        IsPlayerInProximity();
    }

    private void IsPlayerInProximity()
    {
        if (Physics.CheckSphere(transform.position, InteractRadius, PlayerLayer))
        {
            if (_prefabInstance == null)
            {
                _prefabInstance = Instantiate(CanvasPrefab);
                _prefabInstance.transform.position = transform.position + 1.5f * Vector3.up ;
            }
        }
        else
        {
            if (_prefabInstance != null)
                Destroy(_prefabInstance);
        }
    }
}
