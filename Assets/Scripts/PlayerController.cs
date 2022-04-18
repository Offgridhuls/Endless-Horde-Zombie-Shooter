using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public bool isFiring;
    public bool isReloading;
    public bool isJumping;
    public bool isRunning;
    public bool isAiming;
    public bool inInventory;
    // Start is called before the first frame update

    public InventoryComponent inventory;
    public GameUIController uiController;

    private void Awake()
    {
        if(inventory == null)
        {
            inventory = GetComponent<InventoryComponent>();

        }
        if(uiController == null)
        {
            uiController = FindObjectOfType<GameUIController>();
        }
    }
    public void OnInventory(InputValue value)
    {
        if (inInventory)
        {
            inInventory = false;
        }
        else
        {
            inInventory = true;
        }
        OpenInventory(inInventory);
    }
    private void OpenInventory(bool open)
    {
        if(open)
        {
            uiController.EnableInventoryMenu();

        }
        else
        {
            uiController.EnableGameMenu();
        }
    }
}
