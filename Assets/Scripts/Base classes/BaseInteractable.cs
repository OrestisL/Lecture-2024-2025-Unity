using UnityEngine;
using UnityEngine.Events;

public abstract class BaseInteractable : MonoBehaviour
{
    public LayerMask PlayerLayer;
    public float InteractRadius = 2.0f;
    public GameObject CanvasPrefab;
    public AudioClip InteractClip;
    public AudioSource AudioSource;

    private GameObject _prefabInstance;
    public abstract void Interact();

    public UnityEvent<GameObject> OnPlayerDetected;

    public virtual void Start() 
    {
        AudioSource = gameObject.AddComponent<AudioSource>();
        AudioSource.spatialBlend = 1.0f;
        AudioSource.volume = 0.70f;

        AudioSource.rolloffMode = AudioRolloffMode.Linear;
        AudioSource.maxDistance = 500.0f;

    }

    private void FixedUpdate()
    {
        IsPlayerInProximity();
    }

    private void IsPlayerInProximity()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, InteractRadius, PlayerLayer);
        if (colliders.Length > 0)
        {
            if (_prefabInstance == null)
            {
                _prefabInstance = Instantiate(CanvasPrefab);
                _prefabInstance.transform.position = transform.position + 1.5f * Vector3.up;

                // ? checks for null
                OnPlayerDetected?.Invoke(colliders[0].gameObject); // the passed gameObject will be the player
            }
        }
        else
        {
            if (_prefabInstance != null)
            {
                Destroy(_prefabInstance);
                // ensure all listeners know that the player has left this interactable's proximity
                OnPlayerDetected?.Invoke(null);
            }
        }
    }
}
