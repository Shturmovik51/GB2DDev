using UnityEngine.Purchasing;

namespace Model.Shop
{
    public class ShopProduct
    {
        public string Id { get; }
        public int Count { get; }
        public TypeOfProduct Type { get; }
        public ProductType CurrentProductType { get; }

        public ShopProduct(string id, ProductType productType, int count, TypeOfProduct type)
        {
            Id = id;
            Count = count;
            CurrentProductType = productType;
            Type = type;
        }
    }
}