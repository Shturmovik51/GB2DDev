using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShedItemsView
{
    private Transform _placeForShedItems;
    private ItemsRepository _itemsRepository;

    public ShedItemsView(Transform placeForShedItems, ItemsRepository itemsRepository)
    {
        _placeForShedItems = placeForShedItems;
        _itemsRepository = itemsRepository;
    }

    private void ViewItems()
    {

    }

}
