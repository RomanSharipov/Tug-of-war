using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private Star[] _stars;
    [SerializeField] private float _delayBetweenSpawnStar;
    [SerializeField] private float _delayBeforeSpawnStar;
    

    public void ShowStars(int countStars)
    {
        StartCoroutine(ShowStarsWithDelay(countStars, _delayBetweenSpawnStar, _delayBeforeSpawnStar));
    }

    private IEnumerator ShowStarsWithDelay(int countStars, float delayBetweenSpawnStar, float delayBeforeSpawnStar)
    {
        WaitForSeconds waitDelayBetweenSpawnStar = new WaitForSeconds(delayBetweenSpawnStar);
        WaitForSeconds waitDelayBeforeSpawnStar = new WaitForSeconds(delayBeforeSpawnStar);
        yield return waitDelayBeforeSpawnStar;
        for (int i = 0; i < countStars; i++)
        {
            _stars[i].gameObject.SetActive(true);
            yield return waitDelayBetweenSpawnStar;
        }
    }
}
