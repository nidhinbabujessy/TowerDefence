using UnityEngine;
using System.Collections.Generic;

public class TowerPlacement : MonoBehaviour
{
    public static TowerPlacement Instance;

    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private GameObject ghostTowerPrefab;
    [SerializeField] private LayerMask gridLayer;
    [SerializeField] private LayerMask towerLayer;
    [SerializeField] private float towerRadius = 0.5f;

    private GameObject ghostTower;
    private Renderer ghostRenderer;

    public bool upgradebool;

    // List to store placed towers
    private List<GameObject> placedTowers = new List<GameObject>();

    private void Start()
    {
        InitializeGhostTower();
    }

    private void Update()
    {
        if (upgradebool)
        {
            UpdateGhostTower();
            HandleTowerPlacement();
        }
    }

    private void InitializeGhostTower()
    {
        ghostTower = Instantiate(ghostTowerPrefab);
        ghostTower.SetActive(false);
        ghostRenderer = ghostTower.GetComponent<Renderer>();
    }

    private void UpdateGhostTower()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, gridLayer))
        {
            Vector3 gridPosition = new Vector3(Mathf.Round(hit.point.x), 0, Mathf.Round(hit.point.z));
            ghostTower.SetActive(true);
            ghostTower.transform.position = gridPosition;
            ghostRenderer.material.color = IsValidPlacement(gridPosition) ? Color.green : Color.red;
        }
        else
        {
            ghostTower.SetActive(false);
        }
    }

    private bool IsValidPlacement(Vector3 position)
    {
        return !Physics.CheckSphere(position, towerRadius, towerLayer);
    }

    private void HandleTowerPlacement()
    {
        if (Input.GetMouseButtonDown(0) && IsValidPlacement(ghostTower.transform.position))
        {
            PlaceTower(ghostTower.transform.position);
        }
    }

    private void PlaceTower(Vector3 position)
    {
        GameObject newTower = Instantiate(towerPrefab, position, Quaternion.identity);
        placedTowers.Add(newTower); // Add the new tower to the list
    }

    // Method to get the list of placed towers
    public List<GameObject> GetPlacedTowers()
    {
        return placedTowers;
    }

    public void Upgrade()
    {
        for (int i = 0; i < placedTowers.Count; i++)
        {
            Tower tower = placedTowers[i].GetComponent<Tower>();
            tower.SpannerEnable();
        }
    }

    public void EnableTowerPlacement()
    {
        upgradebool = true;
    }
    public void DisableTowerPlacement()
    {
        upgradebool = false;
    }
}
