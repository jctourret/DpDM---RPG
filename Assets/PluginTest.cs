
using UnityEngine;
using TMPro;

public class PluginTest : MonoBehaviour
{
    public AndroidJavaObject javaClass;
    public TextMeshProUGUI text;
    int counter = 0;

    private void Start()
    {
        javaClass = new AndroidJavaObject("com.cachuflitogamesforever.mylibrary.PluginClass");
    }


    public void AddFive()
    {
        int number = javaClass.Call<int>("AddFiveToInteger", counter);
        text.text = "Counter: " + number;
    }
}
