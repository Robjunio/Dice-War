using UnityEngine;

public class DiceHandler : MonoBehaviour
{
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Throw()
    {
        AudioClip audioClip = Resources.Load<AudioClip>("Sounds/Throw_Dice");
        AudioSource.PlayClipAtPoint(audioClip, transform.position);

        // Calculate torque
        float x = Random.Range(0, 200);
        float y = Random.Range(0, 200);
        float z = Random.Range(0, 200);

        transform.rotation = Quaternion.identity;
        rb.AddForce(Vector3.up * Random.Range(50, 200));
        rb.AddTorque(x, y, z);
    }
}
