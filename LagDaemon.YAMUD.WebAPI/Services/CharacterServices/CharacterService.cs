using LagDaemon.YAMUD.API;
using LagDaemon.YAMUD.API.Security;
using LagDaemon.YAMUD.API.Services;
using LagDaemon.YAMUD.Model;
using LagDaemon.YAMUD.Model.Characters;
using LagDaemon.YAMUD.WebAPI.Utilities;

namespace LagDaemon.YAMUD.WebAPI.Services.CharacterServices
{
    public class CharacterService : ICharacterService
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<Character> _repository;
        private IUserAccountService _userAccountService;

        public CharacterService(IUnitOfWork unitOfWork, IUserAccountService userAccountService)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<Character>();
            _userAccountService = userAccountService;
        }


        [Security(UserAccountRoles.Admin, UserAccountRoles.Owner, UserAccountRoles.Founder)]        
        public async Task<Character> CreateCharacter(Character character)
        {
            _repository.Insert(character);
            await _unitOfWork.SaveChangesAsync();
            return character;
        }

        [Security(UserAccountRoles.Admin, UserAccountRoles.Owner, UserAccountRoles.Founder)]
        public async Task<Character> UpdateOrCreateCharacter(Character character)
        {
            var existingCharacter = (await _repository.GetAsync(x => x.Id == character.Id)).FirstOrDefault();
            if (existingCharacter != null)
            {
                var entry = _unitOfWork.Entry(existingCharacter);
                entry.CurrentValues.SetValues(character);
                await _unitOfWork.SaveChangesAsync();
                return existingCharacter;
            }
            else
            {
                return await CreateCharacter(character);
            }
        }

        [Security(UserAccountRoles.Admin, UserAccountRoles.Owner, UserAccountRoles.Founder)]
        public async Task<IEnumerable<Character>> GetAll()
        {
            return await _repository.GetAsync();
        }

        public async Task<IEnumerable<Character>> GetAllForCurrentUser()
        {
            IEnumerable<Character> characters = new List<Character>();
            var userAccountId = (await _userAccountService.GetCurrentUser()).OnSuccess(async x =>
            {
                characters = await _repository.GetAsync(c => c.OwnerId == x.ID);
            });
            return characters;
        }

        [Security(UserAccountRoles.Admin, UserAccountRoles.Owner, UserAccountRoles.Founder)]
        public async Task DeleteCharacter(Guid id)
        {
            var character = (await _repository.GetAsync(x => x.Id == id)).FirstOrDefault();
            _repository.Delete(character);
            await _unitOfWork.SaveChangesAsync();
        }

        [Security(UserAccountRoles.Admin, UserAccountRoles.Owner, UserAccountRoles.Founder)]
        public async Task<Character> GetCharacter(Guid id)
        {
            return (await _repository.GetAsync(x => x.Id == id)).FirstOrDefault();
        }


    }
}
