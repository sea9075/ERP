using ERP.Models.Purchase;

namespace ERP.DataAccess.Repository.IRepository.IBasicInformation
{
    public interface IProductFlowRepository : IRepository<ProductFlow>
    {
        void Update(ProductFlow productFlow);
    }
}
