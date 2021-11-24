using UnityEngine;

public class Tile : MonoBehaviour
{
    SpriteRenderer sr;

    public TileType type;
    public int x, y;

    public enum TileType { Normal, Spawn, Disabled, Base, Path }

    public bool Occupied { get; private set; }

    [HideInInspector] public Tower tower;

    #region Unity Methods
    public virtual void Start() => sr = GetComponent<SpriteRenderer>();

    void OnMouseEnter()
    {
        if (type == TileType.Normal)
            Grid.Instance.HoveredTile = this;
    }

    void OnMouseExit()
    {
        if (type == TileType.Normal && Grid.Instance.HoveredTile == this)
            Grid.Instance.HoveredTile = null;
    }

    void OnMouseDown()
    {
        if (type != TileType.Normal) return;

        // remove tower if control is pressed
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            DeleteTower();

        // open store if not occupied
        //else if (!Occupied)
        //    TowerShop.Instance.Open();
    }

    /*void OnMouseOver()
    {
        if (type != TileType.Normal) return;

        bool control = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
        sr.color = control && tower ? Color.red : Color.green;
    }*/
    #endregion

    public void DeleteTower()
    {
        Occupied = false;
        Destroy(tower.gameObject);
        tower = null;
    }

    public bool SetTower(Tower prefab)
    {
        Occupied = true;
        if (tower != null)
            return false;

        tower = Instantiate(prefab, transform);
        tower.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);

        return true;
    }
}
