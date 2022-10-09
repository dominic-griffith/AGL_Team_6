using System.Collections;
using UnityEngine;

public class SimpleDeleteBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeleteOnTimer());
    }

    private IEnumerator DeleteOnTimer()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.transform.CompareTag("Player")) return;
        Destroy(gameObject);
    }
}
