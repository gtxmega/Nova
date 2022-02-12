using System;
using GameCore.CustomDataStruct;

namespace GameCore.Attributes.Specification
{
    public interface ISpecification
    {
        event Action<ThreeSpecification, ThreeSpecification> OnChangeSpecification;

        ThreeSpecification Specifications { get; }
        ThreeSpecification BonusSpecifications { get; }
        ESpecification MainSpecification { get; }
        
    }
}
