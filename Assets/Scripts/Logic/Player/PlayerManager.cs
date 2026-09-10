using UnityEngine;

// Logic-side per-system manager for the Player. Landing point for player
// input intents pushed from PlayerVisualManager via VisualManager -> LogicManager.
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    private PlayerMovement playerMovement;
    private PlayerInventory playerInventory;

    void Awake()
    {
        Instance = this;
        playerMovement = GetComponent<PlayerMovement>();
        playerInventory = GetComponent<PlayerInventory>();
    }

    public void SetMoveInput(Vector2 moveInput) => playerMovement.SetMoveInput(moveInput);
    public void SetSprintEnabled(bool enabled) => playerMovement.SetSprintEnabled(enabled);
    public void TryJump() => playerMovement.TryJump();
    public void SetMovementEnabled(bool newValue) => playerMovement.SetMovementEnabled(newValue);
    public InventorySystem GetInventorySystem() => playerInventory;
}
