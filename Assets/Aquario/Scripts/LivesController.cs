using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Events;

public class LivesController : MonoBehaviour {
    public bool Invicible = false;
    public int MaxLives = 3;    
    public LivesUIController UIController;    
    public string[] EnemiesTags;
    public float InvincibleWindow = 3f;

    public UnityEvent OnGameOver;

    private int LivesCounter = 3;
    private bool IsInvicible = false;
    private Animator animator;

    // Use this for initialization
    void Start () {
        LivesCounter = MaxLives;
        UIController.SetLives(LivesCounter);
        animator = GetComponent<Animator>();
        //StartCoroutine(TestLives());
	}

    public void RemoveLife() {  

        LivesCounter -= 1;
        UIController.RemoveLife();
    }

	public void RemovePearls() {        
		LivesCounter -= 1;
		UIController.RemoveLife();
	}

    public void AddLife() {
        if (LivesCounter < MaxLives) {
            LivesCounter += 1;
            UIController.AddLife();
        }        
    }

    public void ResetLives() {
        LivesCounter = MaxLives;
        Debug.Log(LivesCounter);
    }

    public void OnCollisionEnter2D(Collision2D coll) {
        if (IsEnemy(coll.gameObject.tag) && !Invicible) {
            if (!IsInvicible) {
                RemoveLife();
                if (LivesCounter > 0) {
                    StartCoroutine(InvicibleTimer());
                } else {
                    OnGameOver.Invoke();
                }                
            }            
        }
      
    }

    public IEnumerator InvicibleTimer() {
        IsInvicible = true;
        animator.SetBool("Invicible", IsInvicible);

        yield return new WaitForSeconds(InvincibleWindow);
        IsInvicible = false;
        animator.SetBool("Invicible", IsInvicible);
    }

    bool IsEnemy(string tag) {
        if (Array.Exists(EnemiesTags, enemyTag => enemyTag == tag)) {
            return true;
        }

        return false;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public IEnumerator TestLives() {
        while (true) {
            for (int i = 0; i < LivesCounter; i++) {
                yield return new WaitForSeconds(1);
                UIController.RemoveLife();
            }
            for (int i = 0; i < LivesCounter; i++) {
                yield return new WaitForSeconds(1);
                UIController.AddLife();
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
