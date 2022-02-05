using UnityEngine.Purchasing;

namespace Model.Shop
{
    public class ShopProduct
    {
        public string Id;
        public ProductType CurrentProductType;

        public ShopProduct(string id, ProductType productType)
        {
            Id = id;
            CurrentProductType = productType;
        }
    }
}