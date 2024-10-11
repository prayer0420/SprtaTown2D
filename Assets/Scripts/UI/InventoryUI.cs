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
        // ���� ���� ����
        foreach (Transform child in itemSlotContainer)
        {
            Destroy(child.gameObject);
        }

        // �κ��丮 ������ ǥ��
        foreach (var item in GameManager.Instance.Player.inventorySystem.items)
        {
            GameObject itemSlot = Instantiate(itemSlotPrefab, itemSlotContainer);
            // ������ ���� ����
            itemSlot.GetComponent<ItemSlot>().SetItem(item);
        }
    }
}
