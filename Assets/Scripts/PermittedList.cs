using System.Collections.Generic;
using UnityEngine;

public class PermittedList : MonoBehaviour
{
    public Dictionary<GameObject, List<GameObject>> permittedList = new Dictionary<GameObject, List<GameObject>>();
    [SerializeField] public GameObject Base;
    [SerializeField] GameObject JumpJumpVoid;
    [SerializeField] GameObject JumpSlide;
    [SerializeField] GameObject JumpJump;
    [SerializeField] GameObject JumpWallPLaser;
    [SerializeField] GameObject PlatformJumps;
    [SerializeField] GameObject LongCLaser;

    void Awake()
    {
        List<GameObject> BaseList = new List<GameObject>()
        {
            JumpJumpVoid,
            JumpSlide,
            JumpJump,
            JumpWallPLaser,
            PlatformJumps,
            LongCLaser
        };

        List<GameObject> JumpJumpVoidList = new List<GameObject>()
        {
            JumpSlide,
            JumpJump,
            JumpWallPLaser,
            PlatformJumps,
            LongCLaser
        };

        List<GameObject> JumpSlideList = new List<GameObject>()
        {
            JumpJumpVoid,
            JumpJump,
            JumpWallPLaser,
            PlatformJumps,
            LongCLaser
        };

        List<GameObject> JumpJumpList = new List<GameObject>()
        {
            JumpJumpVoid,
            JumpSlide,
            JumpWallPLaser,
            PlatformJumps,
            LongCLaser
        };
        List<GameObject> JumpWallPLaserList = new List<GameObject>()
        {
            JumpJumpVoid,
            JumpSlide,
            JumpJump,
            PlatformJumps,
        };
        List<GameObject> PlatformJumpsList = new List<GameObject>()
        {
            JumpJumpVoid,
            JumpSlide,
            JumpJump,
            JumpWallPLaser,
            LongCLaser
        };
        List<GameObject> LongCLaserList = new List<GameObject>()
        {
            JumpJumpVoid,
            JumpSlide,
            JumpJump,
            PlatformJumps
        };
        permittedList.Add(Base, BaseList);
        permittedList.Add(JumpJumpVoid, JumpJumpVoidList);
        permittedList.Add(JumpSlide, JumpSlideList);
        permittedList.Add(JumpJump, JumpJumpList);
        permittedList.Add(JumpWallPLaser, JumpWallPLaserList);
        permittedList.Add(PlatformJumps, PlatformJumpsList);
        permittedList.Add(LongCLaser, LongCLaserList);
    }
}
