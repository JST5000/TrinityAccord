using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveToBump : MonoBehaviour
{ 
    Transform Mover;
    Transform Target;

    Vector3 originalPosition;

    Vector3 BumpVelocity;
    float TimeSpent;
    float TotalDuration;

    public bool bumping = false;
    private Vector3 PreviousVelocity;

    // Update is called once per frame
    void Update()
    {
        if(bumping)
        {
            LerpMove();
        }
    }

    public void StartBump(Transform mover, Transform target, float totalDuration, bool useYDirectionOnly) 
    {
        this.Mover = mover;
        this.Target = target;
        this.TimeSpent = 0;
        this.TotalDuration = totalDuration;

        this.originalPosition = mover.position;

        this.BumpVelocity = GetBumpVelocity(mover, target, totalDuration, useYDirectionOnly);

        this.bumping = true;
    }

    bool towardsTarget = true;

    //Returns the current velocity
    public void Move()
    {
        float TimeForEachSection = TotalDuration / 3;
        if (TimeSpent > TotalDuration)
        {
            Mover.position = originalPosition;
            bumping = false;
        }
        else if (TimeSpent > TimeForEachSection)
        {
            if(towardsTarget)
            {
                PreviousVelocity = new Vector3(0, 0, 0);
            }
            Mover.position = Vector3.SmoothDamp(Mover.position, originalPosition, ref PreviousVelocity, TimeForEachSection);
            towardsTarget = false;
        }
        else if (TimeSpent <= TimeForEachSection)
        {
            Mover.position = Vector3.SmoothDamp(Mover.position, new Vector3(Mover.position.x, Target.position.y), ref PreviousVelocity, TimeForEachSection);
            towardsTarget = true;
        }
        TimeSpent += Time.deltaTime;
        
    }

    public void LerpMove()
    {
        float TimeForEachSection = TotalDuration / 2;
        float t = (TimeSpent % TimeForEachSection) / TimeForEachSection;
        //Exponential
        t = Mathf.Sin(t * Mathf.PI * 0.5f); ;
        //"Smooth Step"
        //t = t * t * (3f - 2f * t);
        //"Smoother Step" - https://chicounity3d.wordpress.com/2014/05/23/how-to-lerp-like-a-pro/
        //t = t * t * t * (t * (6f * t - 15f) + 10f);

        Rect screenSpaceScaled = RectTransformToScreenSpace((RectTransform)Mover);
        
        if (TimeSpent > TotalDuration)
        {
            Mover.position = originalPosition;
            bumping = false;
        }
        else if (TimeSpent > TimeForEachSection)
        {
            Mover.position = Vector3.Lerp(new Vector3(originalPosition.x, Target.position.y + screenSpaceScaled.height / 2), originalPosition, t);
            towardsTarget = false;
        }
        else if (TimeSpent <= TimeForEachSection)
        {

            Mover.position = Vector3.Lerp(originalPosition, new Vector3(originalPosition.x, Target.position.y + screenSpaceScaled.height / 2), t);
            towardsTarget = true;
        }
        TimeSpent += Time.deltaTime;
    }

    public static Rect RectTransformToScreenSpace(RectTransform transform)
    {
        Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
        Rect rect = new Rect(transform.position.x, Screen.height - transform.position.y, size.x, size.y);
        rect.x -= (transform.pivot.x * size.x);
        rect.y -= ((1.0f - transform.pivot.y) * size.y);
        return rect;
    }

    public Vector3 GetBumpVelocity(Transform mover, Transform target, float totalDuration, bool useYDirectionOnly)
    {
        float edgeOfMoverX = mover.position.x;// - ((RectTransform)mover).rect.width / 2;
        float edgeOfMoverY = mover.position.y;// - ((RectTransform)mover).rect.height / 2;

        float displacementX = target.position.x - edgeOfMoverX;
        float displacementY = target.position.y - edgeOfMoverY;

        float velocityX = (displacementX / (totalDuration)) / 2;
        float velocityY = (displacementY / (totalDuration)) / 2;

        if(useYDirectionOnly)
        {
            velocityX = 0;
        }

        return new Vector3(velocityX, velocityY);
    }

    public bool IsDoneBumping()
    {
        return bumping;
    }
}
