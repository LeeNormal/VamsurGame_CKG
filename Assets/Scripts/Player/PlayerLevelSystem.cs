using UnityEngine;

public class PlayerLevelSystem : MonoBehaviour
{
    public WeaponManager weaponManager;
    public GameObject[] weaponPrefabs;
    void Start()
    {
        if (weaponManager.CanAddWeapon())
        {
            weaponManager.AddWeapon(weaponPrefabs[0]); // 0번 무기 자동 장착
        }
    }

    public void OnLevelUp()
    {
        if (weaponManager.CanAddWeapon())
        {
            int randomIndex = Random.Range(0, weaponPrefabs.Length);
            weaponManager.AddWeapon(weaponPrefabs[randomIndex]);
        }
        else
        {
            weaponManager.UpgradeRandomWeapon();
        }
    }
}
