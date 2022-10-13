using static NewTelegramBot.SQLConnectInfo;
using static NewTelegramBot.botClass;
using static NewTelegramBot.Program;
using static NewTelegramBot.KeyboardMarkupList;


using System;
using System.Net;
using System.IO;
using System.Threading;
using System.Net.Mail;
using System.Data;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace NewTelegramBot
{
    class SecondThread
    {

        private static int timesDay = 1, timesMonth = 1, DayTime = 1;
        private static string datesS;

        public static void asd()
        {
            Thread SecondThreed = new Thread(fda);         
            SecondThreed.Start();           
        }
        static void fda()
        {
            int i = 0;
            while (i < 1)
            {
                
                SearchingDayTime();
                SearchingMonth();
            }            
        }

        static byte[] CreatorReports(string g, string t)
        {
            DataTable Request = ForReportRequest(g, t);
            DataTable Event = ForReportRound(g, t);
            DataTable Note = ForReportNote(g, t);
            var reportExcel = ExcelCreator.CompletedRequestCient(Request, Event, Note);
            DateTime dates = DateTime.Now;
            datesS = dates.ToShortDateString();
            datesS = datesS.Replace(".", "-");

            return reportExcel;
        }
        public static void CreateMailMessage(string path)
        {
            // отправитель - устанавливаем адрес и отображаемое в письме имя           
            // кому отправляем            
            // создаем объект сообщения         
            // тема письма
            // текст письма
            // письмо представляет код html
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            // логин и пароль
            try
            {
                MailAddress from = new MailAddress("ideCrusher@yandex.ru", "CoreDataNet_Bot");
                //MailAddress to = new MailAddress("annaklwrus@yandex.ru");
                MailAddress to = new MailAddress("Karpov_vs@osnovaholding.ru");
                MailMessage m = new MailMessage(from, to);
                m.Subject = "Отчет";
                m.Body = "Отчет о работе диспетчерской службы.";
                m.Attachments.Add(new Attachment($"{path}"));
                SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 25);
                smtp.Credentials = new NetworkCredential("ideCrusher@yandex.ru", "libabzbnkudmgpip");
                smtp.EnableSsl = true;               
                smtp.Send(m);

                Console.WriteLine("Отчет отправлен.");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка" + ex.Message);
            }
        }

        public static void SearchingDayTime()
        {
            DateTime data = DateTime.Now;
            if (DutyShift.Dispather != "")
            {
                if (data.Hour == 8 && data.Minute == 59 && DayTime == 1)
                {
                    DayTime = 2;
                }
                if (data.Hour != 8 && data.Minute != 59 && DayTime == 3)
                {
                    DayTime = 1;
                }
                if (data.Hour == 8 && data.Minute == 59 && DayTime == 2)
                {
                    try
                    {
                        dsa();                                                   
                    }
                    catch (Exception ex) { Console.WriteLine("Ошибка" + ex.Message); }
                }
                if (data.Hour == 8 && data.Minute == 59 && DayTime == 2)
                {
                    DayTime = 3;
                }
            }
        }


        public static void SearchingMonth()
        {
            DateTime data = DateTime.Now;

            if (data.Day == 30 && data.Hour == 23 && data.Minute == 59 && timesMonth == 1)
            {
                timesMonth = 2;
            }
            if (data.Day == 30 && data.Hour != 23 && data.Minute != 59 && timesMonth == 3)
            {
                timesMonth = 1;
            }
            if (data.Day == 30 && data.Hour == 23 && data.Minute == 59 && timesMonth == 2)
            {
                try
                {
                    var Excel = CreatorReports("month", "GETDATE()");
                    string pathMonth = $"Отчеты/ОтчетМесяц{datesS}.xlsx";
                    System.IO.File.WriteAllBytes(pathMonth, Excel);
                    CreateMailMessage(pathMonth);

                }
                catch (Exception ex) { Console.WriteLine("Ошибка" + ex.Message); }
            }
            if (data.Day == 30 && data.Hour == 18 && data.Minute == 30 && timesMonth == 2)
            {
                timesMonth = 3;
            }
        }



        public static void dsa ()
        {
            ITelegramBotClient bot = new TelegramBotClient(Token);            

            bot.SendTextMessageAsync(DutyShift.idUserKey,"Ваша смена окончена.", replyMarkup: PhaseBoarde);
        }
    }
}
