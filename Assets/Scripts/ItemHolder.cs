using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public Item item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<CharacterData>().AddItem(item);
            Destroy(this.gameObject);
        }
    }
}
