using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // 장착 중인 무기 리스트
    public List<WeaponBase> equippedWeapons = new();           
     // 아직 장착하지 않은 무기 프리팹 리스트
    public List<GameObject> availableWeaponPrefabs = new();
    // 무기 슬롯 최대 개수
    public int maxWeapons = 5;                                  

    private void Update()
    {
        foreach (var weapon in equippedWeapons)
        {
            weapon.Attack();
        }
    }

    // 무기 추가 가능한지 체크
    public bool CanAddWeapon()
    {
        return equippedWeapons.Count < maxWeapons;
    }

    // 아직 장착하지 않은 무기가 남아있는지 체크
    public bool HasAvailableWeapons()
    {
        return availableWeaponPrefabs.Count > 0;
    }

    // 아직 장착하지 않은 무기 중 하나 랜덤으로 뽑기
    public GameObject GetRandomAvailableWeapon()
    {
        if (availableWeaponPrefabs.Count == 0)
            return null;

        int index = Random.Range(0, availableWeaponPrefabs.Count);
        GameObject selected = availableWeaponPrefabs[index];
        availableWeaponPrefabs.RemoveAt(index); // 뽑은 건 리스트에서 제거
        return selected;
    }

    // 무기 추가
    public void AddWeapon(GameObject weaponPrefab)
    {
        if (!CanAddWeapon())
        {
            Debug.LogWarning("무기 슬롯이 가득 찼습니다!");
            return;
        }

        if (weaponPrefab == null)
        {
            Debug.LogError("무기 프리팹이 null입니다!");
            return;
        }

        GameObject weaponObj = Instantiate(weaponPrefab, transform);
        WeaponBase weapon = weaponObj.GetComponent<WeaponBase>();

        if (weapon != null)
        {
            weapon.Initialize();
            equippedWeapons.Add(weapon);
            Debug.Log($"{weapon.weaponName} 무기 장착 완료!");

            // !!! 추가 !!!
            availableWeaponPrefabs.Remove(weaponPrefab); // 장착한 무기는 리스트에서 제거
        }
        else
        {
            Debug.LogError("무기 프리팹에 WeaponBase 스크립트가 없습니다!");
        }
    }


    // 이미 장착한 무기 중 랜덤으로 하나 업그레이드
    public void UpgradeRandomWeapon()
    {
        if (equippedWeapons.Count == 0)
            return;

        int index = Random.Range(0, equippedWeapons.Count);
        equippedWeapons[index].Upgrade();
    }
}
