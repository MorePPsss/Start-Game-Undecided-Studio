using UnityEngine;

/*原本想做爆炸效果的控制逻辑 还没实现
 TODO 爆炸效果的逻辑
 */
public class ExplodeBlocks : MonoBehaviour
{
    public GameObject block;
    // 启动爆炸效果
    public void Explode()
    {
        Debug.Log("爆炸！");
        this.GetComponent<Renderer>().enabled = true;

    }
}
