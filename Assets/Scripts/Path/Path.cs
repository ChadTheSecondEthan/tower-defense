using System.Collections.Generic;

public class Path
{
    public Tile Cur { get; private set; }
    readonly List<Tile> prev;

    public Path(Tile start)
    {
        Cur = start;
        prev = new List<Tile>();
    }

    public void Next()
    {
        foreach (Tile tile in Grid.Instance.GetNeighbours(Cur))
        {
            if ((tile.type == Tile.TileType.Path || tile.type == Tile.TileType.Base) && !prev.Contains(tile))
            {
                prev.Add(Cur);
                Cur = tile;
                return;
            }
        }
        prev.Add(Cur);
        Cur = null;
    }
}