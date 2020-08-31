using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public int LoadScore()
    {
        return PlayerPrefs.GetInt("Highscore", 0);
    }

    public int SaveScore(int score)
    {
        if (score > PlayerPrefs.GetInt("Highscore", 0))
        {
            PlayerPrefs.SetInt("Highscore", score);
            return 1;
        }
        else
            return 0;
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
    }
}
