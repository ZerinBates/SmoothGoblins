using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTile :Tile
{
    [SerializeField]
    private Color _offsetColor;
    // Start is called before the first frame update
    public override void Init(int x, int y)
    {
        this.x = x;
        this.y = y;
        var isOffset = (x + y) % 2 == 1;
        _renderer.color = isOffset ? _offsetColor : _baseColor;
        Pnode = new PathNode(x, y, walkable);
    }
}
