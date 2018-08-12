using System.Collections;
using System.Collections.Generic;
using Models;
using MVVM;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class TestStuff : MonoBehaviour
{
    public Camera Cam;
    public GameObject TileGhost;

    private GameObject _tileGhost;
    void Start()
    {
        _tileGhost = Instantiate(TileGhost);
        _tileGhost.SetActive(false);
    }

	// Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                var vm = hit.collider.gameObject.GetDataContext()?.Value as Fence;
                if (vm != null)
                {
                    if (Input.GetMouseButtonUp(0))
                    {
                        vm.IsBuilt = true;
                    }
                    else
                    {
                        _tileGhost.SetActive(true);
                        _tileGhost.transform.position = vm.Position;
                        _tileGhost.transform.rotation = vm.IsVertical ? Quaternion.AngleAxis(-90, Vector3.up) : Quaternion.identity;
                    }
                }
            }
        }
        else
        {
            _tileGhost.SetActive(false);
        }
    }
}
