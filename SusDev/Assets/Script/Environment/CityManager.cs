using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CityManager : MonoBehaviour
{
    private TestGrid[,] grid;
    public float roadLength;
    public int blockSize;
    public int numOfBlocksX;
    public int numOfBlocksZ;
    public int gridCellSize;

    public GameObject buildingsParent;

    //building prefabs
    [SerializeField]
    public GameObject[] orinaryHouse;
    [SerializeField]
    public GameObject[] skyscrapers;
    [SerializeField]
    public GameObject[] government;
    [SerializeField]
    public GameObject[] waterPlant;
    [SerializeField]
    public GameObject[] trashPlant;
    [SerializeField]
    public GameObject[] solarPlant;
    [SerializeField]
    public GameObject[] thermalPlant;
    [SerializeField]
    public GameObject[] windPlant;
    [SerializeField]
    public GameObject[] hospital;
    [SerializeField]
    public GameObject[] school;

    //VFX
    [SerializeField]
    public GameObject smoke;

    public static Vector3 buildingPos;
    //postions
    private List<TestGrid> _vaccantHouse;
    private List<TestGrid> _vaccantSkyscrapers;
    private List<TestGrid> _vaccantSpecial;

    // Start is called before the first frame update
    void Start()
    {
        _vaccantHouse = new List<TestGrid>();
        _vaccantSkyscrapers = new List<TestGrid>();
        _vaccantSpecial = new List<TestGrid>();
        //instantiate grid system
        grid = new TestGrid[numOfBlocksX, numOfBlocksZ];
        for (int i = 0; i < numOfBlocksX; i++)
        {
            for (int j = 0; j < numOfBlocksZ; j++)
            {
                grid[i, j] = new TestGrid(i, j, new Vector3(roadLength + blockSize * gridCellSize * i + roadLength * i, 0.5f, roadLength + blockSize * gridCellSize * j + roadLength * j)
                    , blockSize, blockSize, gridCellSize);
            }
        }
        AssignBuildingType(new Vector2(2, 3), new Vector2(2, 4), 1);
        AssignBuildingType(new Vector2(3, 2), new Vector2(3, 3), 1);
        AssignBuildingType(new Vector2(6, 0), new Vector2(6, 5), 2);
        InstantiateConstruction(1, 2, 1);
        InstantiateConstruction(0, 0, 10);
        InstantiateConstruction(1, 1, 5);
        InstantiateConstruction(2, 6, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            /*  InstantiateConstruction();*/
           
        }
    }
    //0,0: ordinary houses
    //1,1: skysrapers
    //1,2: government
    //2,3: waterPlant;
    //2,4: trashPlant;
    //2,5: solarPlant;
    //2,6: thermalPlant;
    //2,7: windPlant;
    //0,8: hospital;
    //0,9: school
    public void InstantiateConstruction(int cellType, int buildingType, int numToInstantiate)
    {
        for(int i = 0; i < numToInstantiate; i++)
        {
            Build(GetVaccantList(cellType), buildingType);
        }
    }
    public List<TestGrid> GetVaccantList(int type)
    {
        switch (type)
        { 
            case 0:
                _vaccantHouse.Clear();
                for (int i = 0; i < numOfBlocksX; i++)
                {
                    for (int j = 0; j < numOfBlocksZ; j++)
                    {
                        if (grid[i, j]._buildingType == 0)
                        {
                            if (grid[i, j]._vacant.Count > 0)
                            {
                                _vaccantHouse.Add(grid[i, j]);
                            }
                        }
                    }
                }
                return _vaccantHouse;
            case 1:
                _vaccantSkyscrapers.Clear();
                for (int i = 0; i < numOfBlocksX; i++)
                {
                    for (int j = 0; j < numOfBlocksZ; j++)
                    {
                        if (grid[i, j]._buildingType == 1)
                        {
                            if (grid[i, j]._vacant.Count > 0)
                            {
                                _vaccantSkyscrapers.Add(grid[i, j]);
                            }
                        }
                    }
                }
                return _vaccantSkyscrapers;
            case 2:
                _vaccantSpecial.Clear();
                for (int i = 0; i < numOfBlocksX; i++)
                {
                    for (int j = 0; j < numOfBlocksZ; j++)
                    {
                        if (grid[i, j]._buildingType == 2)
                        {
                            if (grid[i, j]._vacant.Count > 0)
                            {
                                _vaccantSpecial.Add(grid[i, j]);
                            }
                        }
                    }
                }
                return _vaccantSpecial;
            default:
                return null;
        }
    }
    //0,0: ordinary houses
    //1,1: skysrapers
    //1,2: government
    //2,3: waterPlant;
    //2,4: trashPlant;
    //2,5: solarPlant;
    //2,6: thermalPlant;
    //2,7: windPlant;
    //0,8: hospital;
    //0,9: school;
    public void Build(List<TestGrid> list2instantiate, int type)
    {
        if (list2instantiate != null)
        {
            switch (type)
            {
                case 0:
                    PickGridtoInstantiate(list2instantiate, orinaryHouse);
                    return;
                case 1:
                    PickGridtoInstantiate(list2instantiate, skyscrapers);
                    return;
                case 2:
                    PickGridtoInstantiate(list2instantiate, government);
                    return;
                case 3:
                    PickGridtoInstantiate(list2instantiate, waterPlant);
                    return;
                case 4:
                    PickGridtoInstantiate(list2instantiate, trashPlant);
                    return;
                case 5:
                    PickGridtoInstantiate(list2instantiate, solarPlant);
                    return;
                case 6:
                    PickGridtoInstantiate(list2instantiate, thermalPlant);
                    return;
                case 7:
                    PickGridtoInstantiate(list2instantiate, windPlant);
                    return;
                case 8:
                    PickGridtoInstantiate(list2instantiate, hospital);
                    return;
                case 9:
                    PickGridtoInstantiate(list2instantiate, school);
                    return;
                default:
                    return;
            }
        }
    }
    public void PickGridtoInstantiate(List<TestGrid> grid, GameObject[] constructions)
    {
        ShuffleList(grid);
        if (grid.Count > 0)
        {
            //randomize buildings
            int index = Random.Range(0, constructions.Length - 1);
            int x = grid[0]._vacant[grid[0]._vacant.Count - 1][0];
            int z = grid[0]._vacant[grid[0]._vacant.Count - 1][1];
            Vector3 pos = new Vector3(blockSize * gridCellSize * grid[0]._x + roadLength * (grid[0]._x + 1) +
                x * grid[0]._cellSize, 2.5f, blockSize * gridCellSize * grid[0]._y + roadLength * (grid[0]._y + 1) + z * grid[0]._cellSize);
            Vector3 cellPos = new Vector3(x * (grid[0]._cellSize + roadLength) + roadLength, 0.5f, z * (grid[0]._cellSize + roadLength) + roadLength);
            GameObject building = Instantiate(constructions[index], pos + new Vector3(grid[0]._cellSize / 2, 0, grid[0]._cellSize / 2), Quaternion.identity);
            buildingPos = building.transform.position;
            building.transform.SetParent(buildingsParent.transform);
            StartCoroutine(InstatiateBuildings(building.transform, building.transform.position, 2f));
            grid[0].SetValue(cellPos, 1);
        }
    }

    public void AssignBuildingType(Vector2 min, Vector2 max, int buildingType)
    {
        for (int c = (int)min.x; c <= (int)max.x; c++)
        {
            for (int r = (int)min.y; r <= (int)max.y; r++)
            {
                grid[c, r].SetBuildingType(buildingType);
            }
        }
    }
    IEnumerator InstatiateBuildings(Transform t, Vector3 startPosition, float duration)
    {

        float time = 0;
        int acceleration = 2;
        Vector3 targetPosition = new Vector3(startPosition.x, 0.5f, startPosition.z);
        while (time + time * acceleration < duration)
        {
            t.position = Vector3.Lerp(startPosition, targetPosition, (time + time * acceleration) / duration);
            time += Time.deltaTime;
            yield return null;

        }
        Smoke(startPosition);
        t.position = targetPosition;
    }
    public void Smoke(Vector3 Position)
    {
        Vector3 targetPos = new Vector3(Position.x, 0f, Position.z);
        GameObject smk = Instantiate(smoke, targetPos, Quaternion.identity);
        Destroy(smk, 1f);
    }
    public void ShuffleList(List<TestGrid> input)
    {
        for (int i = 0; i < input.Count; i++)
        {
            var temp = input[i];
            int index = Random.Range(i, input.Count);
            input[i] = input[index];
            input[index] = temp;
        }
    }
}

