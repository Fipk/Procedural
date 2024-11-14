using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    private Item currentItem;

    public Image cursor;

    public Slot[] craftingSlots;

    public List<Item> itemList;
    public string[] recipes;
    public Item[] recipeResults;
    public Slot ResultSlot;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("test");
            if (currentItem != null)
            {
                cursor.gameObject.SetActive(true);
                Slot nearestSlot = null;
                float shortestDistance = float.MaxValue;

                foreach (Slot slot in craftingSlots)
                {
                    float dist = Vector2.Distance(Input.mousePosition, slot.transform.position);
                    if (dist < shortestDistance)
                    {
                        shortestDistance = dist;
                        nearestSlot = slot;
                    }
                }
                nearestSlot.gameObject.SetActive(true);
                nearestSlot.GetComponent<Image>().sprite = currentItem.GetComponent<Image>().sprite;
                nearestSlot.item = currentItem;

                itemList[nearestSlot.slotIndex] = currentItem;
                currentItem = null;

                CheckForCreatedRecipes();
            }
        }
    }

    void CheckForCreatedRecipes()
    {
        ResultSlot.gameObject.SetActive(false);
        ResultSlot.item = null;

        string currentRecipeString = "";
        foreach(Item item in itemList)
        {
            if (item != null)
            {
                currentRecipeString += item.Name;
            } else
            {
                currentRecipeString += "null";
            }
        }

        for (int i = 0; i < recipes.Length; i++)
        {
            if (recipes[i] == currentRecipeString)
            {
                ResultSlot.gameObject.SetActive(true);
                ResultSlot.GetComponent<Image>().sprite = recipeResults[i].GetComponent<Image>().sprite;
                ResultSlot.item = recipeResults[i];
            }
        }
    }

    public void OnClickSlot(Slot slot)
    {
        slot.item = null;
        itemList[slot.slotIndex] = null;
        slot.gameObject.SetActive(false);
        CheckForCreatedRecipes();
    }
    public void OuMouseDownItem(Item item)
    {
        if (currentItem == null)
        {

            currentItem = item;
            cursor.gameObject.SetActive(true);
            cursor.sprite = currentItem.GetComponent<Image>().sprite;

        }
    }
}
