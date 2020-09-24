using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class SiblingRuleTile : RuleTile
{

    public enum SibingGroup
    {
        Terrain
    }
    public SibingGroup sibingGroup;

    public override bool RuleMatch(int neighbor, TileBase other)
    {
        if (other is RuleOverrideTile)
            other = (other as RuleOverrideTile).m_InstanceTile;

        switch (neighbor)
        {
            case TilingRule.Neighbor.This:
                {
                    return other is SiblingRuleTile
                        && (other as SiblingRuleTile).sibingGroup == this.sibingGroup;
                }
            case TilingRule.Neighbor.NotThis:
                {
                    return !(other is SiblingRuleTile
                        && (other as SiblingRuleTile).sibingGroup == this.sibingGroup);
                }
        }

        return base.RuleMatch(neighbor, other);
    }
}
