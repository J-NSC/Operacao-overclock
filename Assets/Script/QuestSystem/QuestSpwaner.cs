using UnityEngine;

public class QuestSpawner : MonoBehaviour
{
    [SerializeField] GameObject startPoint;
    [SerializeField] GameObject endPoint; 
    [SerializeField] GameObject taskPrefab;
    [SerializeField] float speed = 2f;

    private GameObject instantiatedObject;

    void Start()
    {
        instantiatedObject = Instantiate(taskPrefab, startPoint.transform.position, Quaternion.identity);
    }

    void Update()
    {
        if (instantiatedObject != null)
        {
            if (Vector3.Distance(instantiatedObject.transform.position, endPoint.transform.position) > 0.01f)
            {
                instantiatedObject.transform.position = Vector3.MoveTowards(
                    instantiatedObject.transform.position, 
                    endPoint.transform.position, 
                    speed * Time.deltaTime
                );    
            }
        }
    }

    public void InstantiedNewQuest()
    {
        instantiatedObject = Instantiate(taskPrefab, startPoint.transform.position, Quaternion.identity);
    }
}
