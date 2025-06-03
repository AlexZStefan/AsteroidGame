using UnityEngine;

public class EquipMisile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<ShipFire>().EquipSmartMisile();
            Destroy(gameObject);
        }
    }
}
