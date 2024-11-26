using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Tiles/Custom Rule Tile")]
public class CustomRuleTile : RuleTile
{
    // Define the tiles that should be treated as "self"
    public TileBase[] specialTiles;

    public override bool RuleMatch(int neighbor, TileBase other)
    {
        // Treat all "specialTiles" as if they match the Rule Tile itself
        if (specialTiles != null && specialTiles.Length > 0 && other != null)
        {
            foreach (var specialTile in specialTiles)
            {
                if (other == specialTile)
                {
                    return true; // Matches "self"
                }
            }
        }

        // Default behavior for other cases
        return base.RuleMatch(neighbor, other);
    }
}
