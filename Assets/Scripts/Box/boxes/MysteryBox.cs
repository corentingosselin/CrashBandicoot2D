using Environment;
using UnityEngine;

public class MysteryBox : Box
{
    
    [SerializeField] GameObject wumpa;
    [SerializeField] GameObject life;
    
    public override void Init()
    {
        base.Init();
        boxName = "mystery";
    }
    
    public override void OnJumpingBox(GameObject player)
    {
        base.OnJumpingBox(player);
        Break();
    }

    public override void Break()
    {
        base.Break();
        
        
        //fruit or life ?
        int r = Random.Range(0,100);
        if (r <= 10)
        {
            Instantiate(life, new Vector3(transform.position.x , transform.position.y , transform.position.z), Quaternion.identity);
        }
        else
        {
            int number = Random.Range(1, 6);
            for (int i = 0; i < number; i++)
            {
                GameObject fruit = Instantiate(wumpa,
                    new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                fruit.gameObject.GetComponent<WumpaFruit>().Drop();
            }
        }
    }
 
}
