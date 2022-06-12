using BL;
using Common.Enums;
using Common.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace MedOkBot
{
	public class UserHandler
	{
        private Entities.User User;
        public static async Task CreateNewUser(ITelegramBotClient botClient, Message message)
        {
            await botClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
            await Task.Delay(500);
            await botClient.SendTextMessageAsync(chatId: message.Chat.Id, text: "Привет. Какое-то лицо у тебя незнакомое. Ты нездешний или новичок?",
                replyMarkup: new InlineKeyboardMarkup(
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Нездешний", "ButtonAlien"),
                        InlineKeyboardButton.WithCallbackData("Новичок", "ButtonNewBoy"),
                    }
                ));
        }
        public static async Task ButtonAlien_Click(ITelegramBotClient botClient, CallbackQuery callbackQuery)
		{
            await botClient.SendChatActionAsync(callbackQuery.Message!.Chat.Id, ChatAction.Typing);
            await Task.Delay(500);
            await botClient.SendTextMessageAsync(chatId: callbackQuery.Message!.Chat.Id, text: "Извини, тогда я пожалуй пойду");
        }
        public static async Task ButtonNewBoy_Click(ITelegramBotClient botClient, CallbackQuery callbackQuery)
		{
            var user = new Entities.User(0, callbackQuery.Message!.Chat.Id.ToString(), UserRole.NewBoy, false, DateTime.Now, "", "", null, null, 
                Departments.WithOutDepartment, Bots.Unknown, null, callbackQuery.Message!.Chat.Id, RegistrationState.AskName);
            await new UsersBL().AddOrUpdateAsync(user);
            await botClient.SendChatActionAsync(callbackQuery.Message!.Chat.Id, ChatAction.Typing);
            await Task.Delay(500);
            await botClient.SendTextMessageAsync(chatId: callbackQuery.Message!.Chat.Id, text: "Круто. Как тебя зовут?");
            await botClient.SendTextMessageAsync(chatId: callbackQuery.Message!.Chat.Id, text: "Красивое имя, а меня зовут...",
                 replyMarkup: new InlineKeyboardMarkup(
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Том", "ButtonSetTom"),
                        InlineKeyboardButton.WithCallbackData("Бен", "ButtonSetBen"),
                        InlineKeyboardButton.WithCallbackData("Анжела", "ButtonSetAngela"),
                    }
            ));
        }
        public static async Task ButtonSetTom_Click(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            await botClient.SendChatActionAsync(callbackQuery.Message!.Chat.Id, ChatAction.Typing);
            await Task.Delay(500);
            await botClient.EditMessageTextAsync(chatId: callbackQuery.Message!.Chat.Id, messageId: callbackQuery.Message!.MessageId, text: "Красивое имя, а меня зовут Том, приятно познакомиться");
            var user = (await new UsersBL().GetAsync(new UsersSearchParams() { TelegramChatId = callbackQuery.Message!.Chat.Id })).Objects.FirstOrDefault();
            user.BotId = Bots.Tom;
            await new UsersBL().AddOrUpdateAsync(user);
            await RegistrationUser(botClient, callbackQuery.Message!.Chat.Id);
        }
        public static async Task ButtonSetBen_Click(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            await botClient.SendChatActionAsync(callbackQuery.Message!.Chat.Id, ChatAction.Typing);
            await Task.Delay(500);
            await botClient.EditMessageTextAsync(chatId: callbackQuery.Message!.Chat.Id, messageId: callbackQuery.Message!.MessageId, text: "Красивое имя, а меня зовут Бэн, приятно познакомиться");
            var user = (await new UsersBL().GetAsync(new UsersSearchParams() { TelegramChatId = callbackQuery.Message!.Chat.Id })).Objects.FirstOrDefault();
            user.BotId = Bots.Ben;
            await new UsersBL().AddOrUpdateAsync(user);
            await botClient.SendStickerAsync(callbackQuery.Message!.Chat.Id, "https://vk.com/doc471787638_637888375?hash=TkBewB17o9BazDeuGxPJuh6hI8ZRFACuYqkFpvt7bkw&dl=bdVfaMspEwb9VJLuLkXZ7x81bBHGRrfsruf3BNh4ZiT");
            await RegistrationUser(botClient, callbackQuery.Message!.Chat.Id);
        }
        public static async Task ButtonSetAngela_Click(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            await botClient.SendChatActionAsync(callbackQuery.Message!.Chat.Id, ChatAction.Typing);
            await Task.Delay(500);
            await botClient.EditMessageTextAsync(chatId: callbackQuery.Message!.Chat.Id, messageId: callbackQuery.Message!.MessageId, text: "Красивое имя, а меня зовут Анжела, приятно познакомиться");
            var user = (await new UsersBL().GetAsync(new UsersSearchParams() { TelegramChatId = callbackQuery.Message!.Chat.Id })).Objects.FirstOrDefault();
            user.BotId = Bots.Angela;
            await new UsersBL().AddOrUpdateAsync(user);
            await RegistrationUser(botClient, callbackQuery.Message!.Chat.Id);
        }
        public static async Task RegistrationUser(ITelegramBotClient botClient, long chatId)
        {
            var user = (await new UsersBL().GetAsync(new UsersSearchParams() { TelegramChatId = chatId })).Objects.FirstOrDefault();
            if(user.RegistrationStateId == RegistrationState.AskName)
			{
                await botClient.SendChatActionAsync(chatId, ChatAction.Typing);
                await Task.Delay(500);
                await botClient.SendTextMessageAsync(chatId, "Давай я заполню, твою анкету работника. Напиши пожалуйста полностью своё ФИО.");
                user.RegistrationStateId = RegistrationState.AskFullname;
                await new UsersBL().AddOrUpdateAsync(user);
            }
        }
        public static async Task<Message> RegistrationUser(ITelegramBotClient botClient, Message message, Entities.User user)
        {
            if (user.RegistrationStateId == RegistrationState.AskFullname)
            {
                user.Firstname = message.Text.Split(' ')[1];
                user.Lastname = message.Text.Split(' ')[0];
                user.Middlename = message.Text.Split(' ')?[2];
                await botClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
                await Task.Delay(500);
                await botClient.SendTextMessageAsync(message.Chat.Id, "Ага");
                
                user.RegistrationStateId = RegistrationState.AskBirthDate;
                await new UsersBL().AddOrUpdateAsync(user);
                return await botClient.SendTextMessageAsync(message.Chat.Id, "Назови мне свою дату рождения."); ;
            }
            if (user.RegistrationStateId == RegistrationState.AskBirthDate)
            {
                user.BirthDate = DateTime.Parse(message.Text);
                await botClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
                await Task.Delay(500);
                await botClient.SendTextMessageAsync(message.Chat.Id, "Ух ты, такой молодой, а уже в такой серьёзной компании, твои близкие наверное тобой гордятся.");
                user.RegistrationStateId = RegistrationState.AskEmail;
                await new UsersBL().AddOrUpdateAsync(user);
                return await botClient.SendTextMessageAsync(message.Chat.Id, "Последнее что мне нужно, это твоя электронная почта."); ;
            }
            if (user.RegistrationStateId == RegistrationState.AskEmail)
            {
                user.Email = message.Text;
                await botClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
                await Task.Delay(500);
                user.RegistrationStateId = RegistrationState.Completed;
                await new UsersBL().AddOrUpdateAsync(user);
                await botClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
                await Task.Delay(500);
                return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                            text: "Готово, можем приступать к работе",
                                                            replyMarkup: Handlers.inlineKeyboard);
            }
            return null;

        }
    }
}
