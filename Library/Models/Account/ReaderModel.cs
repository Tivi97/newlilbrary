
using System.ComponentModel.DataAnnotations;

namespace Library.Models.Account
{
    
        public class ProfileModel
        {
            public ReaderModel Tourist { get; set; }
          //  public ExperienceModel Exp { get; set; }
        }

        public class ReaderModel
        {
            public int Id { get; set; }

        public string Fio { get; set; }

        public string Birthday { get; set; }

        public string PassportSeries { get; set; }

        public string PassportKod { get; set; }

        public string CertificateCode { get; set; }

        public string Email { get; set; }

        public string userRole { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }

    //________________________
    public string Sex { get; set; }
            public string Birthday { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
            public bool IsAdmin { get; set; }
            public bool IsCoach { get; set; }
            public bool IsBlocked { get; set; }
        }

        //public class ExperienceModel
        //{
        //    public int IdUser { get; set; }
        //    public int IdHike { get; set; }
        //}

        //public class HikeModel
        //{
        //    public string IsLead { get; set; }
        //    public string Name { get; set; }
        //    public string Start { get; set; }
        //    public string Finish { get; set; }
        //    public string Category { get; set; }
        //    public string Type { get; set; }
        //}
    
}
