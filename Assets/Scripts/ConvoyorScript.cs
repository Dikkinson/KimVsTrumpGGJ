using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvoyorScript : MonoBehaviour
{
    public Transform parent;
    public List<GameObject> ItemsList;
    void Start()
    {
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(5);
        }
    }
    public void Spawn()
    {
        int rnd = Random.Range(0, ItemsList.Count);
        var ItemOnConveyor = Instantiate(ItemsList[rnd], parent);
        ItemOnConveyor.SetActive(true);
        Destroy(ItemOnConveyor, 15f);
    }
}
