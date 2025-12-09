using UnityEngine;
using TMPro;

public class VelocityText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmpText;
    
    void Start()
    {
        tmpText = GetComponent<TextMeshProUGUI>(); // Get the TextMeshProUGUI component
    }
    void FixedUpdate()
    {
        Rigidbody rb = GameObject.Find("Spaceship4_2").GetComponent<Rigidbody>();
        float speed = rb.linearVelocity.magnitude; // Calculate speed from velocity vector magnitude
        tmpText.text = speed.ToString("F2") + " m/s";
    }
}
