using LagDaemon.YAMUD.API;
using LagDaemon.YAMUD.API.Services;
using LagDaemon.YAMUD.API.Specs;
using LagDaemon.YAMUD.Model.Items;

namespace LagDaemon.YAMUD.WebAPI.Services.ItemsServices
{
    public class ItemsService
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<Item> _repository;

        public ItemsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<Item>();
        }

        public async Task<IEnumerable<Item>> GetAllItems(GeneralItemQuerySpec spec)
        {
            return await _repository.GetAsync(spec);
        }

        public async Task<Item?> GetItemById(Guid id)
        {
            var spec = new GeneralItemQuerySpec(x => x.Id == id, null, null );

            return (await _repository.GetAsync(spec)).FirstOrDefault();
        }


    }
}
