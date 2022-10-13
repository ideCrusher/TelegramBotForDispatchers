using System;
using System.Data;
using System.Data.SqlClient;

namespace NewTelegramBot
{
    class SQLConnectInfo
    {
        //Строка подключение к БД

        //Сервер
        //public static string connectString { get; } = @"Data Source = DESKTOP-ME4DC3N\SQLEXPRESS; Initial Catalog = TelegramBotInterfaceForDispatcher; Integrated Security = True";

        //Ноут
        public static string connectString { get; } = @"Data Source = DESKTOP-DHI66CB\SQLEXPRESS; Initial Catalog = TelegramBotInterfaceForDispatcher; Integrated Security = True";

        //Получение списка пользователей
        public static DataTable getTgUser()
        {
            DataTable result = null;
            try
            {
                string sConnect = SQLConnectInfo.connectString;
                string sqlQuery = @"SELECT  [id_User],[User_Chat_id],[UserName],[UserRole],[UserNumberPhone] FROM [dbo].[Users]";
                SqlConnection con = new SqlConnection(sConnect);
                SqlCommand comm = new SqlCommand(sqlQuery, con);
                try
                {
                    Console.WriteLine("Получаем данные...");
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(comm);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    result = ds.Tables[0];

                    con.Close();
                }
                catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }
            }
            catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }
            Console.WriteLine("Данные пользователей получены.");
            return result;
        }
        public static DataTable getRequestNewForAdmin()
        {
            DataTable result = null;

            try
            {
                string sConnect = SQLConnectInfo.connectString;
                string sqlQuery = @"SELECT id_Request, UserName , date_Request, text_Request, name_Status, text_Working FROM Request_Client inner join Users on Request_Client.id_User = Users.id_User inner join Request_Status on Request_Client.id_Status = Request_Status.id_Status WHERE Request_Client.id_Status = 4";
                SqlConnection con = new SqlConnection(sConnect);
                SqlCommand comm = new SqlCommand(sqlQuery, con);
                try
                {
                    Console.WriteLine("Получаем данные о заявках...");
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(comm);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    result = ds.Tables[0];

                    con.Close();
                }
                catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }
            }
            catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }
            Console.WriteLine("Данные о заявках получены получены.");

            return result;
        }
        //Получение новых завок
        public static DataTable getRequestNew()
        {
            DataTable result = null;

            try
            {
                string sConnect = SQLConnectInfo.connectString;
                string sqlQuery = @"SELECT id_Request, UserName , date_Request, text_Request, name_Status, text_Working FROM Request_Client inner join Users on Request_Client.id_User = Users.id_User inner join Request_Status on Request_Client.id_Status = Request_Status.id_Status WHERE Request_Client.id_Status = 4";
                SqlConnection con = new SqlConnection(sConnect);
                SqlCommand comm = new SqlCommand(sqlQuery, con);
                try
                {
                    Console.WriteLine("Получаем данные о заявках...");
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(comm);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    result = ds.Tables[0];

                    con.Close();
                }
                catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }
            }
            catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }
            Console.WriteLine("Данные о заявках получены получены.");

            return result;
        }

        //Получение заявок на выполнении
        public static DataTable getRequestAVG()
        {
            DataTable result = null;

            try
            {
                string sConnect = SQLConnectInfo.connectString;
                string sqlQuery = @"SELECT id_Request, UserName , date_Request, text_Request, name_Status, text_Working FROM Request_Client inner join Users on Request_Client.id_User = Users.id_User inner join Request_Status on Request_Client.id_Status = Request_Status.id_Status WHERE Request_Client.id_Status = 2";
                SqlConnection con = new SqlConnection(sConnect);
                SqlCommand comm = new SqlCommand(sqlQuery, con);
                try
                {
                    Console.WriteLine("Получаем данные о заявках...");
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(comm);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    result = ds.Tables[0];

                    con.Close();
                }
                catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }
            }
            catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }
            Console.WriteLine("Данные о заявках получены получены.");

            return result;
        }

        //Получение заявок пользователя из истории активых заявок
        public static DataTable getRequestInTheClientStory(int id_User)
        {
            DataTable result = null;

            try
            {
                string sConnect = SQLConnectInfo.connectString;
                string sqlQuery = $@"SELECT id_Request, UserName , date_Request, text_Request, name_Status, text_Working FROM Request_Client inner join Users on Request_Client.id_User = Users.id_User inner join Request_Status on Request_Client.id_Status = Request_Status.id_Status WHERE Request_Client.id_User = {id_User}";
                SqlConnection con = new SqlConnection(sConnect);
                SqlCommand comm = new SqlCommand(sqlQuery, con);
                try
                {
                    Console.WriteLine("Получаем данные о заявках...");
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(comm);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    result = ds.Tables[0];

                    con.Close();
                }
                catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }
            }
            catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }
            Console.WriteLine("Данные о заявках получены получены.");

            return result;
        }
        public static DataTable getRequestInTheClientActive(int id_User)
        {
            DataTable result = null;

            try
            {
                string sConnect = SQLConnectInfo.connectString;
                string sqlQuery = $@"SELECT id_Request, UserName , date_Request, text_Request, name_Status, text_Working FROM Request_Client inner join Users on Request_Client.id_User = Users.id_User inner join Request_Status on Request_Client.id_Status = Request_Status.id_Status WHERE Request_Client.id_User = {id_User} and Request_Client.id_Status IN(1,2)";
                SqlConnection con = new SqlConnection(sConnect);
                SqlCommand comm = new SqlCommand(sqlQuery, con);
                try
                {
                    Console.WriteLine("Получаем данные о заявках...");
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(comm);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    result = ds.Tables[0];

                    con.Close();
                }
                catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }
            }
            catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }
            Console.WriteLine("Данные о заявках получены получены.");

            return result;
        }

        //Регистрации пользователя
        public static void WriteUserDB(long Chat_id_User, string UserName)
        {
            try
            {
                string sConnect = SQLConnectInfo.connectString;
                string sqlQuery = $@"INSERT INTO [dbo].[Users]([User_Chat_id],[UserName])VALUES('{Chat_id_User}','{UserName}')";

                SqlConnection con = new SqlConnection(sConnect);
                SqlCommand comm = new SqlCommand(sqlQuery, con);
                try
                {
                    con.Open();
                    comm.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }
            }
            catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }
            Console.WriteLine($"Добавлен пользователь:{UserName}");
        }
        
        //Добавление записи о смене дежурства
        public static void addChangeOfDuty(string TextSmena, int Chat_id_User)
        {
            try
            {
                string sConnect = SQLConnectInfo.connectString;
                string sqlQuery = $@"INSERT INTO [dbo].[ChangeOfDuty]([Actions],[id_User],[Data_Time])VALUES('{TextSmena}','{Chat_id_User}','{DateTime.Now}')";

                SqlConnection con = new SqlConnection(sConnect);
                SqlCommand comm = new SqlCommand(sqlQuery, con);
                try
                {
                    con.Open();
                    comm.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }
            }
            catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }
            Console.WriteLine("Добавлена запись в таблицу 'Смена дежурств'.");
        }
        
        //Добавление заметки
        public static void addNote(int id_User, string TextZametki)
        {
            try
            {
                string sConnect = SQLConnectInfo.connectString;
                string sqlQuery = $@"INSERT INTO [dbo].[Note]([id_User],[Date_Time],[Text_Note])VALUES('{id_User}','{DateTime.Now}','{TextZametki}')";

                SqlConnection con = new SqlConnection(sConnect);
                SqlCommand comm = new SqlCommand(sqlQuery, con);
                try
                {
                    con.Open();
                    comm.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }
            }
            catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }
            Console.WriteLine("Заметка добавлена");
        }
        
        //Добавление собатия
        public static void addEvent(int id_User, string message)
        {
            try
            {
                string sConnect = SQLConnectInfo.connectString;
                string sqlQuery = $@"INSERT INTO [dbo].[AllEvent]([id_User],[Data_Event],[Text_Event])VALUES('{id_User}','{DateTime.Now}','{message}')";

                SqlConnection con = new SqlConnection(sConnect);
                SqlCommand comm = new SqlCommand(sqlQuery, con);
                try
                {
                    con.Open();
                    comm.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }
            }
            catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }
            Console.WriteLine("Событие добавлено");
        }
        
        //Создание завки
        public static void addRequest(int id_User, string textMessage)
        {
            try
            {
                string sConnect = SQLConnectInfo.connectString;
                string sqlQuery = $@"INSERT INTO [dbo].[Request_Client]([id_User],[date_Request],[text_Request],[id_Status])VALUES('{id_User}','{DateTime.Now}','{textMessage}','4')";

                SqlConnection con = new SqlConnection(sConnect);
                SqlCommand comm = new SqlCommand(sqlQuery, con);
                try
                {
                    con.Open();
                    comm.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }
            }
            catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }
            Console.WriteLine("Добавлена заявка!");
        }

        //Обновление статуса заявок
        public static void UpdateRequest(int id_Request,int id_User, int id_Status)
        {
            try
            {
                string sConnect = SQLConnectInfo.connectString;
                string sqlQuery = $@"update [dbo].[Request_Client] set [id_Status] = {id_Status}, id_executer = {id_User} where id_Request = {id_Request}";

                SqlConnection con = new SqlConnection(sConnect);
                SqlCommand comm = new SqlCommand(sqlQuery, con);
                try
                {
                    con.Open();
                    comm.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }
            }
            catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }
            Console.WriteLine("Изменения сохранены.");
        }

        public static void UpdateRequestWorking(int id_Request, string Text)
        {
            try
            {
                string sConnect = SQLConnectInfo.connectString;
                string sqlQuery = $@"update [dbo].[Request_Client] set text_Working = '{Text}' where id_Request = {id_Request}";

                SqlConnection con = new SqlConnection(sConnect);
                SqlCommand comm = new SqlCommand(sqlQuery, con);
                try
                {
                    con.Open();
                    comm.ExecuteNonQuery();
                    con.Close();
                    Console.WriteLine("Изменения сохранены.");
                }

                catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }
            }
            catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }
            
        }

        public static DataTable ForReportRequest(string g, string t)
        {
            DataTable result = null;

            try
            {
                string sConnect = SQLConnectInfo.connectString;
                string sqlQuery = $@"SELECT id_Request, Users.UserName , date_Request, text_Request, name_Status, text_Working, Us.UserName FROM Request_Client inner join Users on Request_Client.id_User = Users.id_User inner join Users as Us on Request_Client.id_executer = Us.id_User inner join Request_Status on Request_Client.id_Status = Request_Status.id_Status WHERE DATEDIFF({g}, date_Request, CAST({t} AS DATE)) = 0";
                SqlConnection con = new SqlConnection(sConnect);
                SqlCommand comm = new SqlCommand(sqlQuery, con);
                try
                {
                    Console.WriteLine("Получаем данные о заявках для отчета...");
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(comm);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    result = ds.Tables[0];

                    con.Close();

                    Console.WriteLine("Данные получены.");  
                }
                catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }
            }
            catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }

            return result;
        }

        public static DataTable ForReportRound(string g, string t)
        {
            DataTable result = null;

            try
            {
                string sConnect = SQLConnectInfo.connectString;
                string sqlQuery = $@"select UserName, Data_Event, Text_Event from AllEvent inner join Users on AllEvent.id_User = Users.id_User where Text_Event = 'Завершить обход' and DATEDIFF({g}, Data_Event, CAST({t} AS DATE)) = 0";
                SqlConnection con = new SqlConnection(sConnect);
                SqlCommand comm = new SqlCommand(sqlQuery, con);
                try
                {
                    Console.WriteLine("Получаем данные о заявках для отчета...");
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(comm);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    result = ds.Tables[0];

                    con.Close();

                    Console.WriteLine("Данные получены.");
                }
                catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }
            }
            catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }

            return result;
        }

        public static DataTable ForReportNote(string g, string t)
        {
            DataTable result = null;

            try
            {
                string sConnect = SQLConnectInfo.connectString;
                string sqlQuery = $@"select UserName, Date_Time, Text_Note from Note inner join Users on Note.id_User = Users.id_User where DATEDIFF({g}, Date_Time, CAST({t} AS DATE)) = 0";
                SqlConnection con = new SqlConnection(sConnect);
                SqlCommand comm = new SqlCommand(sqlQuery, con);
                try
                {
                    Console.WriteLine("Получаем данные о заявках для отчета...");
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(comm);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    result = ds.Tables[0];

                    con.Close();

                    Console.WriteLine("Данные получены.");
                }
                catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }
            }
            catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }

            return result;
        }



        public static DataTable ForReportRequestInDuty(string s, string f)
        {
            DataTable result = null;

            try
            {
                string sConnect = SQLConnectInfo.connectString;
                string sqlQuery = $@"SELECT id_Request, Users.UserName , date_Request, text_Request, name_Status, text_Working, Us.UserName FROM Request_Client inner join Users on Request_Client.id_User = Users.id_User inner join Users as Us on Request_Client.id_executer = Us.id_User inner join Request_Status on Request_Client.id_Status = Request_Status.id_Status WHERE date_Request >= cast('{s}' as datetimeoffset) and date_Request <= cast('{f}' as datetimeoffset)";
                SqlConnection con = new SqlConnection(sConnect);
                SqlCommand comm = new SqlCommand(sqlQuery, con);
                try
                {
                    Console.WriteLine("Получаем данные о заявках для отчета...");
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(comm);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    result = ds.Tables[0];

                    con.Close();

                    Console.WriteLine("Данные получены.");
                }
                catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }
            }
            catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }

            return result;
        }
        public static DataTable ForReportRoundInDuty(string s, string f)
        {
            DataTable result = null;

            try
            {
                string sConnect = SQLConnectInfo.connectString;
                string sqlQuery = $@"select UserName, Data_Event, Text_Event from AllEvent inner join Users on AllEvent.id_User = Users.id_User where Text_Event = 'Завершить обход' and Data_Event >= cast('{s}' as datetimeoffset) and Data_Event <= cast('{f}' as datetimeoffset)";
                SqlConnection con = new SqlConnection(sConnect);
                SqlCommand comm = new SqlCommand(sqlQuery, con);
                try
                {
                    Console.WriteLine("Получаем данные о заявках для отчета...");
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(comm);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    result = ds.Tables[0];

                    con.Close();

                    Console.WriteLine("Данные получены.");
                }
                catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }
            }
            catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }

            return result;
        }
        public static DataTable ForReportNoteInDuty(string s, string f)
        {
            DataTable result = null;

            try
            {
                string sConnect = SQLConnectInfo.connectString;
                string sqlQuery = $@"select UserName, Date_Time, Text_Note from Note inner join Users on Note.id_User = Users.id_User where Date_Time >= cast('{s}' as datetimeoffset) and Date_Time <= cast('{f}' as datetimeoffset)";
                SqlConnection con = new SqlConnection(sConnect);
                SqlCommand comm = new SqlCommand(sqlQuery, con);
                try
                {
                    Console.WriteLine("Получаем данные о заявках для отчета...");
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(comm);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    result = ds.Tables[0];

                    con.Close();

                    Console.WriteLine("Данные получены.");
                }
                catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }
            }
            catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }

            return result;
        }




    }
}
