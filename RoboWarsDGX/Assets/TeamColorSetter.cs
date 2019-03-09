using Photon.Pun;
using UnityEngine;

public class TeamColorSetter : MonoBehaviourPun
{
    [Header ("Red team materials")]
    public Material glovesRed;
    public Material topRed;
    public Material bottomRed;
    public Material shoesRed;

    [Header("Blue team materials")]
    public Material glovesBlue;
    public Material topBlue;
    public Material bottomBlue;
    public Material shoesBlue;

    [Header("Full body Clothes")]
    public SkinnedMeshRenderer gloves;
    public SkinnedMeshRenderer top;
    public SkinnedMeshRenderer bottom;
    public SkinnedMeshRenderer shoes;

    [Header ("First person clothes")]
    public SkinnedMeshRenderer glovesFPS;

    private TeamColor teamColor = TeamColor.Null;

    public TeamColor TeamColor { get => teamColor; }

    public void ChangeClothes(TeamColor color)
    {
        photonView.RPC("ChangeClothesColor", RpcTarget.AllBuffered, (int)color);
    }

    [PunRPC]
    private void ChangeClothesColor(int color)
    {
        TeamColor myColor = (TeamColor)color;
        teamColor = myColor;
        if (myColor == TeamColor.Red)
        {
            ChangeClothesToRed();
        }
        else
        {
            ChangeClothesToBlue();
        }
    }

    private void ChangeClothesToRed()
    {
        if (photonView.IsMine)
        {
            Material[] mats = glovesFPS.materials;
            mats[0] = glovesRed;
            glovesFPS.materials = mats;
        }
        else
        {
            Material[] mats = gloves.materials;
            mats[0] = glovesRed;
            gloves.materials = mats;

            mats = top.materials;
            mats[0] = topRed;
            top.materials = mats;

            mats = bottom.materials;
            mats[0] = bottomRed;
            bottom.materials = mats;

            mats = shoes.materials;
            mats[0] = shoesRed;
            shoes.materials = mats;
        }
    }

    private void ChangeClothesToBlue()
    {
        if (photonView.IsMine)
        {
            Material[] mats = glovesFPS.materials;
            mats[0] = glovesBlue;
            glovesFPS.materials = mats;
        }
        else
        {
            Material[] mats = gloves.materials;
            mats[0] = glovesBlue;
            gloves.materials = mats;

            mats = top.materials;
            mats[0] = topBlue;
            top.materials = mats;

            mats = bottom.materials;
            mats[0] = bottomBlue;
            bottom.materials = mats;

            mats = shoes.materials;
            mats[0] = shoesBlue;
            shoes.materials = mats;
        }
    }
}
