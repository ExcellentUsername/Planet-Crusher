using UnityEngine;
using TMPro;

public class MassDestroyedText : MonoBehaviour
{
    private PlanetaryManager mgr;
    private float totalMassDestroyed = 0f;
    [SerializeField] private TextMeshProUGUI tmpText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mgr = Object.FindAnyObjectByType<PlanetaryManager>();
        mgr.onPlanetaryBodyExplosion.AddListener(updateDestroyedMass);

        tmpText = GetComponent<TextMeshProUGUI>(); // Get the TextMeshProUGUI component
        DisplayDestroyedMass(0f); // Initialize display to 0
    }
    void updateDestroyedMass(float mass)
    {
        totalMassDestroyed += mass; // Update the displayed mass

        DisplayDestroyedMass(mass); // Display the displayed mass
    }
    void DisplayDestroyedMass(float mass)
    {
        tmpText.text = totalMassDestroyed.ToString("F2");
    }
}