using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Library_System
{
    class PublicVariables
    {
        public static string connectionServer = @"Data Source=Ammar;Initial Catalog=Library_System;Integrated Security=True";
        public static string currentlySignedInUsername = "";
        public static int currentlySignedInID;
        public static string registerName;
        public static string registerSurname;
        public static string registerMobileNumber;
        public static string registerUsername;
        public static string registerPassword;
        public static string registerRepeatPassword;
        public static int registerDefaultRank = 3;
        public static int registerRequestedRank;
    }
    //For Ranks
    public class RegisterUserRanks
    {
        public int Rank { get; set; }
        public string RankTitle { get; set; }
    }
    //For Ranks
}
