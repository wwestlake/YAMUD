using FluentResults;
using LagDaemon.YAMUD.API;
using LagDaemon.YAMUD.API.Services;
using LagDaemon.YAMUD.Model.Characters;
using System.Linq.Expressions;

namespace LagDaemon.YAMUD.WebAPI.Services.CharacterServices
{
    public class QuestService : IQuestService
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<Quest> _repoQuest;

        public QuestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repoQuest = _unitOfWork.GetRepository<Quest>();
        }

        public async Task<Result<Quest>> GetQuest(Guid id)
        {
            var result = (await _repoQuest.GetAsync(q => q.Id == id, null, "QuestGoal,Sections")).FirstOrDefault();
            return result != null ? Result.Ok(result) : Result.Fail<Quest>("Quest not found");
        }

        public async Task<Result<Quest>> CreateQuest(Quest quest)
        {
            _repoQuest.Insert(quest);
            _unitOfWork.SaveChanges();
            return Result.Ok(quest);
        }

        public async Task<Result<Quest>> UpdateQuest(Quest quest)
        {
            _repoQuest.Update(quest);
            _unitOfWork.SaveChanges();
            return Result.Ok(quest);
        }


        public async Task<Result> DeleteQuest(Guid id)
        {
            var quest = (await _repoQuest.GetAsync(q => q.Id == id)).FirstOrDefault();
            if (quest == null)
            {
                return Result.Fail("Quest not found");
            }

            _repoQuest.Delete(quest);
            _unitOfWork.SaveChanges();
            return Result.Ok();
        }

        public async Task<Result<IEnumerable<Quest>>> GetFilterQuests(Expression<Func<Quest, bool>> filter,
                       Func<IQueryable<Quest>, IOrderedQueryable<Quest>> orderBy,
                       string includeProperties = "QuestGoal,Sections")
        {
            var result = await _repoQuest.GetAsync(filter, orderBy, includeProperties);
            return Result.Ok(result);
        }

        public async Task<Result<IEnumerable<Quest>>> GetAllQuests()
        {
            var result = await _repoQuest.GetAsync();
            return Result.Ok(result);
        }

        public async Task<Result<IEnumerable<Quest>>> GetQuests(IQuerySpec<Quest> querySpec)
        {
            var result = await _repoQuest.GetAsync(querySpec);
            return Result.Ok(result);
        }

        public async Task<Result<Quest>> GetSingleQuest(Expression<Func<Quest, bool>> filter)
        {
            var result = await _repoQuest.GetSingleAsync(filter);
            return result != null ? Result.Ok(result) : Result.Fail<Quest>("Quest not found");
        }

        public async Task<Result<long>> CountQuests()
        {
            var result = await _repoQuest.CountAsync();
            return Result.Ok(result);
        }

    }
}
