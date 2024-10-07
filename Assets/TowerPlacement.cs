using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    public GameObject towerPrefab;
    public GameObject ghostTowerPrefab; // Ghost tower for preview
    public LayerMask gridLayer;
    public LayerMask towerLayer;
    public float towerRadius = 0f; // Radius to check for existing towers

    private GameObject ghostTower; // Instance of the ghost tower
    private Renderer ghostRenderer; // To control ghost tower color

    void Start()
    {
        // Instantiate the ghost tower and disable it at the start
        ghostTower = Instantiate(ghostTowerPrefab);
        ghostTower.SetActive(false);
        ghostRenderer = ghostTower.GetComponent<Renderer>();
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, gridLayer))
        {
            Vector3 gridPosition = new Vector3(Mathf.Round(hit.point.x), 0, Mathf.Round(hit.point.z));

            // Show the ghost tower and update its position
            ghostTower.SetActive(true);
            ghostTower.transform.position = gridPosition;

            // Check if a tower already exists at the grid position
            if (Physics.CheckSphere(gridPosition, towerRadius, towerLayer))
            {
                // Invalid placement, set ghost tower to red
                ghostRenderer.material.color = Color.red;
            }
            else
            {
                // Valid placement, set ghost tower to green
                ghostRenderer.material.color = Color.green;
            }

            // On left-click, place the tower if valid
            if (Input.GetMouseButtonDown(0) && !Physics.CheckSphere(gridPosition, towerRadius, towerLayer))
            {
                Instantiate(towerPrefab, gridPosition, Quaternion.identity);
            }
        }
        else
        {
            // Hide the ghost tower if the mouse is not over the grid
            ghostTower.SetActive(false);
        }
    }
}
