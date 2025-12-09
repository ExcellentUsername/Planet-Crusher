using UnityEngine;

[CreateAssetMenu(fileName = "PlanetaryBody", menuName = "Scriptable Objects/PlanetaryBody")]
public class PlanetaryBody : ScriptableObject
{
    public float mass;
    public float radius;
    public Color surfaceColor;
    public GameObject shape;
    public Rigidbody m_Rigidbody;
    private ParticleSystem particleSystem;
    private ParticleSystemRenderer particleRenderer;
    public void applyPlanetaryBody()
    {
        // Create the visual representation as a sphere
        shape = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        shape.transform.localScale = new Vector3(radius, radius, radius);

        // Add collider handler script and assign this ScriptableObject as its data source
        PlanetaryBodyCollider collider = shape.AddComponent<PlanetaryBodyCollider>(); // Add the collider script
        collider.planetaryBody = this; // Assign this PlanetaryBody instance to the collider
        
        // Configure Rigidbody
        m_Rigidbody = shape.AddComponent<Rigidbody>();
        m_Rigidbody.useGravity = false; // Disable Unity's built-in gravity
        m_Rigidbody.mass = mass; 

        // Add and configure audio source for explosion sound
        AudioSource audioSource = shape.AddComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>("Audio/explosion_sound");
        audioSource.playOnAwake = false;
        
        // Add and configure light component
        Light lightComp = shape.AddComponent<Light>();
        lightComp.color = surfaceColor;
        lightComp.range = radius * 16f;
        lightComp.intensity = 5f * mass;
        lightComp.renderMode = LightRenderMode.ForcePixel;

        // Set up material with color and emission
        Renderer renderer = shape.GetComponent<Renderer>();
        Material baseMat = Resources.Load<Material>("Materials/BallMaterial");
        Material matInstance = new Material(baseMat);
        
        // URP uses _BaseColor instead of _Color
        matInstance.SetColor("_BaseColor", surfaceColor);
        
        // Enable emission and set emission color
        matInstance.EnableKeyword("_EMISSION");
        matInstance.SetColor("_EmissionColor", surfaceColor);
        // Also set this to ensure emission intensity
        matInstance.SetFloat("_EmissiveExposureWeight", 1f);
        
        // Assign the material instance to the renderer
        renderer.material = matInstance;

        // Create particle system once
        AddParticleSystem();
    }

    void AddParticleSystem()
    {
        // Add particle system component
        particleSystem = shape.AddComponent<ParticleSystem>();
        particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);  // Stop before configuring so it does not play on start
                
        // Configure explosion-like particle system
        ParticleSystem.MainModule main = particleSystem.main;
        main.duration = 0.5f;  // Short burst duration
        main.loop = false;     // Don't loop
        main.startSpeed = new ParticleSystem.MinMaxCurve(15f, 35f);  // High speed
        main.startLifetime = new ParticleSystem.MinMaxCurve(0.5f, 0.8f);  // Short lifetime
        main.startSize = new ParticleSystem.MinMaxCurve(0.2f, 0.5f);
        // Use the planetary surface color so particle color matches the body
        main.startColor = new ParticleSystem.MinMaxGradient(surfaceColor);
        main.gravityModifier = -0.1f;  // Slight upward bias

        // Configure emission for burst effect
        ParticleSystem.EmissionModule emission = particleSystem.emission;
        emission.rateOverTime = 0;  // No continuous emission
        emission.SetBursts(new ParticleSystem.Burst[] { 
            new ParticleSystem.Burst(0f, 120, 200)  // 50-70 particles at start
        });

        // Configure shape to emit from sphere surface
        ParticleSystem.ShapeModule shape_module = particleSystem.shape;
        shape_module.shapeType = ParticleSystemShapeType.Sphere;
        shape_module.radius = radius * 0.2f;  // Emit from around the body
        
        // Configure renderer for particle system
        ParticleSystemRenderer psRenderer = shape.GetComponent<ParticleSystemRenderer>();
        Material psMat = Resources.Load<Material>("Materials/ParticleSystemMaterial");
        Material psMatInstance = new Material(psMat);  // Create instance!
        int colorId = Shader.PropertyToID("_Color");
        psMatInstance.SetColor(colorId, surfaceColor);
        psRenderer.material = psMatInstance;  // Assign instance
        particleRenderer = psRenderer;
    }
    public void Explode()
    {
        if (particleSystem != null)
        {
            // Make sure it's stopped before playing again
            particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            if (particleRenderer != null)
            {
                particleRenderer.enabled = true;  // Enable only the particle system renderer
            }
            particleSystem.Play();  // Play the explosion
            shape.GetComponent<AudioSource>().Play();
            Destroy(shape.GetComponent<Renderer>()); // Remove visual representation
            Destroy(shape.GetComponent<Collider>()); // Remove collider
            mass = 0; // Set mass to 0 to avoid further interactions
        }
    }
}