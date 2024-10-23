using UnityEngine;

public class ColorChange : MonoBehaviour
{
    [SerializeField]
    private Color m_randomColor;
    [SerializeField]
    private Material m_material;

    public float UpwardsForce = 10.0f;
    private void Start()
    {
        m_material = GetComponent<Renderer>().material;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //change color for both objects
        m_randomColor = Random.ColorHSV();
        GameObject other = collision.gameObject;
        other.transform.localScale = Random.Range(0.5f, 1.5f) * Vector3.one;

        Renderer otherRend = other.GetComponent<Renderer>();
        if (otherRend != null)
            otherRend.material.color = m_randomColor;

        m_material.color = m_randomColor;

        //apply upwards force to other
        Rigidbody otherBody = other.GetComponent<Rigidbody>();
        if (otherBody == null)
            return;

        float random = Random.Range(1.1f, 1.3f);
        otherBody.AddForce(Vector3.up * UpwardsForce * random, ForceMode.Acceleration);
    }
}
