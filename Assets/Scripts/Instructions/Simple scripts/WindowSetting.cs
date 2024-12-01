using UnityEngine;

public class WindowSetting : MonoBehaviour
{
  public GameObject Selected1;
  public GameObject Selected2;



  void Start()
  {
    if (PlayerPrefs.GetInt("Windowed") == 1)
    {
      Selected1.SetActive(true);
      Selected2.SetActive(false);
    }
    else
    {
      Selected1.SetActive(false);
      Selected2.SetActive(true);
    }
  }

  public void ScaleTo(int num)
  {
    switch (num)
    {
      case 0: ScaleTo0(); break;
      case 1: ScaleTo1(); break;
      case 2: ScaleTo2(); break;
      case 3: ScaleTo3(); break;
      case 4: ScaleTo4(); break;
      case 5: ScaleTo5(); break;
      case 6: ScaleTo6(); break;
    }
  }

  void ScaleTo0()
  {
    bool windowed = PlayerPrefs.GetInt("Windowed") == 0;

    PlayerPrefs.SetFloat("Window", 2f);
    Screen.SetResolution(960, 540, windowed);
  }
  void ScaleTo1()
  {
    bool windowed = PlayerPrefs.GetInt("Windowed") == 0;

    PlayerPrefs.SetFloat("Window", 1f);
    Screen.SetResolution(1280, 720, windowed);
  }
  void ScaleTo2()
  {
    bool windowed = PlayerPrefs.GetInt("Windowed") == 0;

    PlayerPrefs.SetFloat("Window", 0f);
    Screen.SetResolution(1920, 1080, windowed);
  }
  void ScaleTo3()
  {
    bool windowed = PlayerPrefs.GetInt("Windowed") == 0;

    PlayerPrefs.SetFloat("Window", 3f);
    Screen.SetResolution(400, 225, windowed);
  }
  void ScaleTo4()
  {
    bool windowed = PlayerPrefs.GetInt("Windowed") == 0;

    PlayerPrefs.SetFloat("Window", 4f);
    Screen.SetResolution(640, 360, windowed);
  }
  void ScaleTo5()
  {
    bool windowed = PlayerPrefs.GetInt("Windowed") == 0;

    PlayerPrefs.SetFloat("Window", 5f);
    Screen.SetResolution(800, 450, windowed);
  }
  void ScaleTo6()
  {
    bool windowed = PlayerPrefs.GetInt("Windowed") == 0;

    PlayerPrefs.SetFloat("Window", 6f);
    Screen.SetResolution(1024, 576, windowed);
  }

  public void fullScreen()
  {
    PlayerPrefs.SetInt("Windowed", 0);
    Screen.fullScreen = true;
  }
  public void windowed()
  {
    PlayerPrefs.SetInt("Windowed", 1);
    Screen.fullScreen = false;
  }


  void Update()
  {
    if (PlayerPrefs.GetInt("Windowed") == 1)
    {
      Selected1.SetActive(false);
      Selected2.SetActive(true);

    }
    else
    {
      Selected1.SetActive(true);
      Selected2.SetActive(false);
    }
  }
}