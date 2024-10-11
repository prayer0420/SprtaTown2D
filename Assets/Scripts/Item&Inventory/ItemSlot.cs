using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Image icon;
    public ItemSO item;

    public void SetItem(ItemSO newItem)
    {
        item = newItem;
        icon.sprite = item.itemIcon;
    }

    public void OnItemClicked()
    {
        // ������ ��� �Ǵ� ��� ���� ����
    }
}
