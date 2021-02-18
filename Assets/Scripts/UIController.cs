using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text text;
    public Slider slider;
    public GameObject innerReticule;

    void Update()
    {
        float value = PickUpCells.currentPower;
        float maxValue = PickUpCells.maxPower;

        text.text = $"{value}/{maxValue}";
        slider.value = value / maxValue;

        innerReticule.SetActive(PickUpCells.targeted);
    }

}
