using UnityEngine;
using System.Collections;

public class BoxManager : MonoBehaviour
{
    public GameObject[] GridBlock, AutoFillBlocks;
    public GameObject[] RunBlockGroup, JumpBlockGroup, FallBlockGroup, StayBlockGroup;
    public GameObject RunBlock, JumpBlock, FallBlock, StayBlock;
    public GameObject[] Runinventory, JumpInventory, FallInventory, StayInventory;

    public bool runFound = false;
    public bool jumpFound = false;
    public bool fallFound = false;
    public bool stayFound = false;

    //checkRun
    public float rayLength = 2.5f;
    public LayerMask boxLayer;
    public int maxHits = 5;
    public float raySpacing = 0.5f;

    //placeBlock
    public int BlockRandomiser;
    public int InventoryRandomiser;

    private void Awake()
    {
        GridBlock = GameObject.FindGameObjectsWithTag("Grid");
    }

    public void OnWin()
    {
        //findBlocks
        for (int i = 0; i < GridBlock.Length; i++)
        {
            BoxCheckList checkList = GridBlock[i].GetComponent<BoxCheckList>();
            if(checkList.activated)
            {
                if(checkList.activatedRun)
                {
                    Vector2 origin = GridBlock[i].transform.position;
                    Vector2 direction = Vector2.right;
                
                    RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction, rayLength, boxLayer);
                    System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));        // Sort hits by distance
                    int count = Mathf.Min(maxHits, hits.Length);        // Limit to maxHits
                
                    if (count >= maxHits)
                    {
                        for (int r = 0; r < 10; r++)
                        {
                            if (RunBlockGroup[r] == null)
                            {
                                RunBlockGroup[r] = GridBlock[i];
                                break;
                            }
                        }
                        runFound = true;
                    }
                }

                if(checkList.activatedJump)
                {
                    //change this to select a random one out of the "discovered" ones (do this later in developement)
                    for (int r = 0; r < JumpBlockGroup.Length; r++)
                    {
                        if(JumpBlockGroup[r] == null)
                        {
                            JumpBlockGroup[r] = GridBlock[i];
                            break;
                        }
                    }
                    jumpFound = true;
                }

                if(checkList.activatedFall)
                {
                    for (int r = 0; r < FallBlockGroup.Length; r++)
                    {
                        if (FallBlockGroup[r] == null)
                        {
                            FallBlockGroup[r] = GridBlock[i];
                            break;
                        }
                    }
                    fallFound = true;
                }

