using System;
using GameCore.CustomDataStruct;
using UnityEngine;

namespace GameCore.Attributes.Specification
{
    public class SpecificationAttributes : MonoBehaviour, ISpecification
    {
        public event Action<ThreeSpecification, ThreeSpecification> OnChangeSpecification;

        public ThreeSpecification Specifications => _specifications;

        public ThreeSpecification BonusSpecifications => _bonusSpecifications;

        public ESpecification MainSpecification => _mainSpeficitaon;


        [SerializeField] private ThreeSpecification _specifications;
        [SerializeField] private ESpecification _mainSpeficitaon;

        private ThreeSpecification _bonusSpecifications;


    }
}
