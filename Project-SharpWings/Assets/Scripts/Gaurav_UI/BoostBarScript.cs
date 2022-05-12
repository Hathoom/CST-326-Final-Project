using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostBarScript : MonoBehaviour
{
    public Slider boostBar;

    private float maxBoostValue = 6;
    private float currentBoostValue;

    // Start is called before the first frame update
    void Start()
    {
        currentBoostValue = maxBoostValue;
        boostBar.maxValue = maxBoostValue;
        boostBar.value = maxBoostValue;
    }

    public void useStamina(float amount)
    {
        if (currentBoostValue - amount >= 0)
        {
            currentBoostValue -= amount;
            boostBar.value = currentBoostValue;

            
            StartCoroutine(RegenBoostBar());
        }
        else
        {
            Debug.Log("Boost over");
        }
    }

    private IEnumerator RegenBoostBar()
    {
        yield return new WaitForSeconds(2);

        while (currentBoostValue < maxBoostValue)
        {
            currentBoostValue += 0.02f;
            boostBar.value = currentBoostValue;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
