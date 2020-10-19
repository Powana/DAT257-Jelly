using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PollutionHealth : MonoBehaviour
{	
	public int maxHealth;
	public int currentHealth;
	private float fillAmount;
	public HealthBar pollutionBar;
	public HealthBar waterBar;
	
	// Maxhealth for lake and pollution size
	public static int maxHealthLake = 1000;
	public static int maxHealthPollution = 1000; 
	
	void Start() {
		
		pollutionBar.SetMaxHealth(maxHealthPollution);
		waterBar.SetMaxHealth(Map.resourceManager.resources["lake"].value);		
		// Start at full health (no pullution/full health lake)
		currentHealth = maxHealth;
	}
	
	void Update() {
		pollutionChange();
	}
	
	void pollutionChange () {
		
		// Input values at tick
		int lakeHealth = Map.resourceManager.resources["lake"].value;
		int pollutionDelta = Map.resourceManager.resources["pollution"].value;

		pollutionBar.SetHealth((int)pollutionBar.slider.maxValue-pollutionDelta);
		waterBar.SetHealth(lakeHealth);

		// Safeguard - can't exceed 100% (removes overflow), write in ResourceManagers tick instead
		/*if (waterBar.slider.MaxHealth) {
			waterBar.SetHealth(lakeHealth) = waterBar.slider.MaxHealth;
		}*/
	}
}
