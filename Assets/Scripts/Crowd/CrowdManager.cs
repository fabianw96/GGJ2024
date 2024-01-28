using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdManager : MonoBehaviour
{

    public Spectator spectatorPrefab;

    private List<Spectator> Spectators;
    [SerializeField]
    private int stageCrowdSize = 100;
	[SerializeField]
	private float spawnRadius = 15.0f;
	[SerializeField]
    private float stageWidth = 1.0f;
	public int numberOfStages = 4;

	private float nextStageRotationOffset = 0;

    private Vector3 spawnPosition = Vector3.zero;
	public Transform arenaMiddle;

    private float spawnOffset;

	public int randomSpectatorsToHop = 0;
	public int simulatanousHops = 20;
	
	private bool runningCoroutine = false;

	

	// Start is called before the first frame update

	private void Awake()
	{
		Spectators = new List<Spectator>();
        spawnOffset = 360.0f / stageCrowdSize;
		nextStageRotationOffset = spawnOffset / 2;
        
	}
	void Start()
    {
		for (int i = 0; i < numberOfStages; i++)
		{
			StartCoroutine(SpawnCrowd(i));

		}

	}

    // Update is called once per frame
    void Update()
    {
        if(!runningCoroutine)
		{
			runningCoroutine = true;
			StartCoroutine(CrowdRandomHop());
			
		}
    }

    IEnumerator SpawnCrowd(int stage)
    {

        for (int i = 1; i < stageCrowdSize+1; i++)
        {
			
			RaycastHit hit;
			Physics.Raycast(transform.forward * (spawnRadius+stageWidth*stage) + new Vector3(0, 100, 0), Vector3.down, out hit);
			Debug.DrawRay(transform.forward * spawnRadius + new Vector3(0, 0, stageWidth + stage) + new Vector3(0, 10, 0), Vector3.down, Color.red, 1000.0f);
		

			Spectator dude = Instantiate(spectatorPrefab, new Vector3(hit.point.x, hit.point.y + 1, hit.point.z), Quaternion.identity);
			dude.transform.LookAt(arenaMiddle.transform.position);
			dude.render.material.color = new Color(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

			transform.rotation = Quaternion.Euler(0,nextStageRotationOffset*((stage+1)%2) +transform.rotation.y + spawnOffset * i, 0);
			Spectators.Add(dude);
			yield return null;
        }
		
    }



	IEnumerator CrowdRandomHop()
	{
		for (int i = 0; i < randomSpectatorsToHop; i++)
		{
			for (int j = 0; j < simulatanousHops; j++)
			{

				int randomdude = Random.Range(0, Spectators.Count - 1);
				if (Spectators[randomdude].rb.velocity == Vector3.zero)
				{
					Spectators[randomdude].Hop();
					Spectators[randomdude].transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, 0);

					if (Random.Range(0,2)==1)
					{
						Spectators[randomdude].transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, 5);
					}
					else
					{
						Spectators[randomdude].transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, -5);
					}
				}
			}
			
			yield return null;
		}
		runningCoroutine = false;
	}
}
