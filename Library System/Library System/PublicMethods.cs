using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
namespace Library_System
{
    class PublicMethods : MainWindow
    {
        //Main Information Method
        public static List<string> GettingInfoFromSql(string username)
        {
            try
            {
                List<string> rankAndSignedInInfo = new List<string>(new string[2]);
                string CheckQuery = "";
                if (username.Contains("@"))
                {
                    CheckQuery = "Select ID,Name,Surname,Rank,BooksLimit from Users Where Email = @username";
                }
                else if (username.All(char.IsDigit))
                {
                    CheckQuery = "Select ID,Name,Surname,Rank , BooksLimit from Users Where UniversityNumber = @username";
                }
                else
                {
                    MessageBox.Show("Error 20 : Label will be Erorr Please contact ADMIN");//if it comes here then a wrong username has been signed up
                    string rank = "3";
                    string signedInInfo = "Error";
                    string signedInID = "-1";
                    rankAndSignedInInfo = new List<string>(new string[] { rank, signedInInfo, signedInID });
                    return rankAndSignedInInfo;
                }
                SqlConnection rankCheckConnection = new SqlConnection(PublicVariables.connectionServer);
                rankCheckConnection.Open();
                SqlCommand rankCheckCommand = new SqlCommand(CheckQuery, rankCheckConnection);
                rankCheckCommand.Parameters.AddWithValue("@username", username);
                SqlDataAdapter rankCheckDataAdapter = new SqlDataAdapter(rankCheckCommand);
                DataTable rankCheckDataTable = new DataTable();
                rankCheckDataAdapter.Fill(rankCheckDataTable);
                DataRow[] rankCheckDataRow = rankCheckDataTable.Select();
                foreach (DataRow row in rankCheckDataRow)
                {
                    string rank = row["Rank"].ToString();
                    string signedInInfo = Regex.Replace(row["Name"].ToString(), @"\s+", "") + " " + Regex.Replace(row["Surname"].ToString(), @"\s+", "");
                    string signedInID = row["ID"].ToString();
                    string signedInBooksLimit = row["BooksLimit"].ToString();
                    rankAndSignedInInfo = new List<string>(new string[] { rank, signedInInfo, signedInID, signedInBooksLimit });
                }
                return rankAndSignedInInfo;
            }
            catch (System.Exception x)
            {
                List<string> rankAndSignedInInfo = new List<string>(new string[2]);
                MessageBox.Show("Error 20 : " + x.ToString()); MessageBox.Show("Error : Label will be Erorr");//if it comes here then a wrong username has been signed up
                string rank = "3";
                string signedInInfo = "Error";
                string signedInID = "-1";
                rankAndSignedInInfo = new List<string>(new string[] { rank, signedInInfo, signedInID });
                return rankAndSignedInInfo;
            }
        }
        //Login Methods
        public static bool LoginRules(string loginUsername, string loginPassword)
        {
            try
            {
                //signing in rules:
                if (loginUsername.Length == 0)
                {
                    MessageBox.Show("Username is empty.");
                    return false;
                }
                if (loginPassword.Length == 0)
                {
                    MessageBox.Show("Password is empty.");
                    return false;
                }
                bool isEmail = false;
                if (loginUsername.Contains('@'))
                {
                    isEmail = true;
                }
                else if (loginUsername.All(char.IsDigit))
                {
                    isEmail = false;
                }
                else
                {
                    MessageBox.Show("You should login with either your University ID or your Email.");
                    return false;
                }
                if (LoginProcess(loginUsername, Encryption.ComputeSha256Hash(loginPassword), isEmail))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error 21 : " + x.ToString());
                return false;
            }
        }
        static bool LoginProcess(string loginUsername, string loginHashedPassword, bool isEmail)
        {
            try
            {
                string loginQuery = "";
                if (isEmail)
                {
                    loginQuery = "Select Name , Surname , Email , Password from Users where Email =@loginUsername and Password =@Password ";
                }
                else
                {
                    loginQuery = "Select Name , Surname , UniversityNumber , Password from Users where UniversityNumber =@loginUsername and Password =@Password ";
                }
                SqlConnection loginConnection = new SqlConnection(PublicVariables.connectionServer);
                SqlCommand loginCommand = new SqlCommand(loginQuery, loginConnection);
                loginCommand.Parameters.AddWithValue("@loginUsername", loginUsername);
                loginCommand.Parameters.AddWithValue("@Password", loginHashedPassword);
                loginConnection.Open();
                SqlDataAdapter loginDataAdapter = new SqlDataAdapter(loginCommand);
                DataTable loginDatatable = new DataTable();
                loginDataAdapter.Fill(loginDatatable);
                if (loginDatatable.Rows.Count == 1)
                {
                    DataRow[] loginDataRow = loginDatatable.Select();
                    foreach (DataRow item in loginDataRow)
                    {
                        MessageBox.Show($"Login Successful! Welcome { item["Name"].ToString().Trim()} { item["Surname"].ToString().Trim()} ");
                    }
                    loginConnection.Close();
                    return true;
                }
                else
                {
                    MessageBox.Show("Email or Password wrong. Please check them and try again .");
                    loginConnection.Close();
                    return false;
                }
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error 22 : " + x.ToString());
                return false;
            }
        }
        //Register Methods
        public static bool RegisterationRules(string registerName, string registerSurname, string registerMobileNumber, string registerUsername, string registerPassword, string registerRepeatPassword, int registerDefaultRank, int registerRequestedRank)
        {
            try
            {
                if (registerName.Length == 0)
                {
                    MessageBox.Show("Please Enter your name.");
                    return false;
                }
                if (registerSurname.Length == 0)
                {
                    MessageBox.Show("Please Enter your surname.");
                    return false;
                }
                if (registerUsername.Length == 0)
                {
                    MessageBox.Show("Username is empty.");
                    return false;
                }
                if (registerUsername.Length < 3)
                {
                    MessageBox.Show("Username can't be smaller than 3 characters.");
                    return false;
                }
                if (registerUsername.Any(char.IsWhiteSpace))
                {
                    MessageBox.Show("Username can't contain any space. ");
                    return false;
                }
                if (registerMobileNumber.Length != 0)
                {
                    Regex phoneNumpattern = new Regex(@"^5(0[5-7]|[3-5]\d) ?\d{3} ?\d{4}$");
                    if (phoneNumpattern.IsMatch(registerMobileNumber))
                    {
                    }
                    else
                    {
                        MessageBox.Show("Make sure you wrote your Mobile Number correctly and without '0' at start (You can leave it empty)");
                        return false;
                    }
                }
                if (registerPassword.Length == 0)
                {
                    MessageBox.Show("Password is empty.");
                    return false;
                }
                if (registerPassword.Length < 8)
                {
                    MessageBox.Show("Password can't be smaller than 8 characters.");
                    return false;
                }
                if (!registerPassword.Any(char.IsUpper))
                {
                    MessageBox.Show("Password must contain at least one upper character.");
                    return false;
                }
                if (!registerPassword.Any(char.IsLower))
                {
                    MessageBox.Show("Password must contain at least one lower character.");
                    return false;
                }
                if (!registerPassword.Any(char.IsNumber))
                {
                    MessageBox.Show("Password must contain at least one Number ");
                    return false;
                }
                if (registerPassword.Any(char.IsWhiteSpace))
                {
                    MessageBox.Show("Password can't contain any space. Plesae erase them.");
                    return false;
                }
                if (registerPassword != registerRepeatPassword)
                {
                    MessageBox.Show("The password and repeat password don't match . Make sure you wrote it correctly");
                    return false;
                }
                if (registerRequestedRank == -1)
                {
                    MessageBox.Show("Please choose your rank");
                    return false;
                }
                bool isEmail = false;
                if (registerUsername.Contains('@'))
                {
                    isEmail = true;
                }
                else if (registerUsername.All(char.IsDigit))
                {
                    isEmail = false;
                }
                else
                {
                    MessageBox.Show("You should Register with either your University ID or your Email.");
                    isEmail = false;
                    return false;
                }
                if (RegisterExisitingUsernameCheckingAndProcess(registerName, registerSurname, registerMobileNumber, registerUsername, Encryption.ComputeSha256Hash(registerPassword), registerDefaultRank, isEmail, registerRequestedRank))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error 23 : " + x.ToString());
                return false;
            }
        }
        public static bool RegisterExisitingUsernameCheckingAndProcess(string registerName, string registerSurname, string registerMobileNumber, string registerUsername, string registerPassword, int registerDefaultRank, bool isEmail, int registerRequestedRank)
        {
            try
            {
                string registerCheckQuery = "";
                string registerQuery = "";
                if (isEmail)
                {
                    registerCheckQuery = "Select Email from Users where Email = @registerUsername ";
                    registerQuery = $"insert into Users (Email,Password,Rank,Name,Surname,MobileNumber,RequestedRank,RequestedRankName) values (@registerUsername,@registerPassword,@registerDefaultRank,@registerName,@registerSurname,@registerMobileNumber,@registerRequestedRank,@RequestedRankName)";
                }
                else
                {
                    registerCheckQuery = "Select UniversityNumber from Users where UniversityNumber = @registerUsername ";
                    registerQuery = $"insert into Users (UniversityNumber,Password,Rank,Name,Surname,MobileNumber,RequestedRank,RequestedRankName) values(@registerUsername,@registerPassword,@registerDefaultRank,@registerName,@registerSurname,@registerMobileNumber,@registerRequestedRank,@RequestedRankName)";
                }
                SqlConnection registerCheckConnection = new SqlConnection(PublicVariables.connectionServer);
                SqlCommand registerCheckCommand = new SqlCommand(registerCheckQuery, registerCheckConnection);
                registerCheckCommand.Parameters.AddWithValue("@registerUsername", registerUsername);
                registerCheckConnection.Open();
                SqlDataAdapter registerCheckDataAdapter = new SqlDataAdapter(registerCheckCommand);
                DataTable registerCheckDatatable = new DataTable();
                registerCheckDataAdapter.Fill(registerCheckDatatable);
                if (registerCheckDatatable.Rows.Count >= 1)
                {
                    if (isEmail)
                    {
                        MessageBox.Show("Email is already in use.Sign in or try using another email or Register with you University ID");
                    }
                    else
                    {
                        MessageBox.Show("University Number is already in use.Sign in or make sure you are using your own university ID ");
                    }
                    return false;
                }
                else
                {
                    SqlConnection registerConnection = new SqlConnection(PublicVariables.connectionServer);
                    SqlCommand registerCommand = new SqlCommand(registerQuery, registerConnection);
                    registerCommand.Parameters.AddWithValue("@registerUsername", registerUsername);
                    registerCommand.Parameters.AddWithValue("@registerPassword", registerPassword);
                    registerCommand.Parameters.AddWithValue("@registerDefaultRank", registerDefaultRank);
                    registerCommand.Parameters.AddWithValue("@registerName", registerName);
                    registerCommand.Parameters.AddWithValue("@registerSurname", registerSurname);
                    registerCommand.Parameters.AddWithValue("@registerMobileNumber", registerMobileNumber);
                    registerCommand.Parameters.AddWithValue("@registerRequestedRank", registerRequestedRank);
                    if (registerRequestedRank == 2)
                    {
                        registerCommand.Parameters.AddWithValue("@RequestedRankName", "Wants to be Teacher");
                    }
                    else if (registerRequestedRank == 1)
                    {
                        registerCommand.Parameters.AddWithValue("@RequestedRankName", "Wants to be Admin");
                    }
                    else
                    {
                        registerCommand.Parameters.AddWithValue("@RequestedRankName", "Student");
                    }
                    registerConnection.Open();
                    registerCommand.ExecuteNonQuery();
                    registerConnection.Close();
                }
                if (registerRequestedRank == 1 || registerRequestedRank == 2)
                {
                    MessageBox.Show("Your rank will be checked by an admin and activated shortly.You will be signed in now as student");
                }
                MessageBox.Show($"Welcome {registerName} {registerSurname}");
                return true;
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error 24 :" + x.ToString());
                return false;
            }
        }
        //Books Methods
        public static bool RentingProcess(string currentSignedInUsername, int currentSignedInID, int rentedBookID, string bookTitle, string rentedbookAuthor, string rentedbookCategory, int rank)
        {
            try
            {
                SqlConnection bookQuantityCheckConnection = new SqlConnection(PublicVariables.connectionServer);
                string bookQuantityCheckQuery = "Select quantity from books where  ID = @ID";
                SqlCommand bookQuantityCheckCommand = new SqlCommand(bookQuantityCheckQuery, bookQuantityCheckConnection);
                bookQuantityCheckCommand.Parameters.AddWithValue("@ID", rentedBookID);
                SqlDataAdapter bookQuantityCheckDataAdapter = new SqlDataAdapter(bookQuantityCheckCommand);
                DataTable bookQuantityCheckDataTable = new DataTable();
                bookQuantityCheckDataAdapter.Fill(bookQuantityCheckDataTable);
                DataRow[] bookQuantityCheckDataRow = bookQuantityCheckDataTable.Select();
                int quantity = 0;
                foreach (DataRow row in bookQuantityCheckDataRow)
                {
                    quantity = int.Parse(row["Quantity"].ToString().Trim());
                }
                if (quantity > 0)
                {
                }
                else
                {
                    MessageBox.Show("Sorry this book is currently out of stock.");
                    return false;
                }
                string userLimitCheckQuery = "";
                string rentingProcessQuery = "";
                if (currentSignedInUsername.Contains("@"))
                {
                    userLimitCheckQuery = "Select * from Users BooksLimit where Email =  @currentSignedIn";
                    rentingProcessQuery =
                        "update Books set TakenBooks = TakenBooks + 1 where ID = @ID " +
                        "update Users set[TotalTakenBooks] = [TotalTakenBooks] + 1 where Email = @currentSignedIn " +
                        "update Users set[CurrentTakenBooks] = [CurrentTakenBooks] + 1 where Email = @currentSignedIn " +
                        "update Users set[BooksLimit] = [BooksLimit] - 1 where Email = @currentSignedIn ";
                }
                else
                {
                    userLimitCheckQuery = "Select * from Users BooksLimit where [UniversityNumber] =  @currentSignedIn";
                    rentingProcessQuery =
                        "update Books set TakenBooks = TakenBooks + 1 where  ID = @ID  " +
                        "update Users set[TotalTakenBooks] = [TotalTakenBooks] + 1 where UniversityNumber = @currentSignedIn " +
                        "update Users set[CurrentTakenBooks] = [CurrentTakenBooks] + 1 where UniversityNumber = @currentSignedIn " +
                        "update Users set[BooksLimit] = [BooksLimit] - 1 where UniversityNumber = @currentSignedIn";
                }
                SqlConnection userLimitCheckConnection = new SqlConnection(PublicVariables.connectionServer);
                SqlCommand userLimitCheckCommand = new SqlCommand(userLimitCheckQuery, userLimitCheckConnection);
                userLimitCheckCommand.Parameters.AddWithValue("@currentSignedIn", currentSignedInUsername);
                SqlDataAdapter userLimitCheckDataAdapter = new SqlDataAdapter(userLimitCheckCommand);
                DataTable userLimitCheckDataTable = new DataTable();
                userLimitCheckDataAdapter.Fill(userLimitCheckDataTable);
                DataRow[] userLimitCheckDataRow = userLimitCheckDataTable.Select();
                string currentLimit = "0";
                int currentLimitNumber = 0;
                foreach (DataRow row in userLimitCheckDataRow)
                {
                    currentLimit = (row["BooksLimit"].ToString().Trim());
                }
                currentLimitNumber = int.Parse(currentLimit.Trim());
                if (currentLimitNumber > 0)
                {
                    SqlConnection rentingProcessConnection = new SqlConnection(PublicVariables.connectionServer);
                    SqlCommand rentingProcessCommand = new SqlCommand(rentingProcessQuery, rentingProcessConnection);
                    rentingProcessCommand.Parameters.AddWithValue("currentSignedIn", currentSignedInUsername);
                    rentingProcessCommand.Parameters.AddWithValue("@ID", rentedBookID);
                    rentingProcessConnection.Open();
                    rentingProcessCommand.ExecuteNonQuery();
                    rentingProcessConnection.Close();
                    MessageBox.Show($"you have successfully rented {bookTitle}, You have 7 days to return it");
                }
                else if (currentLimitNumber < 0)
                {
                    SqlConnection rentingProcessConnection = new SqlConnection(PublicVariables.connectionServer);
                    SqlCommand rentingProcessCommand = new SqlCommand(rentingProcessQuery, rentingProcessConnection);
                    rentingProcessCommand.Parameters.AddWithValue("currentSignedIn", currentSignedInUsername); rentingProcessCommand.Parameters.AddWithValue("@ID", rentedBookID);
                    rentingProcessConnection.Open();
                    rentingProcessCommand.ExecuteNonQuery();
                    rentingProcessConnection.Close();
                    MessageBox.Show($"you have successfully rented {bookTitle} , You can return it anytime you want.");
                }
                else
                {
                    MessageBox.Show("Sorry you can't rent this book because because you passed your limit. Please contact the admin to increase your limit or return your current taken books.");
                    return false;
                }
                SavingRentedBooks(bookTitle, currentSignedInUsername, currentSignedInID, rentedBookID, rentedbookAuthor, rentedbookCategory, rank);
                return true;
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error 25 : " + x.ToString());
                return false;
            }
        }
        public static DataTable SearchBooks(string SearchText, bool instockonly, bool searchByFirstLetter)
        {
            try
            {
                string booksSearchQuery = "";
                if (instockonly && searchByFirstLetter)//all checked SEARCH
                {
                    booksSearchQuery = "SELECT [ID],[Title],[NumberOfPages],[Category],[Author],[PublishingYear],[Quantity] FROM[Library_System].[dbo].[Books] where Quantity != 0 AND ([Title] LIKE @SearchText)";
                }
                else if (instockonly && !searchByFirstLetter)//only instock search SEARCH
                {
                    booksSearchQuery = "SELECT [ID],[Title],[NumberOfPages],[Category],[Author],[PublishingYear],[Quantity] FROM[Library_System].[dbo].[Books] where Quantity != 0 AND ([Title] LIKE @SearchText or [Category] LIKE @SearchText or [Author] LIKE @SearchText ) ";
                }
                else if (!instockonly && searchByFirstLetter)//only first letter  SEARCH
                {
                    booksSearchQuery = "SELECT [ID],[Title],[NumberOfPages],[Category],[Author],[PublishingYear],[Quantity] FROM[Library_System].[dbo].[Books] where [Title] LIKE @SearchText";
                }
                else if (!instockonly && !searchByFirstLetter)//NORMAL SEARCH
                {
                    booksSearchQuery = "SELECT [ID],[Title],[NumberOfPages],[Category],[Author],[PublishingYear],[Quantity] FROM[Library_System].[dbo].[Books] where [Title] LIKE @SearchText or [Category] LIKE @SearchText or [Author] LIKE @SearchText";
                }
                using (SqlConnection booksSearchSqlConnection = new SqlConnection(PublicVariables.connectionServer))
                {
                    booksSearchSqlConnection.Open();
                    SqlCommand booksSearchCommand = new SqlCommand(booksSearchQuery, booksSearchSqlConnection);
                    if (searchByFirstLetter)
                    {
                        booksSearchCommand.Parameters.AddWithValue("@SearchText", SearchText + "%");
                    }
                    else
                    {
                        booksSearchCommand.Parameters.AddWithValue("@SearchText", "%" + SearchText + "%");
                    }
                    SqlDataAdapter booksSearchDataAdapter = new SqlDataAdapter(booksSearchCommand);
                    DataTable booksSearchDataTable = new DataTable();
                    booksSearchDataAdapter.Fill(booksSearchDataTable);
                    return booksSearchDataTable;
                }
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error 26 : " + x.ToString());
                DataTable booksSearchDataTable = new DataTable();
                return booksSearchDataTable;
            }
        }
        public static void SavingRentedBooks(string bookTitle, string currentSignedInUsername, int currentSignedInID, int rentedBookID, string rentedbookAuthor, string rentedbookCategory, int rank)
        {
            try
            {
                string savingRentedBooksQuery = "";
                if (currentSignedInUsername.Contains("@"))
                {
                    savingRentedBooksQuery = "INSERT INTO TakenBooks (TakenBookID,TakenBookTitle,TakenByEmail,TakenByID,TakenBookAuthor,TakenBookCategory , Rank) VALUES (@rentedBookID,@bookTitle,@currentSignedInUsername,@currentSignedInID ,@rentedbookAuthor , @rentedbookCategory ,@rank )";
                }
                else if (currentSignedInUsername.All(char.IsDigit))
                {
                    savingRentedBooksQuery = "INSERT INTO TakenBooks (TakenBookID,TakenBookTitle,TakenByUniversityNumber,TakenByID,TakenBookAuthor,TakenBookCategory , Rank) VALUES (@rentedBookID,@bookTitle,@currentSignedInUsername,@currentSignedInID ,@rentedbookAuthor , @rentedbookCategory, @rank )";
                }
                SqlConnection savingRentedBooksConnection = new SqlConnection(PublicVariables.connectionServer);
                SqlCommand savingRentedBooksCommand = new SqlCommand(savingRentedBooksQuery, savingRentedBooksConnection);
                savingRentedBooksCommand.Parameters.AddWithValue("@bookTitle", bookTitle);
                savingRentedBooksCommand.Parameters.AddWithValue("@currentSignedInUsername", currentSignedInUsername);
                savingRentedBooksCommand.Parameters.AddWithValue("@rentedBookID", rentedBookID);
                savingRentedBooksCommand.Parameters.AddWithValue("@currentSignedInID", currentSignedInID);
                savingRentedBooksCommand.Parameters.AddWithValue("@rentedbookAuthor", rentedbookAuthor);
                savingRentedBooksCommand.Parameters.AddWithValue("@rentedbookCategory", rentedbookCategory);
                savingRentedBooksCommand.Parameters.AddWithValue("@rank", rank);
                savingRentedBooksConnection.Open();
                savingRentedBooksCommand.ExecuteNonQuery();
                savingRentedBooksConnection.Close();
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error 27 : " + x.ToString());
                return;
            }
        }
        public static DataTable StudentLateNotification(string username)
        {
            try
            {
                string notificationQuery = "";
                if (username.Contains("@"))
                {
                    notificationQuery = "SELECT ID , TakenBookID , TakenBookTitle , TakenByID , TakenByEmail, TakenTime ,RemainingLimit,Rank  FROM [Library_System].[dbo].[TakenBooks] where RemainingLimit < 0 and TakenByEmail = @username And Rank = 3 And [IsReturned] = 0 AND [IsReturnedApproved] = 0 ";
                }
                else
                {
                    notificationQuery = "SELECT ID , TakenBookID , TakenBookTitle , TakenByID , TakenByUniversityNumber, TakenTime ,RemainingLimit,Rank  FROM [Library_System].[dbo].[TakenBooks] where RemainingLimit < 0 and TakenByUniversityNumber = @username And Rank = 3 And [IsReturned] = 0 AND [IsReturnedApproved] = 0 ";
                }
                SqlConnection notificationConnection = new SqlConnection(PublicVariables.connectionServer);
                notificationConnection.Open();
                SqlCommand notificationCommand = new SqlCommand(notificationQuery, notificationConnection);
                notificationCommand.Parameters.AddWithValue("@username", username);
                SqlDataAdapter notificationDataAdapter = new SqlDataAdapter(notificationCommand);
                DataTable notificationDataTable = new DataTable();
                notificationDataAdapter.Fill(notificationDataTable);
                DataRow[] notificationDataRow = notificationDataTable.Select();
                foreach (DataRow row in notificationDataRow)
                {
                    string rank = row["Rank"].ToString();
                    string takenBookTitle = row["takenBookTitle"].ToString();
                    int RemainingLimit = int.Parse(row["RemainingLimit"].ToString());
                    if (rank == "3" && RemainingLimit < 0)
                    {
                        MessageBox.Show($"You should return \"{takenBookTitle.Trim()}\" Book because you passed your rental time limit. Please return it. ");
                    }
                }
                return notificationDataTable;
            }
            catch (Exception x)
            {
                MessageBox.Show("Error ** : " + x.ToString());
                DataTable notificationDataTable = new DataTable();
                return notificationDataTable;
            }
        }
        //MyBooks Methods
        public static DataTable SearchMyBooks(string SearchText, bool searchByFirstLetter, int currentlySignedInID, string currentlySignedInUsername)
        {
            try
            {
                string SearchMyBooksQuery = "";
                if (currentlySignedInUsername.Contains("@"))
                {
                    if (searchByFirstLetter)
                    {
                        SearchMyBooksQuery = "SELECT [ID],[TakenBookID],[TakenBookTitle],TakenBookAuthor,TakenBookCategory,[TakenTime] FROM[Library_System].[dbo].[TakenBooks] Where [TakenByEmail] = @currentlySignedInUsername AND [TakenByID] = @currentlySignedInID AND IsReturned = 0 AND ([TakenBookTitle] LIKE @SearchText )";
                    }
                    else
                    {
                        SearchMyBooksQuery = "SELECT [ID],[TakenBookID],[TakenBookTitle],TakenBookAuthor,TakenBookCategory,[TakenTime] FROM[Library_System].[dbo].[TakenBooks] Where [TakenByEmail] = @currentlySignedInUsername AND IsReturned = 0 AND  [TakenByID] = @currentlySignedInID AND ([TakenBookTitle] LIKE @SearchText or [TakenBookAuthor] LIKE @SearchText or [TakenBookCategory] LIKE @SearchText )";
                    }
                }
                else
                {
                    if (searchByFirstLetter)
                    {
                        SearchMyBooksQuery = "SELECT [ID],[TakenBookID],[TakenBookTitle],TakenBookAuthor,TakenBookCategory,[TakenTime] FROM[Library_System].[dbo].[TakenBooks] Where [TakenByUniversityNumber] = @currentlySignedInUsername AND IsReturned = 0 AND [TakenByID] = @currentlySignedInID AND ([TakenBookTitle] LIKE @SearchText )";
                    }
                    else
                    {
                        SearchMyBooksQuery = "SELECT [ID],[TakenBookID],[TakenBookTitle],TakenBookAuthor,TakenBookCategory,[TakenTime] FROM[Library_System].[dbo].[TakenBooks] Where [TakenByUniversityNumber] = @currentlySignedInUsername AND IsReturned = 0 AND [TakenByID] = @currentlySignedInID AND ([TakenBookTitle] LIKE @SearchText or [TakenBookAuthor] LIKE @SearchText or [TakenBookCategory] LIKE @SearchText)";
                    }
                }
                using (SqlConnection SearchMyBooksSqlConnection = new SqlConnection(PublicVariables.connectionServer))
                {
                    SearchMyBooksSqlConnection.Open();
                    SqlCommand SearchMyBooksCommand = new SqlCommand(SearchMyBooksQuery, SearchMyBooksSqlConnection);
                    if (searchByFirstLetter)
                    {
                        SearchMyBooksCommand.Parameters.AddWithValue("@SearchText", SearchText + "%");
                    }
                    else
                    {
                        SearchMyBooksCommand.Parameters.AddWithValue("@SearchText", "%" + SearchText + "%");
                    }
                    SearchMyBooksCommand.Parameters.AddWithValue("@currentlySignedInUsername", currentlySignedInUsername);
                    SearchMyBooksCommand.Parameters.AddWithValue("@currentlySignedInID", currentlySignedInID);
                    SqlDataAdapter SearchMyBooksDataAdapter = new SqlDataAdapter(SearchMyBooksCommand);
                    DataTable SearchMyBooksDataTable = new DataTable();
                    SearchMyBooksDataAdapter.Fill(SearchMyBooksDataTable);
                    return SearchMyBooksDataTable;
                }
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error 28 : " + x.ToString());
                DataTable SearchMyBooksDataTable = new DataTable();
                return SearchMyBooksDataTable;
            }
        }
        public static void ReturnBooksProcess(int ID, int selectedBookID)
        {
            try
            {
                string returnBooksQuery = "";
                if (PublicVariables.currentlySignedInUsername.Contains("@"))
                {
                    returnBooksQuery = "update TakenBooks set IsReturned = 1 , [ReturnedTime] = getdate()  where TakenByEmail = @currentlySignedInUsername AND TakenByID = @currentlySignedInID AND ID = @ID AND TakenBookID = @selectedBookID";
                }
                else
                {
                    returnBooksQuery = "update TakenBooks set IsReturned = 1 , [ReturnedTime] = getdate() where TakenByUniversityNumber = @currentlySignedInUsername AND TakenByID = @currentlySignedInID  AND ID = @ID AND TakenBookID = @selectedBookID";
                }
                SqlConnection returnBooksConnection = new SqlConnection(PublicVariables.connectionServer);
                SqlCommand returnBooksCommand = new SqlCommand(returnBooksQuery, returnBooksConnection);
                returnBooksCommand.Parameters.AddWithValue("@currentlySignedInUsername", PublicVariables.currentlySignedInUsername);
                returnBooksCommand.Parameters.AddWithValue("@currentlySignedInID", PublicVariables.currentlySignedInID);
                returnBooksCommand.Parameters.AddWithValue("@ID", ID);
                returnBooksCommand.Parameters.AddWithValue("@selectedBookID", selectedBookID);
                returnBooksConnection.Open();
                returnBooksCommand.ExecuteNonQuery();
                returnBooksConnection.Close();
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error 29 : " + x.ToString());
            }
        }
        //RankApproval Methods 
        public static void RankApproval(int SelectedID, bool isAdmin)
        {
            try
            {
                string rankApprovalQuery;
                if (isAdmin)
                {
                    rankApprovalQuery = "update Users set Rank = 1, BooksLimit = -5 , RequestedRankName = 'Rank = Admin' where ID = @SelectedID";
                }
                else
                {
                    rankApprovalQuery = "update Users set Rank = 2, BooksLimit = -5 , RequestedRankName = 'Rank = Teacher' where ID = @SelectedID ";
                }
                SqlConnection rankApprovalConnection = new SqlConnection(PublicVariables.connectionServer);
                SqlCommand rankApprovalCommand = new SqlCommand(rankApprovalQuery, rankApprovalConnection);
                rankApprovalCommand.Parameters.AddWithValue("@SelectedID", SelectedID);
                rankApprovalConnection.Open();
                rankApprovalCommand.ExecuteNonQuery();
                rankApprovalConnection.Close();
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error 30 : " + x.ToString());
            }
        }
        public static DataTable RankRequestedShowing(string search)
        {
            try
            {
                string RefreshUsersRequsertedRankQuery = "";
                if (string.IsNullOrWhiteSpace(search))
                {
                    RefreshUsersRequsertedRankQuery = $"SELECT [ID],[UniversityNumber],[Email],[RequestedRankName],[Name],[Surname],[MobileNumber],[RegisterationTime] FROM[Library_System].[dbo].[Users] where RequestedRank != 3 AND rank = 3";
                }
                else if (search.All(char.IsDigit))
                {
                    RefreshUsersRequsertedRankQuery = $"SELECT [ID],[UniversityNumber],[RequestedRankName],[Name],[Surname],[MobileNumber],[RequestedRank],[RegisterationTime] FROM[Library_System].[dbo].[Users] where RequestedRank != 3 AND rank = 3 AND (ID Like @search  or UniversityNumber like @search)";
                }
                else 
                {
                    RefreshUsersRequsertedRankQuery = $"SELECT [ID],[Email],[RequestedRankName],[Name],[Surname],[MobileNumber],[RequestedRank],[RegisterationTime] FROM[Library_System].[dbo].[Users] where Email Like @search or Name Like @search or Surname Like @search AND RequestedRank != 3 AND  rank = 3";
                }
                SqlConnection RefreshUsersRequsertedRankSqlConnection = new SqlConnection(PublicVariables.connectionServer);
                SqlCommand RefreshUsersRequsertedRankCommand = new SqlCommand(RefreshUsersRequsertedRankQuery, RefreshUsersRequsertedRankSqlConnection);
                if (string.IsNullOrWhiteSpace(search))
                {
                    RefreshUsersRequsertedRankCommand.Parameters.AddWithValue("@search", search);
                }
                else if (search.All(char.IsDigit))
                {
                    RefreshUsersRequsertedRankCommand.Parameters.AddWithValue("@search","%" + int.Parse(search) + "%");
                }
                else
                {
                    RefreshUsersRequsertedRankCommand.Parameters.AddWithValue("@search", "%" + search + "%");
                }
                RefreshUsersRequsertedRankSqlConnection.Open();
                SqlDataAdapter RefreshUsersRequsertedRankDataAdapter = new SqlDataAdapter(RefreshUsersRequsertedRankCommand);
                DataTable RefreshUsersRequsertedRankDataTable = new DataTable();
                RefreshUsersRequsertedRankDataAdapter.Fill(RefreshUsersRequsertedRankDataTable);
                RefreshUsersRequsertedRankSqlConnection.Close();
                return RefreshUsersRequsertedRankDataTable;
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error 31 : " + x.ToString());
                DataTable RefreshUsersRequsertedRankDataTable = new DataTable();
                return RefreshUsersRequsertedRankDataTable;
            }
        }
        //BooksApproval Methods
        public static DataTable ReturnedBookApprovalSearchAndRefresh(string search)
        {
            try
            {
                string ReturnedBookApprovalSearchAndRefreshQuery = "SELECT [ID],[TakenBookID],[TakenBookTitle],[TakenBookAuthor],[TakenBookCategory],[TakenByID],[TakenByEmail],[TakenByUniversityNumber],[TakenTime],[ReturnedTime]FROM[Library_System].[dbo].[TakenBooks] where ( IsReturned = 1 And IsReturnedApproved = 0 ) And  (TakenBookTitle like @search or TakenBookID like @search or ID like @search or TakenByEmail like @search or TakenByUniversityNumber like @search ) ";
                SqlConnection ReturnedBookApprovalSearchAndRefreshConnection = new SqlConnection(PublicVariables.connectionServer);
                SqlCommand ReturnedBookApprovalSearchAndRefreshCommand = new SqlCommand(ReturnedBookApprovalSearchAndRefreshQuery, ReturnedBookApprovalSearchAndRefreshConnection);
                ReturnedBookApprovalSearchAndRefreshCommand.Parameters.AddWithValue("@search", "%" + search + "%");
                SqlDataAdapter ReturnedBookApprovalSearchAndRefreshDataAdapter = new SqlDataAdapter(ReturnedBookApprovalSearchAndRefreshCommand);
                DataTable ReturnedBookApprovalSearchAndRefreshDatatable = new DataTable();
                ReturnedBookApprovalSearchAndRefreshDataAdapter.Fill(ReturnedBookApprovalSearchAndRefreshDatatable);
                return ReturnedBookApprovalSearchAndRefreshDatatable;
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error 32 : " + x.ToString());
                DataTable ReturnedBookApprovalSearchAndRefreshDatatable = new DataTable();
                return ReturnedBookApprovalSearchAndRefreshDatatable;
            }
        }
        public static void ApprovingReturnedBook(int ID, int takenBookID, int takenByID)
        {
            try
            {
                string approvingReturnedBookQuery = "";
                if (true)
                {
                    approvingReturnedBookQuery = "update TakenBooks set IsReturnedApproved = 1 , [ReturnedApprovalTime] = getdate() where ID = @ID " +
                        "update Books set TakenBooks = TakenBooks - 1 where ID = @takenBookID " +
                        "update Users set [CurrentTakenBooks] = [CurrentTakenBooks] - 1  Where ID = @takenByID " +
                        "UPDATE Users set BooksLimit = CASE WHEN BooksLimit < 2 THEN  BooksLimit + 1  WHEN BooksLimit > 2 THEN BooksLimit - 1 when BooksLimit = 2 then 2 When BooksLimit < 0 then -5 end WHERE ID = @takenByID";
                }
                SqlConnection approvingReturnedBookConnection = new SqlConnection(PublicVariables.connectionServer);
                SqlCommand approvingReturnedBookCommand = new SqlCommand(approvingReturnedBookQuery, approvingReturnedBookConnection);
                approvingReturnedBookCommand.Parameters.AddWithValue("@ID", ID);
                approvingReturnedBookCommand.Parameters.AddWithValue("@takenBookID", takenBookID);
                approvingReturnedBookCommand.Parameters.AddWithValue("@takenByID", takenByID);
                approvingReturnedBookConnection.Open();
                approvingReturnedBookCommand.ExecuteNonQuery();
                approvingReturnedBookConnection.Close();
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error 33 : " + x.ToString());
            }
        }
        //ALL Users METHODS
        public static SqlDataAdapter SearchAllUsersAdmin(string search)
        {
            try
            {
                string searchQuery = "";
                if (string.IsNullOrEmpty(search))
                {
                    searchQuery = "SELECT [ID],[UniversityNumber],[Email],[Rank] ,[Name],[Surname] ,[MobileNumber] ,[RegisterationTime] ,[TotalTakenBooks] ,[CurrentTakenBooks] ,[BooksLimit] FROM[Library_System].[dbo].[Users] ";
                }
                else
                {
                    searchQuery = "SELECT [ID],[UniversityNumber] ,[Email]  ,[Rank] ,[Name],[Surname] ,[MobileNumber] ,[RegisterationTime] ,[TotalTakenBooks] ,[CurrentTakenBooks] ,[BooksLimit] FROM[Library_System].[dbo].[Users] where ID like @search or UniversityNumber like @search or Email like @search or Password like @search or Rank like @search or Name like @search or Surname like @search or MobileNumber like @search or TotalTakenBooks like @search or CurrentTakenBooks like @search or BooksLimit like @search";
                }
                SqlConnection searchConnection = new SqlConnection(PublicVariables.connectionServer);
                SqlCommand searchCommand = new SqlCommand(searchQuery, searchConnection);
                searchCommand.Parameters.AddWithValue("@search", "%" + search + "%");
                SqlDataAdapter searchDataAdapter = new SqlDataAdapter(searchCommand);
                return searchDataAdapter;
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error 34 : " + x.ToString());
                SqlDataAdapter searchDataAdapter = new SqlDataAdapter("select Title from Books", PublicVariables.connectionServer);
                return searchDataAdapter;
            }
        }
        public static void DeletingUser(int userID)
        {
            try
            {
                string approvingReturnedBookQuery = "";
                approvingReturnedBookQuery = "Delete FROM [Library_System].[dbo].[Users] where ID = @id";
                SqlConnection approvingReturnedBookConnection = new SqlConnection(PublicVariables.connectionServer);
                SqlCommand approvingReturnedBookCommand = new SqlCommand(approvingReturnedBookQuery, approvingReturnedBookConnection);
                approvingReturnedBookCommand.Parameters.AddWithValue("@id", userID);
                approvingReturnedBookConnection.Open();
                approvingReturnedBookCommand.ExecuteNonQuery();
                approvingReturnedBookConnection.Close();
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error 85 : " + x.ToString());
            }
        }
        //All Books Methods 
        public static SqlDataAdapter SearchAllBooksAdmin(string search)
        {
            SqlDataAdapter searchAllBooksAdminDataAdapter = new SqlDataAdapter();
            try
            {
                SqlConnection searchAllBooksAdminConnection = new SqlConnection(PublicVariables.connectionServer);
                string searchAllBooksAdminQuery = "Select * from Books where [Title] like @search or [Category] like @search or [Author] like @search";
                SqlCommand searchAllBooksAdminCommand = new SqlCommand(searchAllBooksAdminQuery, searchAllBooksAdminConnection);
                searchAllBooksAdminCommand.Parameters.AddWithValue("@search", "%" + search + "%");
                searchAllBooksAdminDataAdapter = new SqlDataAdapter(searchAllBooksAdminCommand);
                return searchAllBooksAdminDataAdapter;
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 81 : " + x.ToString());
                DataTable searchAllBooksAdminDatatable = new DataTable();
                return searchAllBooksAdminDataAdapter;
            }
        }
        public static bool AddingNewBook(string title, int numberOfPages, string author, string category, DateTime date, int quantity)
        {
            try
            {
                SqlConnection addingNewBookConnection = new SqlConnection(PublicVariables.connectionServer);
                string addingNewBookQuery = "insert into books (title,NumberOfPages,Author,Category,PublishingYear,TotalBooks) values (@title,@numberOfPages,@author,@category,CONVERT(DATE, @date),@quantity) ";
                SqlCommand addingNewBookCommand = new SqlCommand(addingNewBookQuery, addingNewBookConnection);
                addingNewBookCommand.Parameters.AddWithValue("@title", title);
                addingNewBookCommand.Parameters.AddWithValue("@numberOfPages", numberOfPages);
                addingNewBookCommand.Parameters.AddWithValue("@author", author);
                addingNewBookCommand.Parameters.AddWithValue("@category", category);
                addingNewBookCommand.Parameters.AddWithValue("@date", date);
                addingNewBookCommand.Parameters.AddWithValue("@quantity", quantity);
                addingNewBookConnection.Open();
                addingNewBookCommand.ExecuteNonQuery();
                addingNewBookConnection.Close();
                return true;
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 80 : " + x.ToString());
                return false;
            }
        }
        public static void DeletingBook(int bookId)
        {
            try
            {
                string deletingBookQuery = "";
                deletingBookQuery = "Delete FROM[Library_System].[dbo].[Books] where ID = @id";
                SqlConnection deletingBookConnection = new SqlConnection(PublicVariables.connectionServer);
                SqlCommand deletingBookCommand = new SqlCommand(deletingBookQuery, deletingBookConnection);
                deletingBookCommand.Parameters.AddWithValue("@id", bookId);
                deletingBookConnection.Open();
                deletingBookCommand.ExecuteNonQuery();
                deletingBookConnection.Close();
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error 84 : " + x.ToString());
            }
        }
        //Books History Methods
        public static DataTable SearchBooksHistory(string search)
        {
            try
            {
                SqlConnection searchBooksHistoryConnection = new SqlConnection(PublicVariables.connectionServer);
                string searchBooksHistoryquery = "";
                if (string.IsNullOrEmpty(search))
                {
                    searchBooksHistoryquery = "Select * from TakenBooks";
                }
                else
                {
                    searchBooksHistoryquery = "Select * from TakenBooks where [TakenByEmail] like @search or [TakenByUniversityNumber] like @search or [TakenBookID] like @search or [TakenBookTitle] like @search or [TakenBookAuthor] like @search or [TakenBookCategory] like @search or [TakenBookID] like @search ";
                }
                SqlCommand searchBooksHistoryCommand = new SqlCommand(searchBooksHistoryquery, searchBooksHistoryConnection);
                searchBooksHistoryCommand.Parameters.AddWithValue("@search", "%" + search + "%");
                SqlDataAdapter searchBooksHistoryDataAdapter = new SqlDataAdapter(searchBooksHistoryCommand);
                DataTable searchBooksHistoryDatatable = new DataTable();
                searchBooksHistoryDataAdapter.Fill(searchBooksHistoryDatatable);
                return searchBooksHistoryDatatable;
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error 60 : " + x.ToString());
                DataTable searchBooksHistoryDatatable = new DataTable();
                return searchBooksHistoryDatatable;
            }
        }
        //Timeperiod Methods
        public static DataTable ShowingTimePeriod(string search)
        {
            try
            {
                SqlConnection searchBooksHistoryConnection = new SqlConnection(PublicVariables.connectionServer);
                string searchBooksHistoryquery = "";
                if (string.IsNullOrEmpty(search))
                {
                    searchBooksHistoryquery = "Select [ID],[TakenBookID] ,[TakenBookTitle], [TakenTime] , [TakenDays],[RemainingLimit],[Limit],[TakenByID],[TakenByEmail] ,[TakenByUniversityNumber] from TakenBooks where IsReturned = 0  AND Rank = 3";
                }
                else
                {
                    searchBooksHistoryquery = "Select [ID],[TakenBookID] ,[TakenBookTitle], [TakenTime] , [TakenDays],[RemainingLimit],[Limit],[TakenByID],[TakenByEmail] ,[TakenByUniversityNumber] from TakenBooks where IsReturned = 0 And  Rank = 3 AND [TakenByEmail] like @search or [TakenByUniversityNumber] like @search or [TakenBookID] like @search or [TakenBookTitle] like @search or [TakenBookAuthor] like @search or [TakenBookCategory] like @search or [TakenBookID] like @search ";
                }
                SqlCommand searchBooksHistoryCommand = new SqlCommand(searchBooksHistoryquery, searchBooksHistoryConnection);
                searchBooksHistoryCommand.Parameters.AddWithValue("@search", "%" + search + "%");
                SqlDataAdapter searchBooksHistoryDataAdapter = new SqlDataAdapter(searchBooksHistoryCommand);
                DataTable searchBooksHistoryDatatable = new DataTable();
                searchBooksHistoryDataAdapter.Fill(searchBooksHistoryDatatable);
                return searchBooksHistoryDatatable;
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error 83 : " + x.ToString());
                DataTable searchBooksHistoryDatatable = new DataTable();
                return searchBooksHistoryDatatable;
            }
        }
        public static void AddingTime(int time, int id)
        {
            try
            {
                string addingtimeQuery = "";
                addingtimeQuery = "update TakenBooks set Limit = Limit + @Limit where ID = @ID";
                SqlConnection addingtimeConnection = new SqlConnection(PublicVariables.connectionServer);
                SqlCommand addingtimeCommand = new SqlCommand(addingtimeQuery, addingtimeConnection);
                addingtimeCommand.Parameters.AddWithValue("@ID", id);
                addingtimeCommand.Parameters.AddWithValue("@Limit", time);
                addingtimeConnection.Open();
                addingtimeCommand.ExecuteNonQuery();
                addingtimeConnection.Close();
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error 90 : " + x.ToString());
            }
        }
    }
}
