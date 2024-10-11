using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject menuPanel;
    private TowerPlacement _towerPlacement;  // Reference to TowerPlacement

    private bool menuBool = true;

    private void Start()
    {
        if (TowerPlacement.Instance != null)
        {
            _towerPlacement = TowerPlacement.Instance;
        }
        else
        {
            Debug.LogWarning("TowerPlacement instance is not set!");
        }
    }

    public void UpgradeButton()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.TriggerUpgrade();  // Trigger upgrade event
        }
        else
        {
            Debug.LogWarning("GameEvents instance is not set!");
        }
    }

    public void Menu()
    {
        if (menuBool)
        {
            print("true");
            menuPanel.SetActive(true);
            menuBool = false;
        }
        else if(!menuBool)
        {
            print("false");
            menuPanel.SetActive(false);
            menuBool = true;
        }
    }
}
