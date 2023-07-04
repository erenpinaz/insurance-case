namespace Insurance.Api.Application
{
    public static class InsuranceCostCalculator
    {
        /// <summary>
        /// Calculates insurance for multiple products (an order)
        /// </summary>
        /// <param name="products"></param>
        /// <returns><see cref="float"/> total insurance cost</returns>
        public static float Calculate(List<ProductSummaryDto> products)
        {
            float totalInsuranceCost = 0F;

            foreach (var product in products)
            {
                totalInsuranceCost += Calculate(product);
            }

            var isAdditionalOrderCostApplicable = products.Any(p =>
                p.ProductTypeHasInsurance &&
                p.ProductTypeName.Equals(BusinessRules.ProductTypeWithAdditionalOrderCost, StringComparison.OrdinalIgnoreCase));

            if (isAdditionalOrderCostApplicable)
            {
                totalInsuranceCost += BusinessRules.AdditionalOrderInsuranceCost;
            }

            return totalInsuranceCost;
        }

        /// <summary>
        /// Calculates insurance cost for a single product
        /// </summary>
        /// <param name="product"></param>
        /// <returns><see cref="float"/> insurance cost</returns>
        public static float Calculate(ProductSummaryDto product)
        {
            if (!product.ProductTypeHasInsurance)
            {
                return 0F;
            }

            float result = AddRangeBasedCost(0F);
            result = AddSurchargeRate(result);
            result = AddAdditionalCost(result);

            product.InsuranceCost = result;

            return result;

            float AddRangeBasedCost(float insuranceCost)
            {
                if (product.SalesPrice >= BusinessRules.MinSalesPrice && product.SalesPrice < BusinessRules.MaxSalesPrice)
                {
                    insuranceCost += BusinessRules.FirstLevelInsuranceCost;
                }
                else if (product.SalesPrice >= BusinessRules.MaxSalesPrice)
                {
                    insuranceCost += BusinessRules.SecondLevelInsuranceCost;
                }

                return insuranceCost;
            }

            float AddSurchargeRate(float insuranceCost)
            {
                if (product.SurchargeRate > 0)
                {
                    insuranceCost += product.SalesPrice * product.SurchargeRate / 100;
                }

                return insuranceCost;
            }

            float AddAdditionalCost(float insuranceCost)
            {
                if (BusinessRules.ProductTypesWithAdditionalCost.Contains(product.ProductTypeName, StringComparer.OrdinalIgnoreCase))
                {
                    insuranceCost += BusinessRules.AdditionalInsuranceCost;
                }

                return insuranceCost;
            }
        }
    }
}
