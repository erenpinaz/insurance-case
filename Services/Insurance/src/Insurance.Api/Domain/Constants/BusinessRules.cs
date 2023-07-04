﻿namespace Insurance.Api.Domain.Constants
{
    public static class BusinessRules
    {
        /// <summary>
        /// Lower bound range value used to determine insurance cost
        /// </summary>
        public const float MinSalesPrice = 500F;

        /// <summary>
        /// Upper bound range value used to determine insurance cost
        /// </summary>
        public const float MaxSalesPrice = 2000F;

        /// <summary>
        /// A product is first-level insurable when a value 
        /// greater than or equal to <see cref="MinSalesPrice"/> and 
        /// less than <see cref="MaxSalesPrice"/>
        /// </summary>
        public const float FirstLevelInsuranceCost = 1000F;

        /// <summary>
        /// A product is second-level insurable when a value 
        /// greater than or equal to <see cref="MaxSalesPrice"/>
        /// </summary>
        public const float SecondLevelInsuranceCost = 2000F;

        /// <summary>
        /// Value to be added to total when a product subject for 
        /// additional insurance costs
        /// </summary>
        public const float AdditionalInsuranceCost = 500F;

        /// <summary>
        /// Value to be added to total when an order is subject for 
        /// additional insurance cost
        /// </summary>
        public const float AdditionalOrderInsuranceCost = 500F;

        /// <summary>
        /// Used to determine if a product subject for 
        /// additional insurance cost
        /// </summary>
        public static readonly HashSet<string> ProductTypesWithAdditionalCost = new(StringComparer.OrdinalIgnoreCase)
        {
            { ProductTypeNames.LAPTOP },
            { ProductTypeNames.SMARTPHONE }
        };

        /// <summary>
        /// Used to determine if an order subject for 
        /// additional insurance cost
        /// </summary>
        public const string ProductTypeWithAdditionalOrderCost = ProductTypeNames.DIGITAL_CAMERA;
    }
}
