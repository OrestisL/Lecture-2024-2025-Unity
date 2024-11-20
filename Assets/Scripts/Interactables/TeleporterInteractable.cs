using UnityEngine;

public class TeleporterInteractable : BaseInteractable
{
    public Transform Target;
    private GameObject _player;

    public override void Start()
    {
        base.Start();
        InteractClip = SoundManager.Instance.GetSFX(0);
        OnPlayerDetected.AddListener((player) => _player = player);
    }

    public override void Interact()
    {
        AudioSource.PlayOneShot(InteractClip);

        CharacterController controller = _player.GetComponent<CharacterController>();
        controller.enabled = false;

        _player.transform.position = Target.position;
        controller.enabled = true;
    }
}
