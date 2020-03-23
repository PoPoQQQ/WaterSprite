using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerToPlant : MonoBehaviour
{
    Plant currentPlant;
    public float plantDistance = 2f;

    public Plant C_PLANT{get{return currentPlant;}}

    void Update()
    {
        checkMousePlant();
    }

    void checkMousePlant()
    {
        Vector2 mousePos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.zero);
        if (hits.Length == 0)
            return;
        currentPlant=null;
        foreach (var hit in hits)
        {
            currentPlant = hit.collider.gameObject.GetComponent<Plant>();
            if(currentPlant!=null)
            {
                if(checkDistance(hit.transform.position, transform.position))
                    return;
                else
                    currentPlant = null;
            }
        }
    }

    bool checkDistance(Vector2 pos1, Vector2 pos2)
    {
        return ((pos1-pos2).magnitude < plantDistance);
    }
}
