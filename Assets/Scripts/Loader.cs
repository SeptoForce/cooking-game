using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{

    public enum Scene
    {
        MainMenu,
        GameScene,
        LoadingScene,
    }

    public static Scene TargetScene;
    
    public static void Load(Scene targetScene)
    {
        Loader.TargetScene = targetScene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }
    
    public static void LoaderCallback()
    {
        if (TargetScene != Scene.LoadingScene)
        {
            SceneManager.LoadScene(TargetScene.ToString());
        }
    }
}
