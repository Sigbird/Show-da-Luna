using UnityEngine;
using System.Collections;

public class YupiTimeSliders : MonoBehaviour {

	private const float SPEED = 2f;
	private const float MINDIST = 0.05f;
    private const float AUTO_SLIDE_TIMER = 6.0f;
    private const float MANUAL_SLIDE_TIMER = 12.0f;


    public int actualPos;
	

	public GameObject sliderButtonPrevious;
	public GameObject sliderButtonNext;

    private RectTransform rectTransform;
    private float timeToSlide;
    private bool force;
    private bool changed;

    private void SetPos(int newPos)
    {
 
        Vector2 anch = rectTransform.anchorMin;
        anch.x = -newPos;
        rectTransform.anchorMin = anch;
        anch = rectTransform.anchorMax;
        anch.x = 1 - newPos;
        rectTransform.anchorMax = anch;

        actualPos = newPos;

        force = false;
    }

	private void SlideToPos()
	{
       
        if (rectTransform.anchorMin.x != -actualPos)
        {

            float diff = rectTransform.anchorMin.x + actualPos;

            if ((Mathf.Abs(diff) < MINDIST) || force)
            {
                SetPos(actualPos);
            }
            else
            {
                int direction = rectTransform.anchorMin.x > -actualPos ? -1 : 1;
                Vector2 anch = rectTransform.anchorMin;
                anch.x += SPEED * Time.deltaTime * direction;
                rectTransform.anchorMin = anch;
                anch = rectTransform.anchorMax;
                anch.x += SPEED * Time.deltaTime * direction;
                rectTransform.anchorMax = anch;
            }

        }
        

	}

	private int GetCurrentPos()
	{
		int index = Mathf.Abs( (int) Mathf.Floor( rectTransform.anchorMin.x + 0.5f) );
		index = Mathf.Max (0, Mathf.Min (2, index));
		return index;
	}

	public void SliderPrevious()
	{
        timeToSlide = MANUAL_SLIDE_TIMER;
		SetSliderPos (actualPos - 1);
	}

	public void SliderNext()
	{
        timeToSlide = MANUAL_SLIDE_TIMER;
        SetSliderPos (actualPos + 1);
	}

	public void SetSliderPos(int index)
	{
		actualPos = index;

        if (actualPos <= -1)
        {
            SetPos(3);
            actualPos = 2;
        }

        if (actualPos > 2)
        {
            SetPos(-1);
            actualPos = 0;
        }

	}

	void OnDisable() 
	{
        SetPos ( 0 );        
    }

	// Use this for initialization
	void Start () {
		rectTransform = (RectTransform)transform;
        timeToSlide = AUTO_SLIDE_TIMER;
        SetPos(0);
    }
	
	// Update is called once per frame
	void Update () {

        timeToSlide -= Time.deltaTime;

        if (timeToSlide <= 0.0f)
        {
            timeToSlide = AUTO_SLIDE_TIMER;
            SetSliderPos(actualPos + 1);
        }

		SlideToPos();
	
	}
}
