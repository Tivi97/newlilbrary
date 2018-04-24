
using System.ComponentModel.DataAnnotations;

namespace Library.Models.Account
{
    
        public class ProfileModel
        {
            public ReaderModel Reader { get; set; }
         
        }

        public class ReaderModel
        {
            public int Id { get; set; }

        public string Fio { get; set; }

        public string Birthday { get; set; }

        public string PassportSeries { get; set; }

        public string PassportCode { get; set; }

        public string CertificateCode { get; set; }

        public string Email { get; set; }

        public string userRole { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

    }

          
}
