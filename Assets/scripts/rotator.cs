using UnityEngine;

public class rotator : MonoBehaviour
{
    public string targetTag;
    private Vector3 rotation = new Vector3(0, 1, 0);
    private float rotationSpeed = 20;

    public Rigidbody rb;
    private float floatingForce = 1f;

    private void OnTriggerEnter(Collider other)
    {
        rb = other.GetComponent<Rigidbody>();
        if (other.gameObject.tag == targetTag)
        {
            rb.useGravity = false;
            rb.AddForce(Vector3.up * floatingForce, ForceMode.Impulse); // Apply upward force
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == targetTag)
        {
            other.gameObject.transform.Rotate(rotationSpeed * Time.deltaTime * rotation);

            // Reset to avoid hovering
            rb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == targetTag)
        {
            rb.useGravity = true;
        }
    }

}
