using UnityEngine;

public class CraftingTableManager : MonoBehaviour
{
    private CraftingVisualManager manager;
    [SerializeField] private Grabber grabber;

    void Awake()
    {
        grabber = gameObject.GetComponent<Grabber>();
        grabber.Setup(this);
    }

    public void SetUp(CraftingVisualManager _manager)
    {
        manager = _manager;
    }


    public void RemoveItemFromInventory(ItemDataSO itemData)
    {
        manager.RemoveItemFromInventory(itemData);
    }
    #region CraftingGear

    #endregion

    #region Grabber
    public GameObject GrabCraftingGear(GameObject hitObject)
    {
        return hitObject.GetComponent<CraftingGear>().OnGrab();
    }

    public bool LongGrabCraftingGear(GameObject hitObject)
    {
        return hitObject.GetComponent<CraftingGear>().OnLongGrab();
    }

    public void UseGear(GameObject gearObject)
    {
        gearObject.GetComponent<CraftingGear>().OnUse();
    }

    public void PlaceGearDown(GameObject gear, Collider collider)
    {
        gear.GetComponent<CraftingGear>().OnPlaceDown(collider.gameObject);
    }

    public void PutGearBack(GameObject gear)
    {
        gear.GetComponent<CraftingGear>().OnPutBack();
    }

    public void GrabPestle(GameObject pestleObject)
    {
        pestleObject.GetComponent<Pestle>().OnGrab();
    }

    public bool CanMovePestle(GameObject pestleObject)
    {
        return pestleObject.GetComponent<Pestle>().CanMove();
    }

    public void LetPestleGo(GameObject pestle)
    {
        pestle.GetComponent<Pestle>().OnLetGo();
    }
    #endregion

}