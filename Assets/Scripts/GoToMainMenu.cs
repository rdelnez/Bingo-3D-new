using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoToMainMenu : MonoBehaviour {

	public void MainMenu () {
        //Application.LoadLevel ("Main Menu");
        SceneManager.LoadScene(0);
	}
}
