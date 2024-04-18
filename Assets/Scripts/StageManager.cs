using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public Button[] stageButtons;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < stageButtons.Length; i++)
        {
            int stageIndex = i;
            stageButtons[i].onClick.AddListener(() => SelectStage(stageIndex));
            UpdateStageButton(stageIndex);
        }
    }

    private void SelectStage(int stageIndex)
    {
        if (IsStageCleared(stageIndex))
        {
            Debug.Log("Stage " + (stageIndex + 1) + " is cleared. Start the stage!");
        }
        else
        {
            Debug.Log("Stage " + (stageIndex + 1) + " is locked. You need to clear previous stages.");
        }
    }

    private void UpdateStageButton(int stageIndex)
    {
        stageButtons[stageIndex].interactable = IsStageCleared(stageIndex);
    }

    private bool IsStageCleared(int stageIndex)
    {
        return stageIndex == 0 || PlayerPrefs.GetInt("Stage" + stageIndex + "Cleared", 0) == 1;
    }
}
