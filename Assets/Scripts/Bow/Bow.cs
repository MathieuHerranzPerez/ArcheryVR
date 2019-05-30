using UnityEngine;

public class Bow : MonoBehaviour
{
    private int nbCollide = 0;
    private BowPart lastBowPart;


    public void NotifyHitCollider(BowPart bowPart)
    {
        lastBowPart = bowPart;
        Debug.Log("collide : " + nbCollide);
        ++nbCollide;
        if(nbCollide >= 2)
        {
            Debug.Log("Attach");
            //if(bowPart.AttachArrow())
                //nbCollide = 0;
        }
    }

    public void TryToAttach()
    {
        if (nbCollide >= 2)
        {
            lastBowPart.AttachArrow();
        }
    }

    public void NotifyQuitCollide()
    {
        --nbCollide;
    }
}
