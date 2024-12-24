using M13.InterviewProject.Interfaces;

namespace M13.InterviewProject.Models
{
    public struct SpellerErrorsModel : ISpellCheckError
    {
        public int Code { get; set; }
        public int Pos { get; set; }
        public int row { get; set; }
        public int col { get; set; }
        public int len { get; set; }
        public string Word { get; set; }
        public string[] s { get; set; }
    }
}