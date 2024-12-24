namespace M13.InterviewProject.Interfaces
{
    /// <summary>
    /// Сервис для работы с правилами
    /// </summary>
    public interface IRuleService
    {
        /// <summary>
        /// Получить правило
        /// </summary>
        string GetRule(string site);

        /// <summary>
        /// Добавить правило
        /// </summary>
        void AddRule(string site, string rule);

        /// <summary>
        /// Удалить правило
        /// </summary>
        void DeleteRule(string site);
    }
}