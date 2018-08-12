using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Models;
using UnityEngine;
using Views;

public class FarmGrid : MonoBehaviour
{
    public Transform GroundTile1;
    public Transform GroundTile2;
    public Transform FencePost;
    public Transform FenceSlats;

    public BoxCollider FenceCollider;

    public Vector2Int GridSize;
    public Vector2 GridScale;

    private bool[,] _fencePosts;

    private FenceView[,] _xFences;
    private FenceView[,] _zFences;

    [UsedImplicitly]
    private void Start()
    {
        if (GridSize.x <= 0 || GridSize.y <=0)
            return;

        _xFences = new FenceView[GridSize.x+1,GridSize.y];
        _zFences = new FenceView[GridSize.x,GridSize.y+1];

        for (var y = 0; y < GridSize.y; ++y)
        {
            for (var x = 0; x < GridSize.x; ++x)
            {
                InstantiateOnGrid((x+y)%2 > 0 ? GroundTile1 : GroundTile2, "GroundTile", x, y);
            }
        }

        /*
        for (var z = 0; z <= GridSize.y; ++z)
        {
            for (var x = 0; x <= GridSize.x; ++x)
            {
                if (z < GridSize.y)
                {
                    var col = InstantiateOnGrid(FenceCollider, "FenceColliderX", x, z);
                    col.transform.Rotate(Vector3.up, -90);

                    _xFences[x, z] = new FenceView
                    {
                        ViewModel = new Fence
                        {
                            Built = false,
                            Collider = col
                        }
                    };
                }
                if (x < GridSize.x)
                {
                    var col = InstantiateOnGrid(FenceCollider, "FenceColliderZ", x, z);

                    _zFences[x, z] = new FenceView
                    {
                        ViewModel = new Fence
                        {
                            Built = false,
                            Collider = col
                        }
                    };
                }
            }
        }*/

        // TEMP
        _fencePosts = new bool[GridSize.x + 1, GridSize.y + 1];
        _fencePosts[1, 1] = true;
        _fencePosts[1, 2] = true;
        _fencePosts[1, 3] = true;
        _fencePosts[2, 1] = true;
        _fencePosts[2, 3] = true;
        _fencePosts[3, 1] = true;
        _fencePosts[3, 2] = true;
        _fencePosts[3, 3] = true;
        

        for (var y = 0; y <= GridSize.y; ++y)
        {
            for (var x = 0; x <= GridSize.x; ++x)
            {
                if (_fencePosts[x, y])
                {
                    InstantiateOnGrid(FencePost, "FencePost", x, y);

                    if (x < GridSize.x && _fencePosts[x + 1, y])
                    {
                        InstantiateOnGrid(FenceSlats, "FenceSlats", x, y);
                    }
                    if (y < GridSize.y && _fencePosts[x, y + 1])
                    {
                        var slats = InstantiateOnGrid(FenceSlats, "FenceSlats", x, y);
                        slats.Rotate(Vector3.up, -90);
                    }
                }
            }
        }
    }

    private T InstantiateOnGrid<T>(T prefab, string nam, int gridX, int gridY) where T : Component
    {
        T child = Instantiate(prefab, transform);
        child.name = $"{nam}_{gridX}_{gridX}";
        child.transform.localPosition = new Vector3(gridX * GridScale.x, 0, gridY * GridScale.y);
        return child;
    }
}
