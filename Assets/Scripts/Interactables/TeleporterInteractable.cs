using UnityEngine;

public class TeleporterInteractable : BaseInteractable
{
    public Transform Target;
    private GameObject _player;

    private void Start()
    {
        OnPlayerDetected.AddListener((player) => _player = player);
    }

    public override void Interact()
    {
        CharacterController controller = _player.GetComponent<CharacterController>();
        controller.enabled = false;
        _player.transform.position = Target.position;
        controller.enabled = true;
    }
}
