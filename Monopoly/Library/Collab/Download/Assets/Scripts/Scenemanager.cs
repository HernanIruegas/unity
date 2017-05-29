using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scenemanager : MonoBehaviour {

    public Transform panelSalir, panelOriginal;

    public void LoadScene(int name){
        SceneManager.LoadScene(name);
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void SalirJuego(bool clicked){
        if (clicked == true) //salir
        {
            panelSalir.gameObject.SetActive(true);
        }
        else //cancelar
        {
            panelSalir.gameObject.SetActive(false);

        }
    }

    public void CambiarPanel(bool clicked){
        if(clicked == true){
            panelSalir.gameObject.SetActive(true);
            panelOriginal.gameObject.SetActive(false);
        }else{
            panelSalir.gameObject.SetActive(false);
            panelOriginal.gameObject.SetActive(true);
        }
    }
    /*public Image Dado;
    public void BotonMamalon(){
        int num = Random.Range(1,7);
        Dado.sprite = Resources.Load<Sprite> ("dado" + num);
    }*/
}
