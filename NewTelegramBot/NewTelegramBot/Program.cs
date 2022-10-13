using static NewTelegramBot.SQLConnectInfo;
using static NewTelegramBot.KeyboardMarkupList;
using static NewTelegramBot.botClass;


using System;
using System.Collections;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;


namespace NewTelegramBot
{
    class Program
    {
        private static DataTable TableUser;
        private static DataTable TableRequest;

        private static ArrayList Counter = new ArrayList();
        private static string datesS;
        private static string Prichina;

        public static Int64 chatId;

        static int Wir = 0;

        static void Main(string[] args)
        {
            Thread MainThreed = new Thread(StartingBotThreed);
            MainThreed.Start();

            //SecondThread.asd();
        }

        static void StartingBotThreed()
        {
            try
            {
                Console.WriteLine("Запуcк бота...");
                getTgUser();

                var cts = new CancellationTokenSource();
                var cancellationToken = cts.Token;
                var receiverOptions = new ReceiverOptions
                {
                    AllowedUpdates = { }, // receive all update types
                };
                bot.StartReceiving(
                    HandleUpdateAsync,
                    HandleErrorAsync,
                    receiverOptions,
                    cancellationToken
                );
                Console.WriteLine("Запущен бот. \n" + "Имя бота: " + bot.GetMeAsync().Result.FirstName);
                Console.ReadLine();
            }
            catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }           
            Console.ReadLine();            
        }
       
        static ITelegramBotClient bot = new TelegramBotClient(Token);     
        
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            //Выравнивания клавишь всех меню
            Client.ResizeKeyboard = true;
            DispOff.ResizeKeyboard = true;
            DispOn.ResizeKeyboard = true;
            DispBypass.ResizeKeyboard = true;
            registration.ResizeKeyboard = true;
            Waiting.ResizeKeyboard = true;
            PhaseBoarde.ResizeKeyboard = true;
            Request.ResizeKeyboard = true;
            Report.ResizeKeyboard = true;
            Admins.ResizeKeyboard = true;


            //Обработка ответов на инлайн кнопку
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                try
                {
                    CallbackQuery CallBackMessage = update.CallbackQuery;

                    TableUser = getTgUser();
                    Users tsUser = new Users();
                    chatId = CallBackMessage.Message.Chat.Id;
                    try
                    {
                        for (int i = 0; i < TableUser.Rows.Count; i++)
                        {
                            if (TableUser.Rows[i][1].ToString() == CallBackMessage.Message.Chat.Id.ToString())
                            {
                                tsUser = new Users { idRowTable = Convert.ToInt32(TableUser.Rows[i][0]), id = Convert.ToInt64(TableUser.Rows[i][1]), Name = TableUser.Rows[i][2].ToString(), Role = Convert.ToInt32(TableUser.Rows[i][3]), Number = TableUser.Rows[i][4].ToString() };
                            }
                        }
                    }
                    catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }
                    PhaseBoarde = Phase(tsUser);

