using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour {

    [SerializeField]
    private RectTransform healthBar;

    public void SetHealth(int cur, int max)
    {
        float val = (float)cur / max;

        healthBar.localScale = new Vector3(val, healthBar.localScale.y, healthBar.localScale.z);
        if(cur <= 30)
        {
            healthBar.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
        }
        else
        {
            healthBar.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
        }
    }
}
