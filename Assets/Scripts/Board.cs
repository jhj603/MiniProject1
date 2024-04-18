using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static UnityEngine.GraphicsBuffer;

public class Board : MonoBehaviour
{
    public GameObject goCard;

    // Start is called before the first frame update
    void Start()
    {
        int level = GameManager.Level;

        int num = 4 * level;
        int[] arr = new int[num];

        for(int i = 0; i < num / 2; i++)
        {   
            arr[i * 2] = i;
            arr[i * 2 + 1] = i;
        }

        arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray();

        //위치조절은 레벨에 맞게 미리 설정
        for(int i = 0; i < num; i++)
        {
            GameObject go = Instantiate(goCard, this.transform);

            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 0.83f * level;

            go.transform.position = new Vector2(x, y);
            go.GetComponent<Card>().Setting(arr[i], i);
        }
 
        GameManager.Instance.cardCount = arr.Length;
    }
}
