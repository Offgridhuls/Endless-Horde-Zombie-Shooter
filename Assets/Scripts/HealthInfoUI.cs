using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthInfoUI : MonoBehaviour
{
    public Text healthText;
    public Text maxHealthText;
    HealthComponent playerHealthComponent;
    // Start is called before the first frame update
    private void OnEnable()
    {
        PlayerEvents.OnHealthInitialized += OnHealthInitialized;
    }

    private void OnDisable()
    {
        PlayerEvents.OnHealthInitialized -= OnHealthInitialized;
    }
    void Start()
    {
        
    }
    private void OnHealthInitialized(HealthComponent healthComponent)
    {
        playerHealthComponent = healthComponent;
    }
    // Update is called once per frame
    void Update()
    {
        healthText.text = playerHealthComponent.CurrentHealth.ToString();
        maxHealthText.text = playerHealthComponent.MaxHealth.ToString();
    }
}
