using LevelUpCSharp.Collections.Concurrent;
using LevelUpCSharp.Products;

namespace LevelUpCSharp.Production
{
    public class ProductionService
    {
        public Vendor Create(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Result<Vendor>.Failed();
            }

            var vendor = new Vendor(name, new Warehouse<Sandwich>(), new SlowMotion(new SandwichMaster()));

            Repositories.Vendors.Add(vendor.Name, vendor);

            return Result<Vendor>.Success(vendor);
        }
    }
}