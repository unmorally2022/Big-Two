using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectCharacterManager : MonoBehaviour
{
    public AnimationBehavior[] characters;
    public Toggle[] toggles;
    public RawImage[] rawImageCharacters;


    // Start is called before the first frame update
    void Start()
    {
        AppManager.avatars = characters;
        AppManager.avatarTexture = new RenderTexture[rawImageCharacters.Length];
        for(int i=0;i<rawImageCharacters.Length;i++)
            AppManager.avatarTexture[i] = rawImageCharacters[i].texture;



        AppManager.PlayerSelectedAvatar = 0;
    }

    public void GUI_ToggleOnValueChange() {
        AppManager.audioManager.PlayOnce(0);
    }

    public void GUI_button1() {
        for (int i = 0; i < characters.Length;i++) {
            //characters[i].transform.GetChild(1).GetComponent<AnimationBehavior>().SetAnimation(MonsterState.loose);
            characters[i].SetAnimation(MonsterState.loose);
        }
    }
    
    public void GUI_button2()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            //characters[i].transform.GetChild(1).GetComponent<AnimationBehavior>().SetAnimation(MonsterState.win);
            characters[i].SetAnimation(MonsterState.win);
        }
    }

    public void GUI_button3()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            //characters[i].transform.GetChild(1).GetComponent<AnimationBehavior>().SetAnimation(MonsterState.iddle);
            characters[i].SetAnimation(MonsterState.iddle);
        }
    }

    public void GUI_StartGame() {
        AppManager.audioManager.PlayOnce(0);

        for (int i = 0; i < toggles.Length; i++) {
            if (toggles[i].isOn)
            {
                AppManager.PlayerSelectedAvatar = i;
                break;
            }
        }
        SceneManager.LoadScene("GamePlay");
    }
}
