using UnityEngine;

public class FizzBuzz : MonoBehaviour
{
    static void FizzOrBuzz()
    {
        for (int i = 1; i <= 100; i++)
        {
            if (i % 3 == 0 && i % 5 == 0)
                Debug.Log("FizzBuzz");
            else if (i % 3 == 0)
                Debug.Log("Fizz");
            else if (i % 5 == 0)
                Debug.Log("Buzz");
            else
                Debug.Log(i);
        }
    }
}