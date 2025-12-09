using UnityEngine;
using UnityEngine.Events;

public class PlanetaryManager : PlanetaryBodiesMovement
{
    public int startNumberOfPlanetaryBodies = 100;
    public UnityEvent<float> onPlanetaryBodyExplosion;
    
    void Awake()
    {
        for (int i = 0; i < startNumberOfPlanetaryBodies; i++)
        {
            AddPlanetaryBody(); // Add a set amount of planetary bodies at start
        }
        GenerateAllPlanetaryBodyproperties();
        foreach (PlanetaryBody body in planetaryBodies)
        {
            body.applyPlanetaryBody(); // Apply the generated properties to each planetary body
        }
    }
    void Start()
    {
        GeneratePlanetaryBodyCluster(); // Update positions after properties are generated
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateForcesOnPlanetaryBodies(); // Update forces on planetary bodies each frame
    }

    void AddPlanetaryBody()
    {
        PlanetaryBody body = ScriptableObject.CreateInstance<PlanetaryBody>(); // Create a new instance of PlanetaryBody
        planetaryBodies.Add(body); // Add the new body to the list
    }
    void GenerateAllPlanetaryBodyproperties()
    {
        foreach (PlanetaryBody body in planetaryBodies)
        {
            GeneratePlanetaryBodyProperties(body); // Generate properties for each body
        }
    }

    void GeneratePlanetaryBodyProperties(PlanetaryBody body) // Generate random properties for a planetary body
    {
        body.mass = Random.Range(2000f, 7000f);
        body.radius = Random.Range(5f, 125f);
        body.surfaceColor = Random.ColorHSV();
    }
}