                if(checkList.activatedStay)
                {
                    for (int r = 0; r < StayBlockGroup.Length; r++)
                    {
                        if (StayBlockGroup[r] == null)
                        {
                            StayBlockGroup[r] = GridBlock[i];
                            break;
                        }
                    }
                    stayFound = true;
                }
            }
        }
        SetPlaceBox();
    }

    public void SetPlaceBox()
    {
        int randomNumber = Random.Range(0,10);
        int rangeRun = Random.Range(0, Runinventory.Length);
        int rangeJump = Random.Range(0, JumpInventory.Length);
        int rangeFall = Random.Range(0, FallInventory.Length);
        int rangeStay = Random.Range(0, StayInventory.Length);
        if (runFound)
        {
            if (RunBlockGroup[rangeRun] != null)
            { RunBlock = RunBlockGroup[rangeRun]; }
            else
            {RunBlock = RunBlockGroup[0]; }
        }
        if (jumpFound)
        {
            if (JumpBlockGroup[rangeJump] != null)
            { JumpBlock = JumpBlockGroup[rangeJump]; }
            else
            { JumpBlock = JumpBlockGroup[0]; }
        }
        if (fallFound)
        {
            if (FallBlockGroup[rangeFall] != null)
            { FallBlock = FallBlockGroup[rangeFall]; }
            else
            { FallBlock = FallBlockGroup[0]; }
        }
        if (stayFound)
        {
            if (StayBlockGroup[rangeStay] != null)
            { StayBlock = StayBlockGroup[rangeStay]; }
            else
            { StayBlock = StayBlockGroup[0]; }
        }
        BlockRandomiser = Random.Range(1, 5);
        PlaceBlock();
    }
    public void PlaceBlock()
    {
        if (!runFound && !jumpFound && !fallFound && !stayFound) 
        {
            for (int i = 0; i < AutoFillBlocks.Length; i++)
            {
                BoxCheckList checkList = GridBlock[i].GetComponent<BoxCheckList>();
                if(!checkList.occupied)
                {
                    InventoryRandomiser = Random.Range(0, FallInventory.Length);
                    Instantiate(FallInventory[InventoryRandomiser], AutoFillBlocks[i].transform);
                    GridBlock[i].GetComponent<BoxCheckList>().occupied = true;
                    Debug.Log("NoBlocksFound"); return;
                }
            }
        } //place obj in autofill block (2 up from ground)

        switch (BlockRandomiser)
        {
            case 1:
                if(!runFound)
                {
                    Debug.Log("randomiserchangedfrom 1");
                    BlockRandomiser = 2;
                    PlaceBlock();
                    break;
                }
                else
                {
                    InventoryRandomiser = Random.Range(0, Runinventory.Length);
                    Instantiate(Runinventory[InventoryRandomiser], RunBlock.transform);
                    RunBlock.GetComponent<BoxCheckList>().occupied = true;
                    break;
                }
            case 2:
                if (!jumpFound)
                {
                    Debug.Log("randomiserchangedfrom 2");
                    BlockRandomiser = 3;
                    PlaceBlock();
                    break;
                }
                else
                {
                    InventoryRandomiser = Random.Range(0, JumpInventory.Length);
                    Instantiate(JumpInventory[InventoryRandomiser], JumpBlock.transform);
                    JumpBlock.GetComponent<BoxCheckList>().occupied = true;
                    break;
                }
            case 3:
                if (!fallFound)
                {
                    Debug.Log("randomiserchangedfrom 3");
                    BlockRandomiser = 4;
                    PlaceBlock();
                    break;
                }
                else
                {
                    InventoryRandomiser = Random.Range(0, FallInventory.Length);
                    Instantiate(FallInventory[InventoryRandomiser], FallBlock.transform);
                    FallBlock.GetComponent<BoxCheckList>().occupied = true;
                    break;
                }
            case 4:
                if (!stayFound)
                {
                    Debug.Log("randomiserchangedfrom 4");
                    BlockRandomiser = 1;
                    PlaceBlock();
                    break;
                }
                else
                {
                    InventoryRandomiser = Random.Range(0, StayInventory.Length);
                    Instantiate(StayInventory[InventoryRandomiser], StayBlock.transform);
                    StayBlock.GetComponent<BoxCheckList>().occupied = true;
                    break;
                }
        }
    }

    public void ResetBoxes()
    {
        runFound = false;
        jumpFound = false;
        fallFound = false;
        stayFound = false;
        RunBlock = null;
        JumpBlock = null;
        FallBlock = null;
        StayBlock = null;
        foreach (var item in GridBlock)
        {
            BoxCheckList checklist = item.GetComponent<BoxCheckList>();
            if (checklist.activated)
            {
                checklist.activatedRun = false;
                checklist.activatedJump = false;
                checklist.activatedFall = false;
                checklist.activatedStay = false;
                checklist.gameObject.layer = checklist.deactiveLayer;
                checklist.timer = 0;
                checklist.gameObject.GetComponent<SpriteRenderer>().material = checklist.def;
                checklist.activated = false;
            }
        }
        for (int i = 0; i < 10; i++)
        {
            RunBlockGroup[i] = null;
            JumpBlockGroup[i] = null;
            FallBlockGroup[i] = null;
            StayBlockGroup[i] = null;
        }
        //reset active state
        //reset layer
        //reset found blocks
    }
}
