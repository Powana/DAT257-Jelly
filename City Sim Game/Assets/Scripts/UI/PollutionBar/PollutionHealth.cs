using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PollutionHealth : MonoBehaviour
{	
	public int maxHealth = 5000;
	public int currentHealth;
	private float fillAmount;
	public HealthBar pollutionBar;
	public HealthBar waterBar;
	
	void Start() {
		
		// Start at full health (no pullution/full health lake)
		currentHealth = maxHealth;
		
		// Set pollution/lake max slider value
		// Placeholder, should send in resources.pollution/lake max value
		pollutionBar.SetMaxHealth(maxHealth);
		waterBar.SetMaxHealth(maxHealth);
	}
	
	void Update() {
		
		// Placeholder for actual resource value
		// Call on pollutionChange which contains resource.delta
		// Placeholder decrease		
		int pollutionDelta = 10;
		if (Input.GetKeyDown(KeyCode.Q)) {
			pollutionChange(-pollutionDelta);
		} 
		
		// Placeholder increase
		if (Input.GetKeyDown(KeyCode.E)) {
			
			pollutionChange(pollutionDelta);
		}
		
		// Safeguard - can't exceed 100% (removes overflow)
		if (currentHealth > maxHealth) {
			currentHealth = maxHealth;
		}
	
	}
	
	void pollutionChange (int deltaPollution) {
		
		// Input delta values from resource.pollution delta at tick
		
			currentHealth += deltaPollution;

		pollutionBar.SetHealth(currentHealth);
		waterBar.SetHealth(currentHealth);
	}
}
