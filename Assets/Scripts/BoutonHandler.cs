using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutonHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public void restart()
    {
        ScreenManager.restart();
    }

    // Update is called once per frame
    public void quit()
    {
        ScreenManager.quit();
    }
}
