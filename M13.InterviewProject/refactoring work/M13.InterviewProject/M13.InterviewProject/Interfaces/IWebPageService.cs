using System.Threading.Tasks;

namespace M13.InterviewProject.Interfaces
{
    /// <summary>
    /// Сервис для работы с веб страницами
    /// </summary>
    public interface IWebPageService
    {
        /// <summary>
        /// Получить текст старницы
        /// </summary>
        Task<string> GetPageTextAsync(string url, string xpath);
    }
}