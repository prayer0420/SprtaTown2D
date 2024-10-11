using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Transform itemSlotContainer;
    public GameObject itemSlotPrefab;

    private void Start()
    {
        UpdateInventoryUI();
    }

    public void UpdateInventoryUI()
    {
        // 기존 슬롯 삭제
        foreach (Transform child in itemSlotContainer)
        {
            Destroy(child.gameObject);
        }

        // 인벤토리 아이템 표시
        foreach (var item in GameManager.Instance.Player.inventorySystem.items)
        {
            GameObject itemSlot = Instantiate(itemSlotPrefab, itemSlotContainer);
            // 아이템 정보 설정
            itemSlot.GetComponent<ItemSlot>().SetItem(item);
        }
    }
}
