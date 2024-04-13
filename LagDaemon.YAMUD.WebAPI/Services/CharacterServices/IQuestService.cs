using FluentResults;
using LagDaemon.YAMUD.API.Services;
using LagDaemon.YAMUD.Model.Characters;
using System.Linq.Expressions;

namespace LagDaemon.YAMUD.WebAPI.Services.CharacterServices
{
    public interface IQuestService
    {
        Task<Result<long>> CountQuests();
        Task<Result<Quest>> CreateQuest(Quest quest);
        Task<Result> DeleteQuest(Guid id);
        Task<Result<Quest>> GetQuest(Guid id);
        Task<Result<IEnumerable<Quest>>> GetAllQuests();
        Task<Result<IEnumerable<Quest>>> GetQuests(IQuerySpec<Quest> querySpec);
        Task<Result<IEnumerable<Quest>>> GetFilterQuests(Expression<Func<Quest, bool>> filter, Func<IQueryable<Quest>, IOrderedQueryable<Quest>> orderBy, string includeProperties = "QuestGoal,Sections");
        Task<Result<Quest>> GetSingleQuest(Expression<Func<Quest, bool>> filter);
        Task<Result<Quest>> UpdateQuest(Quest quest);
    }
}
