using UnityEngine;

// Landing point for player input intents pushed from PlayerVisualManager via LogicManager.
public class PlayerManager : SingletonManager<PlayerManager>
{
    private PlayerMovement playerMovement;
    private PlayerInventory playerInventory;

    protected override void Awake()
    {
        base.Awake();
        playerMovement = GetComponent<PlayerMovement>();
        playerInventory = GetComponent<PlayerInventory>();
    }

    public void SetMoveInput(Vector2 moveInput) => playerMovement.SetMoveInput(moveInput);
    public void SetSprintEnabled(bool enabled) => playerMovement.SetSprintEnabled(enabled);
    public void TryJump() => playerMovement.TryJump();
    public void SetMovementEnabled(bool newValue) => playerMovement.SetMovementEnabled(newValue);
    public InventorySystem GetInventorySystem() => playerInventory;
}
