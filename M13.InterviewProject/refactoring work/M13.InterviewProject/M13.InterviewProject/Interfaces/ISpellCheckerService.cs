using System.Collections.Generic;
using System.Threading.Tasks;

namespace M13.InterviewProject.Interfaces
{
    /// <summary>
    /// Сервис для проверки орфографии
    /// </summary>
    public interface ISpellCheckerService
    {
        /// <summary>
        /// Получение ошибок
        /// </summary>

        Task<IEnumerable<string>> GetErrorsAsync(string text);

        /// <summary>
        /// Получение колличества ошибок
        /// </summary>
        Task<int> GetErrorCountAsync(string text);
    }
}