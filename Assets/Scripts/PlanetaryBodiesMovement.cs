using UnityEngine;
using System.Collections.Generic;

public class PlanetaryBodiesMovement : MonoBehaviour
{
    float planetSpawnRingDistance = 15f;
    public List<PlanetaryBody> planetaryBodies = new List<PlanetaryBody>();

    Vector3 PositionNewPlanetaryBody(int currentBodyIndex)
    {
        Vector3 randomDirection = Random.onUnitSphere; // Get a random direction in 3D space
        Vector3 currentBodyPosition = Vector3.zero; // Placeholder for new body position
        PlanetaryBody previousBody = planetaryBodies[currentBodyIndex - 1]; // Get the previous body to base the new position on
        float updatedSpawnRingRadius; //Neccesary line of code because System.Collections.Generic
        updatedSpawnRingRadius = planetSpawnRingDistance * currentBodyIndex; // Make a spawn ring radius based on the index, so each body is not within 1 planetSpawnRingDistance of each other
        
        // Calculate new positions based on previous position, random direction and updated spawn ring radius
        currentBodyPosition.x = previousBody.shape.transform.position.x + randomDirection.x * updatedSpawnRingRadius; 
        currentBodyPosition.y = previousBody.shape.transform.position.y + randomDirection.y * updatedSpawnRingRadius; 
        currentBodyPosition.z = previousBody.shape.transform.position.z + randomDirection.z * updatedSpawnRingRadius;
        return currentBodyPosition;
    }

    public void GeneratePlanetaryBodyCluster()
    {
        for (int i = 0; i < planetaryBodies.Count; i++)
        {
            PlanetaryBody body = planetaryBodies[i];
            if (i == 0)
            {
                body.shape.transform.position = Vector3.zero; // First body at origin
            }
            else
            {
                body.shape.transform.position = PositionNewPlanetaryBody(i); // Position of new body based on previous body
            }
        }
    }

    public void UpdateForcesOnPlanetaryBodies()
    {
        foreach (PlanetaryBody body1 in planetaryBodies)
        {
            Vector3 totalForce = Vector3.zero; // Sum of forces acting on body1
            foreach (PlanetaryBody body2 in planetaryBodies)
            {
                if (body1 == body2)
                {
                    continue; // Skip self-interaction
                }
                else
                {
                    Vector3 directionVector = body2.shape.transform.position - body1.shape.transform.position;
                    float distance = directionVector.magnitude;
                    float forceMagnitude = (body1.mass * body2.mass) / (distance * distance); // Gravitational force formula (ignoring G for simplicity)
                    Vector3 force = directionVector.normalized * forceMagnitude;
                    totalForce += force;
                }
            }
            body1.m_Rigidbody.AddForce(totalForce, ForceMode.Impulse); // Apply force to body1 towards body2
        }
    }

}