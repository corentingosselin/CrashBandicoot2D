using Environment;
using UnityEngine;

public class NormalBox : Box
{

    [SerializeField] GameObject wumpa;

    public override void Init()
    {
        base.Init();
        boxName = "normal";
    }
    
    public override void OnJumpingBox(GameObject player)
    {
        base.OnJumpingBox(player);
        Break();
    }

    public override void Break()
    {
        base.Break();
        int number = Random.Range(1,6);
      
        for (int i = 0; i < number; i++)
        {
            GameObject fruit = Instantiate(wumpa, new Vector3(transform.position.x , transform.position.y , transform.position.z), Quaternion.identity);
            fruit.gameObject.GetComponent<WumpaFruit>().Drop();
        }
    }
    
}
