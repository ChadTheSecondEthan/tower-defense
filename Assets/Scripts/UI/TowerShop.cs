public class TowerShop : MenuTab
{
    public static TowerShop Instance { get; private set; }

    void Awake() => Instance = this;
}
