
public static class PersistentData
{
    public delegate void ShipDestroyedHandler(int shipsDestroyed);
    public static event ShipDestroyedHandler onShipDestroyedEvent;

    public static int shipsDestroyed { get; private set; } = 0;
    public static float gameTime { get; set; } = 60f;
    public static float gameSpawnTime { get; set; } = 8f;

    public static void DestroyedShip()
    {
        shipsDestroyed++;
        onShipDestroyedEvent?.Invoke(shipsDestroyed);
    }
    
    public static void ResetShipCount()
    {
        shipsDestroyed = 0;
        onShipDestroyedEvent?.Invoke(shipsDestroyed);
    }
}
