using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupManager : MonoBehaviour {

	private IEnumerator Start ()
    {
		while(!LocalizationManager.instance.GetIsReady())
        {
            yield return null;
        }
        SceneManager.LoadScene("Game scene");
	}

}
