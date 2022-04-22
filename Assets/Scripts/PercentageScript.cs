using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PercentageScript : MonoBehaviour
{
    public Image fillImage;
    public TextMeshProUGUI percentageText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        percentageText.text = (fillImage.fillAmount*100f).ToString("0")+"%";
    }
}