                    string Switcher = "";
                    foreach (string i in Counter)
                    {
                        if (CallBackMessage.Data.Contains(i) && CallBackMessage.Data.Contains("A"))
                        {
                            Switcher = "A";
                        }
                        if (CallBackMessage.Data.Contains(i) && CallBackMessage.Data.Contains("B"))
                        {
                            Switcher = "B";
                        }
                        if (CallBackMessage.Data.Contains(i) && CallBackMessage.Data.Contains("C"))
                        {
                            Switcher = "C";
                        }
                        if (CallBackMessage.Data.Contains(i) && CallBackMessage.Data.Contains("D"))
                        {
                            Switcher = "D";
                        }
                        if (CallBackMessage.Data.Contains(i) && CallBackMessage.Data.Contains("F"))
                        {
                            Switcher = "F";
                        }
                    }
                    if (Switcher == "A")
                    {
                        int o = Convert.ToInt32(CallBackMessage.Data.Replace(Switcher, ""));
                        await botClient.SendTextMessageAsync(
                            CallBackMessage.Message.Chat.Id,
                            $"Заявка номер: {o} принята на выполнение.",
                            replyMarkup: PhaseBoarde
                        );
                        UpdateRequest(Convert.ToInt32(o), tsUser.idRowTable, 2);
                        long f = Mes(o);
                        await botClient.SendTextMessageAsync(
                            f,
                            "Ваша заявка выполняется."
                        );
                    }
                    if (Switcher == "B")
                    {
                        int o = Convert.ToInt32(CallBackMessage.Data.Replace(Switcher, ""));
                        await botClient.SendTextMessageAsync(
                            CallBackMessage.Message.Chat.Id,
                            $"Заявка номер:{o} завершена.",
                            replyMarkup: PhaseBoarde
                        );
                        UpdateRequest(Convert.ToInt32(o), tsUser.idRowTable, 3);
                        long f = Mes(o);
                        await botClient.SendTextMessageAsync(
                            f,
                            "Ваша заявка выполнена."
                        );
                    }
                    if (Switcher == "C")
                    {
                        int o = Convert.ToInt32(CallBackMessage.Data.Replace(Switcher, ""));
                        Wir = o;
                        await botClient.SendTextMessageAsync(
                            CallBackMessage.Message.Chat.Id,
                            $"Введите текс заметки:" + Wir+ " And "+ o,
                            replyMarkup: new ForceReplyMarkup { Selective = true }
                        );                       
                    }
                    if (Switcher == "D")
                    {
                        int o = Convert.ToInt32(CallBackMessage.Data.Replace(Switcher, ""));
                        await botClient.SendTextMessageAsync(
                            CallBackMessage.Message.Chat.Id,
                            $"Заявка номер:{o} подтверждена.",
                            replyMarkup: PhaseBoarde
                        );
                        UpdateRequest(Convert.ToInt32(o), tsUser.idRowTable, 1);
                        long f = Mes(o);
                        await botClient.SendTextMessageAsync(
                            f,
                            "Ваша заявка подтверждена."

                        );
                        for (int i = 0; i < TableUser.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(TableUser.Rows[i][3]) == 2)
                            {
                                await botClient.SendTextMessageAsync(Convert.ToInt64(TableUser.Rows[i][1]), "Добавлена новая заявка!");
                            }
                        }
                    }
                    if (Switcher == "F")
                    {
                        int o = Convert.ToInt32(CallBackMessage.Data.Replace(Switcher, ""));
                        await botClient.SendTextMessageAsync(
                            CallBackMessage.Message.Chat.Id,
                            $"Заявка номер:{o} отменена.",
                            replyMarkup: PhaseBoarde
                        );
                        UpdateRequest(Convert.ToInt32(o), tsUser.idRowTable, 5);
                        long f = Mes(o); 
                        await botClient.SendTextMessageAsync(
                            f,
                            "Ваша заявка отменена."
                        );
                    }
                }
                catch (Exception ex) { Console.WriteLine("ERROR Ошибка обработки inline button: " + ex.Message); }
            }

            //Обработка сообщений
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                try
                {
                    var message = update.Message;

                    TableUser = getTgUser();
                    Users tgUser = new Users();
                    chatId = message.Chat.Id;
                    try
                    {
                        for (int i = 0; i < TableUser.Rows.Count; i++)
                        {
                            if (TableUser.Rows[i][1].ToString() == message.Chat.Id.ToString())
                            {
                                tgUser = new Users { idRowTable = Convert.ToInt32(TableUser.Rows[i][0]), id = Convert.ToInt64(TableUser.Rows[i][1]), Name = TableUser.Rows[i][2].ToString(), Role = Convert.ToInt32(TableUser.Rows[i][3]), Number = TableUser.Rows[i][4].ToString() };
                            }
                        }
                    }
                    catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе с БД: " + ex.Message); }

                    PhaseBoarde = Phase(tgUser);
                    PhaseBoarde.ResizeKeyboard = true;

                    if (message.Text.ToLower() == "/start")
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, $"Добро пожаловать. Для регистрации нажмите кнопку 'Регистрация'", replyMarkup: registration);
                        return;
                    }
                    if (message.Text == "Регистрация" && tgUser.id != message.Chat.Id)
                    {
                        var result = botClient.SendTextMessageAsync(message.Chat.Id, "Введите ваши имя и фамилию", replyMarkup: new ForceReplyMarkup { Selective = true });
                    }
                    if (message.Text == "Регистрация" && tgUser.id == message.Chat.Id)
                    {
                        var result = botClient.SendTextMessageAsync(message.Chat.Id, "Вы уже зарегистрированы", replyMarkup: PhaseBoarde);
                    }
                    if (message.ReplyToMessage != null && message.ReplyToMessage.Text.Contains("Введите ваши имя и фамилию"))
                    {
                        WriteUserDB(message.Chat.Id, message.Text);
                        await botClient.SendTextMessageAsync(message.Chat.Id, $"Вы зарегестрированы.\nВаше имя: {message.Text}. \nОжидайте подтверждения администратора.", replyMarkup: Waiting);
                        await botClient.SendTextMessageAsync(784637231, $"Добавлен новый пользователь.");
                    }
                    if (message.Text == "Подтверждение" && tgUser.Role != 1)
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Вы перешли в главное меню.", replyMarkup: PhaseBoarde);
                    }
                    if (message.Text == "Подтверждение" && tgUser.Role == 1)
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Вы перешли в главное меню.\n" +
                            "Для вас доступна команда '/help'", replyMarkup: PhaseBoarde);
                    }

                    if (message.Text == "Заступить на смену" && tgUser.Role == 2 && DutyShift.Dispather == "" && DutyShift.idUser == 0)
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, $"{tgUser.Name} Заступил на смену.", replyMarkup: DispOn);
                        addChangeOfDuty($"{tgUser.Name} Заступил на смену.", tgUser.idRowTable);
                        addEvent(tgUser.idRowTable, message.Text);
                        DutyShift.Dispather = tgUser.Name;
                        DutyShift.idUser = tgUser.idRowTable;
                        DutyShift.idUserKey = tgUser.id;
                        DutyShift.numberPhone = tgUser.Number;
                        DutyShift.Start = DateTime.Now;
                    }
                    else if (message.Text == "Заступить на смену" && tgUser.Role == 2 && DutyShift.Dispather != "" && DutyShift.idUser != 0)
                    {
                        addEvent(tgUser.idRowTable, message.Text);
                        await botClient.SendTextMessageAsync(message.Chat.Id, $"Дежурный уже на смене. \nUsername: {DutyShift.Dispather}", replyMarkup: PhaseBoarde);
                        await botClient.SendTextMessageAsync(DutyShift.idUserKey, $"Закройте смену.");

                    }
                    if (message.Text == "Сдать смену" && DutyShift.idUser == tgUser.idRowTable)
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, $"{tgUser.Name} Сдал смену.", replyMarkup: DispOff);
                        addEvent(tgUser.idRowTable, message.Text);
                        addChangeOfDuty($"{tgUser.Name} Сдал смену.", tgUser.idRowTable);

                        DutyShift.Finish = DateTime.Now;
                        DutyShift.lustDispather = DutyShift.Dispather;
                        //DutyShiftReport(DutyShift.Start, DutyShift.Finish);
                        DutyShift.Dispather = "";
                        DutyShift.idUser = 0;
                        DutyShift.numberPhone = "";
                        
                    }
                    else if (message.Text == "Сдать смену" && DutyShift.idUser != tgUser.idRowTable)
                    {
                        addEvent(tgUser.idRowTable, message.Text);
                        await botClient.SendTextMessageAsync(message.Chat.Id, $"Вы не заступали на смену.", replyMarkup: PhaseBoarde);
                    }
                    if (message.Text == "Обход" && tgUser.Role == 2)
                    {
                        addEvent(tgUser.idRowTable, message.Text);
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Вы вошли в меню 'Обход'", replyMarkup: DispBypass);
                    }    
                    if (message.Text == "Завершить обход" && tgUser.Role == 2)
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Новая запись добавлена!", replyMarkup: PhaseBoarde);
                        addEvent(tgUser.idRowTable, message.Text);
                    }
                    if (message.Text == "Заметки" && DutyShift.idUser == tgUser.idRowTable)
                    {
                        addEvent(tgUser.idRowTable, message.Text);
                        var result = botClient.SendTextMessageAsync(message.Chat.Id, "Введите текст заметки", replyMarkup: new ForceReplyMarkup { Selective = true });
                    }
                    if (message.Text == "Заметки" && DutyShift.idUser != tgUser.idRowTable)
                    {
                        addEvent(tgUser.idRowTable, message.Text);
                        var result = botClient.SendTextMessageAsync(message.Chat.Id, "Вы не заступали на смену.", replyMarkup: PhaseBoarde);
                    }
                    if (message.ReplyToMessage != null && message.ReplyToMessage.Text.Contains("Введите текст заметки"))
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Новая заметка добавлена!", replyMarkup: PhaseBoarde);
                        addNote((int)tgUser.idRowTable, message.Text);
                    }

                    if (message.Text == "Заявка")
                    {
                        if (tgUser.Role == 1 || tgUser.Role == 3)
                        {
                            var result = botClient.SendTextMessageAsync(message.Chat.Id, "Введите текст заявки", replyMarkup: new ForceReplyMarkup { Selective = true });
                            addEvent(tgUser.idRowTable, message.Text);
                        }
                    }
                    if (message.ReplyToMessage != null && message.ReplyToMessage.Text.Contains("Введите текст заявки"))
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Ваша заявка принята!", replyMarkup: PhaseBoarde);
                        addRequest((int)tgUser.idRowTable, message.Text);
                        for (int i = 0; i < TableUser.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(TableUser.Rows[i][3]) == 4)
                            {
                                await botClient.SendTextMessageAsync(Convert.ToInt64(TableUser.Rows[i][1]), "Добавлена новая заявка!");
                            }
                        }
                    }

                    if (message.Text == "Активные заявки")
                    {
                        if (tgUser.Role == 1 || tgUser.Role == 3)
                        {
                            addEvent(tgUser.idRowTable, message.Text);
                            try
                            {
                                Counter.Clear();

                                TableRequest = getRequestInTheClientActive(tgUser.idRowTable);
                                if (TableRequest.Rows.Count != 0)
                                {
                                    for (int i = 0; i < TableRequest.Rows.Count; i++)
                                    {
                                        await botClient.SendTextMessageAsync(
                                            message.Chat.Id,
                                            text: $"Заявка №{TableRequest.Rows[i][0]} \nКлиент: {TableRequest.Rows[i][1]} \nВремя: {TableRequest.Rows[i][2]} \nЗапрос: {TableRequest.Rows[i][3]} \nПримечание:{TableRequest.Rows[i][5]} \nСтатус:{TableRequest.Rows[i][4]}",
                                            replyMarkup: PhaseBoarde
                                            );
                                    }
                                }
                                else
                                {
                                    await botClient.SendTextMessageAsync(
                                            message.Chat.Id,
                                            text: $"У вас нет активных заявок.",
                                            replyMarkup: PhaseBoarde
                                            );
                                }
                                TableRequest.Clear();
                            }
                            catch (Exception ex) { Console.WriteLine("Ошибка" + ex.Message); }
                        }
                    }
                    if (message.Text == "История заявок")
                    {
                        addEvent(tgUser.idRowTable, message.Text);
                        /*
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Ваша заявка принята!", replyMarkup: PhaseBoarde);
                        addRequest((int)tgUser.idRowTable, message.Text);

                        for (int i = 0; i < TableUser.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(TableUser.Rows[i][3]) != 1)
                            {
                                await botClient.SendTextMessageAsync(Convert.ToInt64(TableUser.Rows[i][1]), "Добавлена новая заявка!", replyMarkup: PhaseBoarde);
                            }
                        }
                        */
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Вкладка находится в разработке", replyMarkup: PhaseBoarde);
                    }
                    if (message.Text == "Заявки ожидающие одобрения" && tgUser.Role == 4)
                    {
                        addEvent(tgUser.idRowTable, message.Text);
                        try
                        {
                            Counter.Clear();

                            TableRequest = getRequestNew();
                            if (TableRequest.Rows.Count != 0)
                            {
                                for (int i = 0; i < TableRequest.Rows.Count; i++)
                                {
                                    Counter.Add(TableRequest.Rows[i][0].ToString());
                                    InlineKeyboardMarkup inlineKeyboard = (new[]
                                    {
                                    new []
                                    {
                                        InlineKeyboardButton.WithCallbackData(text: $"Подтвердить", callbackData: $"{TableRequest.Rows[i][0]}D"),
                                        InlineKeyboardButton.WithCallbackData(text: $"Отмена", callbackData: $"{TableRequest.Rows[i][0]}F"),
                                    },
                                    });
                                    await botClient.SendTextMessageAsync(
                                        message.Chat.Id,
                                        text: $"Заявка №{TableRequest.Rows[i][0]} \nКлиент: {TableRequest.Rows[i][1]} \nВремя: {TableRequest.Rows[i][2]} \nЗапрос: {TableRequest.Rows[i][3]} \nПримечание: {TableRequest.Rows[i][5]}",
                                        replyMarkup: inlineKeyboard);
                                }
                            }
                            else
                            {
                                await botClient.SendTextMessageAsync(
                                        message.Chat.Id,
                                        text: $"Сейчас нет активных заявок.",
                                        replyMarkup: PhaseBoarde
                                        );
                            }
                            TableRequest.Clear();
                        }
                        catch (Exception ex) { Console.WriteLine("Ошибка" + ex.Message); }
                    }
                    if (message.Text == "Заявки" && tgUser.Role == 2)
                    {
                        addEvent(tgUser.idRowTable, message.Text);
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Вы вошли в меню 'Заявки'.", replyMarkup: Request);
                    }
                    if (message.Text == "Подтвержденные" && tgUser.Role == 2)
                    {
                        addEvent(tgUser.idRowTable, message.Text);
                        try
                        {
                            Counter.Clear();

                            TableRequest = getRequestNew();
                            if (TableRequest.Rows.Count != 0)
                            {
                                for (int i = 0; i < TableRequest.Rows.Count; i++)
                                {
                                    Counter.Add(TableRequest.Rows[i][0].ToString());
                                    InlineKeyboardMarkup inlineKeyboard = (new[]
                                    {
                                    new []
                                    {
                                        InlineKeyboardButton.WithCallbackData(text: $"Выполнить", callbackData: $"{TableRequest.Rows[i][0]}A"),
                                        InlineKeyboardButton.WithCallbackData(text: $"Заметка", callbackData: $"{TableRequest.Rows[i][0]}C"),
                                    },
                                    });
                                    await botClient.SendTextMessageAsync(
                                        message.Chat.Id,
                                        text: $"Заявка №{TableRequest.Rows[i][0]} \nКлиент: {TableRequest.Rows[i][1]} \nВремя: {TableRequest.Rows[i][2]} \nЗапрос: {TableRequest.Rows[i][3]} \nПримечание: {TableRequest.Rows[i][5]}",
                                        replyMarkup: inlineKeyboard);
                                }
                            }
                            else
                            {
                                await botClient.SendTextMessageAsync(
                                        message.Chat.Id,
                                        text: $"Сейчас нет активных заявок.",
                                        replyMarkup: PhaseBoarde
                                        );
                            }
                            TableRequest.Clear();
                        }
                        catch (Exception ex) { Console.WriteLine("Ошибка" + ex.Message); }
                    }
                    if (message.Text == "На выполнении" && tgUser.Role == 2)
                    {
                        addEvent(tgUser.idRowTable, message.Text);
                        try
                        {
                            Counter.Clear();

                            TableRequest = getRequestAVG();
                            if (TableRequest.Rows.Count != 0)
                            {
                                for (int i = 0; i < TableRequest.Rows.Count; i++)
                                {
                                    Counter.Add(TableRequest.Rows[i][0].ToString());
                                    InlineKeyboardMarkup inlineKeyboard = (new[]
                                    {
                                    new []
                                    {
                                        InlineKeyboardButton.WithCallbackData(text: $"Завершить", callbackData: $"{TableRequest.Rows[i][0]}B"),
                                        InlineKeyboardButton.WithCallbackData(text: $"Заметка", callbackData: $"{TableRequest.Rows[i][0]}C"),
                                    },
                                    });
                                    await botClient.SendTextMessageAsync(
                                        message.Chat.Id,
                                        text: $"Заявка №{TableRequest.Rows[i][0]} \nКлиент: {TableRequest.Rows[i][1]} \nВремя: {TableRequest.Rows[i][2]} \nЗапрос: {TableRequest.Rows[i][3]} \nПримечание: {TableRequest.Rows[i][5]}",
                                        replyMarkup: inlineKeyboard);

                                }
                            }
                            else
                            {
                                await botClient.SendTextMessageAsync(
                                        message.Chat.Id,
                                        text: $"Вы не выполняете никаких заявок.",
                                        replyMarkup: PhaseBoarde
                                        );
                            }
                        }
                        catch (Exception ex) { Console.WriteLine("Ошибка" + ex.Message); }
                    }

                    if (message.ReplyToMessage != null && message.ReplyToMessage.Text.Contains("Введите текс заметки:"))
                    {
                        UpdateRequestWorking(Convert.ToInt32(Wir), message.Text);
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Заметка добавлена!" + Wir);
                        Wir = 0;
                    }
                    if (message.Text == "Отчет за сутки")
                    {
                        if (tgUser.Role == 2 || tgUser.Role == 3)
                        {
                            try
                            {
                                var Excel = CreatorReports("day", "GETDATE()");

                                System.IO.File.WriteAllBytes($"Отчеты/ОтчетСутки{datesS}.xlsx", Excel);
                                SecondThread.CreateMailMessage($"Отчеты/ОтчетСутки{datesS}.xlsx");
                                await botClient.SendTextMessageAsync(message.Chat.Id, "Отчет о заявках за сутки создан.", replyMarkup: Report);
                            }
                            catch (Exception ex) { Console.WriteLine("Ошибка" + ex.Message); }
                        }
                    }
                    if (message.Text == "Отчет за месяц")
                    {
                        if (tgUser.Role == 2 || tgUser.Role == 3)
                        {
                            try
                            {
                                var Excel = CreatorReports("month", "GETDATE()");
                                System.IO.File.WriteAllBytes($"Отчеты/ОтчетМесяц{datesS}.xlsx", Excel);
                                SecondThread.CreateMailMessage($"Отчеты/ОтчетМесяц{datesS}.xlsx");
                                await botClient.SendTextMessageAsync(message.Chat.Id, "Отчет о заявках за месяц создан.", replyMarkup: Report);
                            }
                            catch (Exception ex) { Console.WriteLine("Ошибка" + ex.Message); }
                        }
                    }
                    if (message.Text == "Отчет за...")
                    {
                        if (tgUser.Role == 2 || tgUser.Role == 3)
                        {
                            await botClient.SendTextMessageAsync(message.Chat.Id, "Введите дату как в примере: 'гггг-мм-дд'", replyMarkup: new ForceReplyMarkup { Selective = true });
                        } 
                    }
                    if (message.ReplyToMessage != null && message.ReplyToMessage.Text.Contains("Введите дату как в примере: 'гггг-мм-дд'"))
                    {
                        try
                        {
                            var Excel = CreatorReports("day", $"'{message.Text}'");
                            System.IO.File.WriteAllBytes($"Отчеты/ОтчетЗа{message.Text}От{datesS}.xlsx", Excel);
                            SecondThread.CreateMailMessage($"Отчеты/ОтчетЗа{message.Text}От{datesS}.xlsx");
                            await botClient.SendTextMessageAsync(message.Chat.Id, $"Отчет за {message.Text} от {datesS} создан.", replyMarkup: Report);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Ошибка" + ex.Message);
                            await botClient.SendTextMessageAsync(message.Chat.Id, "Неправильно введена дата.", replyMarkup: Report);
                        }
                    }
                    if (message.Text == "Отчеты")
                    {
                        if (tgUser.Role == 2 || tgUser.Role == 3)
                        {
                            await botClient.SendTextMessageAsync(message.Chat.Id, "Вы перешли в меню отчеты.", replyMarkup: Report);
                        }
                    }

                    if (message.Text == "Назад" || message.Text == "Завершить обход")
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Вы перешли в главное меню", replyMarkup: PhaseBoarde);
                    }

                    if (message.Text == "Контактные данные")
                    {
                        if (DutyShift.Dispather != "")
                        {
                            await botClient.SendTextMessageAsync(message.Chat.Id, $"Дежурный: {DutyShift.Dispather}\nНомер дежурного: {DutyShift.numberPhone}", replyMarkup: PhaseBoarde);
                        }
                        else
                        {
                            await botClient.SendTextMessageAsync(message.Chat.Id, $"В данный момент дежурного нет на смене.", replyMarkup: PhaseBoarde);
                        }
                    }
                    if(message.Text == "/help")
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, $"Список всех команд" +
                            $"\nНазад - выход в главное меню," +
                            $"\nЗаявка - создать вашу заявку," +
                            $"\nАктивные заявки - ваши активные заявки," +
                            $"\nИстория заявок - ваши выполненные заявки," +
                            $"\nКонтактные данные - контактные данные диспетчеров.", replyMarkup: PhaseBoarde);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка" + ex.Message);
                }
                    //ДОБАВИТЬ НОМЕРА И ПОЧТУ В ЮЗЕРЫ, ТАК ЖЕ ДОДЕЛАТЬ КНОПКУ ОБРАТНАЯ СВЯЗЬ     
                }
        }
        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }

        //Изменение Меню в исходя из Роли пользователя
        public static ReplyKeyboardMarkup Phase(Users tgUser)
        {         
            ReplyKeyboardMarkup PhaseBoarde = null;
            try
            {

                if (tgUser.Role == 2)
                {
                    ReplyKeyboardMarkup markup;
                    markup = DutyShift.Dispather == tgUser.Name ? DispOn : DispOff;
                    PhaseBoarde = markup;
                }
                else if (tgUser.Role == 3)
                {
                    PhaseBoarde = Admins;
                }
                else if (tgUser.Role == 1)
                {
                    PhaseBoarde = Client;
                }
                else if (tgUser.Role == 4)
                {
                    PhaseBoarde = Admins;
                }
                else
                {
                    PhaseBoarde = Waiting;
                }
            }
            catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе метода PHASEBOARD: " + ex.Message); }

            return PhaseBoarde;
        }

        public static long Mes(int id)
        {
           
            long chatID = 0;
            try
            {
                
                for (int i = 0; i < TableRequest.Rows.Count; i++)
                {
                    if (id == (int)TableRequest.Rows[i][0])
                    {
                        for (int j = 0; j < TableUser.Rows.Count; j++)
                        {
                            if (TableRequest.Rows[i][1].ToString() == TableUser.Rows[j][2].ToString())
                            {
                                chatID = Convert.ToInt64(TableUser.Rows[j][1]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine("ERROR Ошибка при работе метода MES: " + ex.Message); }
            return chatID;
        }

        static byte[] CreatorReports(string g, string t)
        {
            DataTable Request = ForReportRequest(g,t);
            DataTable Event = ForReportRound(g,t);
            DataTable Note = ForReportNote(g,t);
            var reportExcel = ExcelCreator.CompletedRequestCient(Request, Event, Note);
            DateTime dates = DateTime.Now;
            datesS = dates.ToShortDateString();
            datesS = datesS.Replace(".", "-");

            return reportExcel;
        }

        public static void DutyShiftReport(DateTime Start, DateTime Finish)
        {
            DataTable Request = ForReportRequestInDuty(DutyShift.Start.ToString(), DutyShift.Finish.ToString());
            DataTable Event = ForReportRoundInDuty(DutyShift.Start.ToString(), DutyShift.Finish.ToString());
            DataTable Note = ForReportNoteInDuty(DutyShift.Start.ToString(), DutyShift.Finish.ToString());
            var reportExcel = ExcelCreator.CompletedRequestCientNew(Request, Event, Note);

            DateTime dates = DateTime.Now;
            datesS = dates.ToShortDateString();
            datesS = datesS.Replace(".", "-");
                     
            System.IO.File.WriteAllBytes($"Отчеты/Смена-{DutyShift.Dispather.Replace(" ","")}-Время{datesS}.xlsx", reportExcel);
            SecondThread.CreateMailMessage($"Отчеты/Смена-{DutyShift.Dispather.Replace(" ", "")}-Время{datesS}.xlsx");           
        }
    }
}
