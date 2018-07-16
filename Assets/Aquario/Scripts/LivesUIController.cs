using UnityEngine;

public class LivesUIController : MonoBehaviour {
    public GameObject LifePrefab;
    public Transform ActiveLives;
    public Transform InactiveLives;

    public void SetLives(int amount) {
        for (int i = 0; i < amount; i++) {
            GameObject life = Instantiate(LifePrefab);
            life.transform.SetParent(ActiveLives, false);
        }
    }

    public void RemoveLife() {
        if (ActiveLives.childCount > 0) {
            Transform child = ActiveLives.GetChild(0);

            if (child != null) {
                child.gameObject.SetActive(false);
                child.SetParent(InactiveLives, false);
            }
        }        
    }

    public void AddLife() {
        if (InactiveLives. childCount > 0) {
            Transform child = InactiveLives.GetChild(0);

            if (child != null) {
                child.gameObject.SetActive(true);
                child.SetParent(ActiveLives, false);
            }
        }        
    }

    public void ResetLives() {
        int count = InactiveLives.childCount;

        for (int i = 0; i < count; i++) {
            Transform life = InactiveLives.GetChild(0);
            life.SetParent(ActiveLives, false);
            life.gameObject.SetActive(true);
        }
    }
}
