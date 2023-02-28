using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] Transform initialPosition;
    [SerializeField] Vector3 randomOffset;
    Vector3 finalPos;
    //[SerializeField][Range(10f,20f)] float timeTaken;
    // Start is called before the first frame update
    void Start()
    {

        SetRandomOffset();


        finalPos = new Vector3(70, this.transform.position.y, this.transform.position.z);
        var tween = LeanTween.move(this.gameObject, finalPos,Random.Range(10f,20f));
        tween.setOnComplete(OnTweenComplete);

    }

    private void SetRandomOffset()
    {
        randomOffset = new Vector3(0f, Random.Range(-5f, 20f), Random.Range(0f, 10f));
    }

    private void OnTweenComplete()
    {
        print("The Tween is complet");
        this.gameObject.transform.position = initialPosition.position + randomOffset;
        finalPos = new Vector3(70, this.transform.position.y, this.transform.position.z);

        var tween = LeanTween.move(this.gameObject, finalPos, Random.Range(80f, 100f));
        tween.setOnComplete(OnTweenComplete);
    }

}
