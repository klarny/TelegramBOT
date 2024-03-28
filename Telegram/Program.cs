using Telegram.Bot.Polling;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System.Threading.Tasks;
using System.Threading;
using System;

internal class Program
{
    private static void Main(string[] args)
    {
        var client = new TelegramBotClient("6469607980:AAHUGLaWi0o7LxDSWQce2NMojevldMPi-JU");
        client.StartReceiving(UpdateHandler, Error); /*метод, который выводит бот*/
        Console.ReadLine();
    }
    private static async Task MessageHeandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        switch (update.Type)
        {
            case UpdateType.Message:
                {
                    var message = update.Message;
                    var user = message.From;
                    var chat = message.Chat;

                    switch (message.Type)
                    {
                        case MessageType.Text:
                            {
                                if (message.Text == null)
                                {
                                    return;

                                }
                                if (message.Text == "/start")
                                {
                                    //создание кнопок в строке
                                    InlineKeyboardMarkup inlineKeyboard = new(new[]
                                    {
                                        new []
                                        {
                                            InlineKeyboardButton.WithCallbackData(text: "1.1", callbackData: "11"),
                                            InlineKeyboardButton.WithCallbackData(text: "1.2", callbackData: "12"),
                                        },
                                        new []
                                        {
                                            InlineKeyboardButton.WithCallbackData(text: "2.1", callbackData: "21"),
                                            InlineKeyboardButton.WithCallbackData(text: "2.2", callbackData: "22"),
                                        },
                                        });

                                    Message sentMessage = await botClient.SendTextMessageAsync(
                                        chatId: chat.Id,
                                        text: "A message with an inline keyboard markup",
                                        replyMarkup: inlineKeyboard,
                                        cancellationToken: cancellationToken);

                                    return;
                                }
                                return;
                            }
                    }
                    return;
                }
        }

    }
    private static async Task CallBack(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        //вывод в зависимости от выбранной кнопки
        if (update != null && update.CallbackQuery != null)
        {
            string answer = update.CallbackQuery.Data;
            switch (answer)
            {
                case "11":
                    await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "Привет");
                    break;
                case "12":
                    await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, answer);
                    break;
                case "21":
                    await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, answer);
                    break;
                case "22":
                    await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, answer);
                    break;
            }


            //InlineKeyboardMarkup inlineKeyboard = update.CallbackQuery.Message.ReplyMarkup!;
            //var inlines = inlineKeyboard.InlineKeyboard;
            //foreach (var item1 in inlines)
            //{
            //    foreach (var item2 in item1)
            //    {

            //       
            //    }
            //}
        }
    }

    private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
    {
        throw new NotImplementedException();
    }
    private static async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        await CallBack(botClient, update, cancellationToken);
        await MessageHeandler(botClient, update, cancellationToken);

    }
}