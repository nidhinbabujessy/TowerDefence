using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] GameObject[] upgradedTowers;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] int upgradeCost = 10;
    [SerializeField] GameObject spanner;

    private void Start()
    {
        spanner.SetActive(false);
    }

    void Update()
    {
        spanner.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == spanner.transform)
                {
                    print("upgrade");
                    TowerUpgrade();
                }
            }
        }
    }

    public void TowerUpgrade()
    {
       
        {
            if (gameObject.CompareTag("Tower1"))
            {
                Instantiate(upgradedTowers[0], transform.position, Quaternion.identity);
                Score.Instance.SpendScore(upgradeCost);
                Destroy(gameObject);
            }
            else if (gameObject.CompareTag("Tower2"))
            {
                Instantiate(upgradedTowers[1], transform.position, Quaternion.identity);
                Score.Instance.SpendScore(upgradeCost);
                Destroy(gameObject);
            }
        }
    }

    public void SpannerEnable()
    {
        spanner.SetActive(true);
    }
}
