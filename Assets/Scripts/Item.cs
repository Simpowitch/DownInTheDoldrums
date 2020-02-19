using System.Collections.Generic;

[System.Serializable]
public class Item 
{
    public string itemName;
    public List<Effect> effects = new List<Effect>();
}
