using UnityEngine;

public class Item : MonoBehaviour
{
    public Encounter encounter;
    // The tile that this item is occupying
    public Tile OccupiedTile;
    bool end = true;
    // The range to check for occupied units
    public int Range = 5;
    public void Activate()
    {
        if (encounter != null)
        {
            encounter.Activate();
        }
        Debug.Log("yay");
    }

    // Check if any of the tiles within the specified range have an occupied unit
    public bool CheckForOccupiedUnits()
    {
        // Loop through all the tiles within the specified range
        for (int x = OccupiedTile.x - Range; x <= OccupiedTile.x + Range; x++)
        {
            for (int y = OccupiedTile.y - Range; y <= OccupiedTile.y + Range; y++)
            {
                // Get the tile at the current coordinates
                Tile tile = GridManager.Instance.GetTileAtPosition(x, y);

                // If the tile is not null and has an occupied unit, return true
                if (tile != null && tile.OccupiedUnit != null &&tile.OccupiedUnit.Faction == Faction.Hero )
                {
                    Activate();
                    return true;
                }
            }
        }

        // If no occupied units were found, return false
        return false;
    }
    void Update()
    {
        if (end)
        {
            if (CheckForOccupiedUnits())
            {
                end = false;
            }
        }
    }
}
