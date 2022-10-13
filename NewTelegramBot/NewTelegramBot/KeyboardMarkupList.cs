using Telegram.Bot.Types.ReplyMarkups;

namespace NewTelegramBot
{
    public static class KeyboardMarkupList
    {
        //Меню диспетчера
        public static ReplyKeyboardMarkup DispOff = new ReplyKeyboardMarkup(new[]
        {
            new KeyboardButton("Заявки"),
            new KeyboardButton("Обход"),
            new KeyboardButton("Заступить на смену"),
        });
        public static ReplyKeyboardMarkup DispOn = new ReplyKeyboardMarkup(new[]
{
            new KeyboardButton("Заявки"),
            new KeyboardButton("Обход"),
            new KeyboardButton("Сдать смену"),
        });

        //Меню заявок для диспетчера
        public static ReplyKeyboardMarkup Request = new ReplyKeyboardMarkup(new[]
        {
            new[]
            {
            new KeyboardButton("Подтвержденные"),
            },
            new[]
            {
            new KeyboardButton("На выполнении"),
            },
            new[]
            {
            new KeyboardButton("Назад"),
            },
        });

        //Меню осмотра для диспетчера
        public static ReplyKeyboardMarkup DispBypass = new ReplyKeyboardMarkup(new KeyboardButton[][]
        {
            new[]
            {
            new KeyboardButton("Заметки"),
            new KeyboardButton("Завершить обход"),
            },
            new[]
            {
            new KeyboardButton("Назад"),
            },
        });
        
        //Меню клиента
        public static ReplyKeyboardMarkup Client = new ReplyKeyboardMarkup(new KeyboardButton[][]
        {
            new[]
            {
            new KeyboardButton("Заявка"),
            new KeyboardButton("Активные заявки"),
            new KeyboardButton("История заявок"),
            },
            new[]
            {
            new KeyboardButton("Контактные данные"),
            },
        });

        public static ReplyKeyboardMarkup ClientRequest = new ReplyKeyboardMarkup(new KeyboardButton[][]
        {
            new[]
            {
            new KeyboardButton("Заявка для диспетчера"),
            },
            new[]
            {
            new KeyboardButton("Заявка для Сис.Админа"),
            },
            new[]
            {
            new KeyboardButton("Назад"),
            },
        });

        //Меню Администрации
        public static ReplyKeyboardMarkup Admins = new ReplyKeyboardMarkup(new KeyboardButton[][]
        {
            new[]
            {
            new KeyboardButton("Заявки ожидающие одобрения"),
            },
            new[]
            {
            new KeyboardButton("Заявка"),
            new KeyboardButton("Активные заявки"),
            new KeyboardButton("История заявок"),            
            },
            
            new[]
            {
            new KeyboardButton("Отчеты"),
            },

        });

        //Меню регистрации
        public static ReplyKeyboardMarkup registration = new ReplyKeyboardMarkup(new[]
        {
            new KeyboardButton("Регистрация"),
        });

        //Меню ожидания подтверждения
        public static ReplyKeyboardMarkup Waiting = new ReplyKeyboardMarkup(new[]
        {
            new KeyboardButton("Подтверждение"),
        });

        //Меню отчетов
        public static ReplyKeyboardMarkup Report = new ReplyKeyboardMarkup(new[]
        {
            new[]
            {
            new KeyboardButton("Отчет за...")
            },

            new[]
            {
            new KeyboardButton("Отчет за месяц"),
            new KeyboardButton("Отчет за день"),
            },

            new[]
            {
            new KeyboardButton("Назад"),
            },
        });

        public static ReplyKeyboardMarkup Admin = new ReplyKeyboardMarkup(new[]
        {
            new[]
            {
            new KeyboardButton("Заявки")
            },
            new[]
            {
            new KeyboardButton("Закрыть смену диспетчера"),
            },
        });

        //Переменная для сохреннеия меню
        public static ReplyKeyboardMarkup PhaseBoarde = Waiting;
    }
}
