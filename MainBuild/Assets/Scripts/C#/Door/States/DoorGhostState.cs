using UnityEngine;
using System.Collections;

public class DoorGhostState : IFSMState<Door, DoorState>
{
    private Material OriginalMaterial { get; set; }
    private Material HoverMaterial { get; set; }

    public DoorGhostState()
    {
//        HoverMaterial = new Material(Shader.Find("Outlined/Silhouetted Diffuse"));
//        HoverMaterial.SetColor("_Color", Color.white);
//        HoverMaterial.SetColor("_OutlineColor", Color.blue);

        HoverMaterial = new Material(Shader.Find("Transparent/Bumped Diffuse"));
    }

    public DoorState State
    {
        get { return DoorState.Ghost; }
    }

    public void Enter(Door entity)
    {
        OriginalMaterial = entity.TargetRenderer.material;
        HoverMaterial.mainTexture = OriginalMaterial.mainTexture;
        entity.TargetRenderer.material = HoverMaterial;
    }

    public void Execute(Door entity)
    {
        if (entity.Opener != null)
        {
//            var material = entity.TargetRenderer.material;
            var distance = Vector3.Distance(entity.Transform.position, entity.Opener.Transform.position);
           // Debug.Log(distance / entity.Opener.Distance);


            if (HoverMaterial.HasProperty("_MainColor"))
            {
                var color1 = HoverMaterial.GetColor("_MainColor");
                color1.a = distance / entity.Opener.Distance;
                HoverMaterial.SetColor("_MainColor", color1);
            } 
            
            if (HoverMaterial.HasProperty("_Color"))
            {
                var color1 = HoverMaterial.GetColor("_Color");
                color1.a = distance / entity.Opener.Distance;
                HoverMaterial.SetColor("_Color", color1);
            }

            if (HoverMaterial.HasProperty("_OutlineColor"))
            {
                var color2 = HoverMaterial.GetColor("_OutlineColor");
                color2.a = distance/entity.Opener.Distance/4.0f;
                HoverMaterial.SetColor("_OutlineColor", color2);
            }

        }

        if (entity.Opener == null || entity.CloseDistance < Vector3.Distance(entity.Opener.Transform.position, entity.Transform.position))
            if (!entity.Animation.isPlaying)
                entity.Close();
    }

    public void Exit(Door entity)
    {
        entity.TargetRenderer.material = OriginalMaterial;
    }
}
