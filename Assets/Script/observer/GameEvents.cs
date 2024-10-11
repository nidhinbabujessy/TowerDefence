using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance;

    // BulletHit event with parameters
    public delegate void BulletHitHandler(Vector3 hitPosition, GameObject hitObject);
    public event BulletHitHandler BulletHit;

    // Upgrade event
    public delegate void UpgradeHandler();
    public event UpgradeHandler UpgradeEvent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;  
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to trigger BulletHit event with parameters
    public void TriggerBulletHit(Vector3 hitPosition, GameObject hitObject)
    {
        BulletHit?.Invoke(hitPosition, hitObject);  // Trigger event with parameters
    }

    // Method to trigger Upgrade event
    public void TriggerUpgrade()
    {
        UpgradeEvent?.Invoke();  // Trigger upgrade event
    }
}
