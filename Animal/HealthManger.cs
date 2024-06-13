using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float energy = 100f; // Initial energy level of the cube
    public float energyDecreasePerSecond = 1f; // Energy decrease per second when moving

    private Vector3 lastPosition; // To track movement

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        // Check if the cube has moved
        if (Vector3.Distance(transform.position, lastPosition) > 0)
        {
            // Decrease energy based on time and movement
            energy -= energyDecreasePerSecond * Time.deltaTime;
            lastPosition = transform.position;

            // Check if energy falls below zero
            if (energy <= 0)
            {
                Die(); // Call the Die method if energy is zero or less
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check for collision with a food object
        if (other.gameObject.CompareTag("Food"))
        {
            energy = 100f; // Replenish energy to original level
            Destroy(other.gameObject); // Optionally, remove the food object
            Debug.Log("FOOD EATEN ENERG RESTORED to :  " + energy);
        }
    }

    private void Die()
    {
        Destroy(gameObject); // Destroy the cube object
        Debug.Log("Someone just died of hunger lmao");
    }
}
