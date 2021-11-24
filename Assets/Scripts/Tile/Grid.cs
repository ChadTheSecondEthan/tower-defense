using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static Grid Instance { get; private set; }
    public int SideLength { get; private set; }
    public Color[] tileColors;

    #region Tiles
    public Tile[] AllTiles { get; private set; }
    public Tile[] OpenTiles { get; private set; }
    public Tile[] ClosedTiles { get; private set; }
    public Tile[] PathTiles { get; private set; }
    public Tile[] SpawnTiles { get; private set; }
    public Tile HoveredTile { get; set; }
    #endregion

    void Awake() => Instance = this;

    public void LoadGridData(GridData gridData)
    {
        AllTiles = transform.GetComponentsInChildren<Tile>();

        SideLength = (int)Mathf.Sqrt(AllTiles.Length);
        for (int i = 0; i < AllTiles.Length; i++)
        {
            // starts at bottom left, goes up until the top and then
            // goes the the bottom of the row to the right
            AllTiles[i].x = i % SideLength;
            AllTiles[i].y = i / SideLength;

            AllTiles[i].type = gridData.tileTypes[i];
            AllTiles[i].GetComponent<SpriteRenderer>().color = tileColors[(int)gridData.tileTypes[i]];
        }

        List<Tile> openTileList = new List<Tile>();
        List<Tile> closedTileList = new List<Tile>();
        List<Tile> pathTilesList = new List<Tile>();
        List<Tile> spawnTilesList = new List<Tile>();

        foreach (Tile t in AllTiles)
        {
            if (t.type == Tile.TileType.Normal)
                openTileList.Add(t);
            else
            {
                closedTileList.Add(t);
                if (t.type == Tile.TileType.Path)
                    pathTilesList.Add(t);
                else if (t.type == Tile.TileType.Spawn)
                    spawnTilesList.Add(t);
            }
        }

        OpenTiles = openTileList.ToArray();
        ClosedTiles = closedTileList.ToArray();
        PathTiles = pathTilesList.ToArray();
        SpawnTiles = spawnTilesList.ToArray();
    }

    public Tile TileAt(int index) => AllTiles[index];
    public Tile TileAt(int x, int y) => AllTiles[y * SideLength + x];

    public Tile[] GetNeighbours(Tile tile)
    {
        int tx = tile.x;
        int ty = tile.y;
        List<Tile> neighbours = new List<Tile>();

        bool leftNeighbour = tx > 0;
        bool rightNeighbour = tx < SideLength - 1;
        bool topNeighbour = ty > 0;
        bool bottomNeighbour = ty < SideLength - 1;

        if (topNeighbour) neighbours.Add(TileAt(tx, ty - 1));
        if (bottomNeighbour) neighbours.Add(TileAt(tx, ty + 1));
        if (leftNeighbour) neighbours.Add(TileAt(tx - 1, ty));
        if (rightNeighbour) neighbours.Add(TileAt(tx + 1, ty));
        
        return neighbours.ToArray();
    }

    public void DestroyAllTowers()
    {
        foreach (Tower tower in transform.GetComponentsInChildren<Tower>())
            Destroy(tower.gameObject);
    }

    public void SaveGridData(GridData gridData) => gridData.tileTypes = 
        new List<Tile>(GetComponentsInChildren<Tile>()).ConvertAll(tile => tile.type).ToArray();
}