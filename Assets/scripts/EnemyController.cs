using UnityEngine;

public class EnemyController : PlayerController
{
    public new void GetDamage(int damage)
    {
        currentHelth -= damage;
        for (int i = 1; i < bar; i--)
        {
            {
                bar = bar + 0.1f/ currentHelth;
                heathBar.rectTransform.anchorMin = new Vector3(bar, 0.5f);
            }

           healthCountTxt.text = currentHelth.ToString();
        }
    }
    
}
