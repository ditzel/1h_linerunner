using UnityEngine;

public class WayPool : MonoBehaviour {

    protected Cube[] Cubes;
    protected int CurrentIndex;
    protected Vector3 CurrentPosition;
    [HideInInspector]
    public Vector3 CurrentDirection;
    public Transform Sphere;
    public Transform Pointer;

	// Use this for initialization
	void Start () {
        Cubes = transform.GetComponentsInChildren<Cube>();
        CurrentIndex = 0;
        CurrentPosition = new Vector3(0f,-0.5f,0f);

        //Generate First Block
        GenerateBlock(5f, Vector3.forward);
    }
	
	// Update is called once per frame
	void Update () {
		
        if(Vector3.Distance(Sphere.position, CurrentPosition) < 2f)
        {
            GenerateWay();
        }

	}

    public void GenerateWay()
    {
        float length = Random.Range(2f, 10f);
        int d = Random.Range(-1,3);
        Vector3 dir = Vector3.zero;
        if (CurrentDirection == Vector3.left || CurrentDirection == Vector3.right)
        {
            dir = Vector3.forward;
        }
        else
        {
            if(Random.Range(-1f, 1f) < 0f)
            {
                dir = Vector3.left;
            }
            else
            {
                dir = Vector3.right;
            }   
        }
        GenerateBlock(length, dir);
    }

    internal void ResetMe()
    {
        foreach (var cube in Cubes) {
            cube.transform.position = Vector3.down * 0.5f;
            cube.transform.localScale = Vector3.one;
        }
        Start();
    }

    public void GenerateBlock(float length, Vector3 direction)
    {
        Cubes[CurrentIndex].transform.position = CurrentPosition + direction * length / 2f;
        CurrentPosition += direction * length;

        Cubes[CurrentIndex].transform.localScale = Vector3.one + direction.normalized * length;
        CurrentIndex = (CurrentIndex + 1) % Cubes.Length;
        CurrentDirection = direction;
        if(Pointer != null)
            Pointer.transform.position = CurrentPosition;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(CurrentPosition, 0.01f);
    }
}
