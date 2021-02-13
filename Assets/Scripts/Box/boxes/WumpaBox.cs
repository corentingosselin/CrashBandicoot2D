using Environment;
using UnityEngine;

public class WumpaBox : Box
{
    private int randomAmount;
    [SerializeField] private GameObject wumpaFruit;
    
    
    public override void Init()
    {
        base.Init();
        boxName = "wumpa";
        randomAmount = Random.Range(5, 10);

    }
    
    public override void Break()
    {
        base.Break();
    }

    public override void OnJumpingBox(GameObject player)
    {
        base.OnJumpingBox(player);
        animator.Play(boxName + "bounce");
        //Pop fruit
        GameObject fruit = Instantiate(wumpaFruit, 
            new Vector3(transform.position.x , transform.position.y + 0.1F , transform.position.z), Quaternion.identity);
        fruit.gameObject.GetComponent<WumpaFruit>().Drop();
        randomAmount--;
        if (randomAmount <= 0)
        {
            Break();
        }
        
    }
    
}
