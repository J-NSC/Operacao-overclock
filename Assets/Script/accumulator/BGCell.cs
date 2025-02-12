using UnityEngine;

public class BGCell : MonoBehaviour
{
    [HideInInspector] public bool IsBlocked;
    [HideInInspector] public bool IsFilled;

    [SerializeField] SpriteRenderer bgSprite;
    [SerializeField] Sprite emptySprite;
    [SerializeField] Sprite blockedSprite;
    [SerializeField] Color startColor;
    [SerializeField] Color endColor;
    [SerializeField] Color correctColor;
    [SerializeField] Color incorrectColor;

    public void Init(int blockValue)
    {
        IsBlocked = blockValue == -1;
        
        if(IsBlocked)
        {
            IsFilled = true;
        }
        
        bgSprite.sprite = IsBlocked ? blockedSprite : emptySprite;
    }


    public void ResetHighLight()
    {
        bgSprite.color = startColor;
    }

    public void UpdateHighLight(bool isCorrect)
    {
        bgSprite.color = isCorrect ? correctColor : incorrectColor;
    }
    
}
