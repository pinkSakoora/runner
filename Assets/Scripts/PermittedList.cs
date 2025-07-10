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

    void Awake()
    {
        List<GameObject> BaseList = new List<GameObject>()
        {
            JumpJumpVoid,
            JumpSlide,
            JumpJump,
            JumpWallPLaser,
            PlatformJumps
        };

        List<GameObject> JumpJumpVoidList = new List<GameObject>()
        {
            JumpSlide,
            JumpJump,
            JumpWallPLaser,
            PlatformJumps
        };

        List<GameObject> JumpSlideList = new List<GameObject>()
        {
            JumpJumpVoid,
            JumpJump,
            JumpWallPLaser,
            PlatformJumps
        };

        List<GameObject> JumpJumpList = new List<GameObject>()
        {
            JumpJumpVoid,
            JumpSlide,
            JumpWallPLaser,
            PlatformJumps
        };
        List<GameObject> JumpWallPLaserList = new List<GameObject>()
        {
            JumpJumpVoid,
            JumpSlide,
            JumpJump,
            PlatformJumps
        };
        List<GameObject> PlatformJumpsList = new List<GameObject>()
        {
            JumpJumpVoid,
            JumpSlide,
            JumpJump,
            JumpWallPLaser,
        };
        permittedList.Add(Base, BaseList);
        permittedList.Add(JumpJumpVoid, JumpJumpVoidList);
        permittedList.Add(JumpSlide, JumpSlideList);
        permittedList.Add(JumpJump, JumpJumpList);
        permittedList.Add(JumpWallPLaser, JumpWallPLaserList);
        permittedList.Add(PlatformJumps, PlatformJumpsList);
    }
}
