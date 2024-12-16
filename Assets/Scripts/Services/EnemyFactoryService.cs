using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyFactoryService : AbstractFactoryService
{
    protected override void OnItemCreated(GameObject item)
    {
        base.OnItemCreated(item);
        item.GetComponent<IHealth>().Reset();
    }
}
