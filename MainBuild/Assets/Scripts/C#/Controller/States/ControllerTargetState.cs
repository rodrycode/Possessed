using UnityEngine;


public class ControllerTargetState : FSMState<ITargetable, TargetState>
{
    private Material OriginalMaterial { get; set; }
    private Material HoverMaterial { get; set; }

    public ControllerTargetState()
    {
        HoverMaterial = new Material(Shader.Find("Outlined/Silhouetted Diffuse"));
        HoverMaterial.SetColor("_Color", Color.white);
        HoverMaterial.SetColor("_OutlineColor", Color.blue);
    }

    public override TargetState State
    {
        get { return TargetState.Target; }
    }


    /// <summary>
    /// Start Targeting
    /// </summary>
    /// <param name="entity"></param>
    public override void Enter(ITargetable entity)
    {
        OriginalMaterial = entity.TargetRenderer.material;
        HoverMaterial.mainTexture = OriginalMaterial.mainTexture;
        entity.TargetRenderer.material = HoverMaterial;
    }

    /// <summary>
    /// Animate Targeting
    /// </summary>
    /// <param name="entity"></param>
    public override void Execute(ITargetable entity)
    {
        // TODO:  Object
        // TODO: Varible Thickness
        // TODO: Varible Speed - 
        // TODO: Varible Color -

        // Update Outine
        HoverMaterial.SetFloat("_Outline",
                   Mathf.Floor(Time.time % 2) == 0
                       ? Mathf.Lerp(0, 0.01f, Time.time % 1)
                       : Mathf.Lerp(0.01f, 0, Time.time % 1));
    }

    /// <summary>
    /// Reset Targeting
    /// </summary>
    /// <param name="entity"></param>
    public override void Exit(ITargetable entity)
    {
        entity.TargetRenderer.material = OriginalMaterial;
    }
}
