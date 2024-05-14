using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;


namespace SummerSchool
{
    class Bot
    {
        
        readonly string token = "7117645507:AAH4A0DXXhQFsccxLyiMBi2zngFdLKPl2gU";
        TelegramBotClient client;

        internal void GetUpdates(string[] answer, string[] command)
        {
            client = new TelegramBotClient(token);
            var me = client.GetMeAsync().Result;
            if (me != null && !string.IsNullOrEmpty(me.Username))
            {
                int offset = 0;
                while (true)
                {
                    try
                    {
                        var updates = client.GetUpdatesAsync(offset).Result;
                        if (updates != null && updates.Count() > 0)
                        {
                            foreach (var update in updates)
                            {
                                ProcessUpdateAsync(update, answer, command);
                                offset = update.Id + 1;
                            }
                        }
                    }
                    catch (Exception ex) { Console.WriteLine(ex); }

                    Thread.Sleep(1000);
                }
            }
        }

        private async void ProcessUpdateAsync(Telegram.Bot.Types.Update update, string[] answer, string[] command)
        {
            var chat = update.Message.Chat.Id; 
            switch (update.Type)
            {
                case UpdateType.Message:
                    switch (update.Message.Text.ToString())
                    {

                        case "/start":
                            await client.SendTextMessageAsync(chat, answer[0], replyMarkup: GetButtons("", "", ""));
                            
                            SendPhoto(update, "photo\\Слайд_17.png", "");
                            Thread.Sleep(1500);
                            await client.SendTextMessageAsync(chat, answer[1] , replyMarkup: GetButtons("", "", ""));
                            Thread.Sleep(3000);
                            await client.SendTextMessageAsync(chat, answer[2], replyMarkup: GetButtons("", "", ""));
                            Thread.Sleep(1500);
                            await client.SendTextMessageAsync(chat, answer[3], replyMarkup: GetButtons("", "", ""));
                            await client.SendTextMessageAsync(chat, answer[4], replyMarkup: GetButtons("", "", ""));
                            
                            SendPhoto(update, "photo\\Фото 1.png", "");
                            await client.SendTextMessageAsync(chat, answer[5], replyMarkup: GetButtons("", "", ""));
                            await client.SendTextMessageAsync(chat, answer[6], replyMarkup: GetButtons("", "", ""));
                            
                            SendPhoto(update, "photo\\Буклет_Белорусский.png", "");
                            await client.SendTextMessageAsync(chat, answer[7], replyMarkup: GetButtons("", "", ""));
                            
                            SendPhoto(update, "photo\\Буклет_Английский.png", "");

                            await client.SendTextMessageAsync(chat, answer[8], replyMarkup: GetButtons("", "", ""));
                            await client.SendTextMessageAsync(chat, answer[9], parseMode: ParseMode.Html, replyMarkup: GetButtons("", "", ""));
                            
                            SendPhoto(update, "photo\\Фото Гродно.png", "");

                            await client.SendTextMessageAsync(chat, answer[10], parseMode: ParseMode.Html, replyMarkup: GetButtons(command[0], "", ""));
                            break;
                        case "Спасибо за информацию":
                            await client.SendTextMessageAsync(chat, answer[11], parseMode: ParseMode.Html,   replyMarkup: GetButtons(command[1], "", ""));
                            
                            SendAudio(update, "music\\Спадчына.mp3", "");
                            break;
                        case "Продолжить":
                            await client.SendTextMessageAsync(chat, answer[12],  parseMode: ParseMode.Html, replyMarkup: GetButtons(command[2], "", ""));
                            break;
                        case "Прoдолжить":
                            await client.SendTextMessageAsync(chat, answer[13], parseMode: ParseMode.Html, replyMarkup: GetButtons("", "", command[3]));
                            break;
                        case "Прoдoлжить":
                            await client.SendTextMessageAsync(chat, answer[14], replyMarkup: GetButtons(command[4], command[5], ""));
                            break;
                        case "История":
                            await client.SendTextMessageAsync(chat, answer[15],  parseMode: ParseMode.Html, replyMarkup: GetButtons("", "", ""));
                            await client.SendTextMessageAsync(chat, answer[16],  parseMode: ParseMode.Html,  replyMarkup: GetButtons("", "", ""));
                            await client.SendTextMessageAsync(chat, answer[17]);
                            SendPhoto(update, "photo\\Фото 2.png", "");
                            await client.SendTextMessageAsync(chat, answer[18]);
                            SendPhoto(update, "photo\\Фото 3.png", "");
                            await client.SendTextMessageAsync(chat, answer[19]);
                            SendPhoto(update, "photo\\Фото 4.png", "");
                            await client.SendTextMessageAsync(chat, answer[20]);
                            SendFile(update, "document\\ремёсла.pptx", "");
                            await client.SendTextMessageAsync(chat, answer[21]);
                            SendAudio(update, "music\\Калина-Калина.mp3", "");
                            SendAudio(update, "music\\Ой рано на Йвана.mp3", "");
                            SendAudio(update, "music\\Край верасов.mp3", "");
                            SendAudio(update, "music\\Очи незабудки.mp3", command[5]);
                            break;
                        case "Современность":
                            await client.SendTextMessageAsync(chat, answer[22], parseMode: ParseMode.Html);
                            SendFile(update, "document\\литература.pptx", "");
                            await client.SendTextMessageAsync(chat, answer[21]);
                            SendAudio(update, "music\\Музыка трау.mp3", "");
                            SendAudio(update, "music\\Белая птушка.mp3", "");
                            SendAudio(update, "music\\Мой край.mp3", "");
                            SendAudio(update, "music\\Беларусь моя.mp3", "");
                            await client.SendTextMessageAsync(chat, answer[23], parseMode: ParseMode.Html, replyMarkup: GetButtons("", "", ""));
                            SendPhoto(update, "photo\\Фото 6.png", command[4]);
                            break;
                        case "готово":
                            await client.SendTextMessageAsync(chat, answer[24], replyMarkup: GetButtons(command[6], "", ""), parseMode: ParseMode.Html);
                            break;
                        case "Готово":
                            await client.SendTextMessageAsync(chat, answer[25], parseMode: ParseMode.Html);
                            await client.SendTextMessageAsync(chat, answer[26],  replyMarkup: GetButtons("", "", ""), parseMode: ParseMode.Html);

                            
                            SendFile(update, "document\\контакты.pdf", command[7]);
                            break;
                        case "Пpoдoлжить":
                            await client.SendTextMessageAsync(chat, answer[27], replyMarkup: GetButtons(command[8], "", ""));
                            SendFile(update, "document\\Программа.pdf", "");
                            break;
                        case "Ознакомился":
                            await client.SendTextMessageAsync(chat, answer[28], replyMarkup: GetButtons("", "", ""));
                            
                            SendPhoto(update, "photo\\Цена.png", command[9]);
                            
                            break;
                        case "Спасибо":
                            await client.SendTextMessageAsync(chat, answer[29]);
                            await client.SendTextMessageAsync(chat, answer[30]);
                            break;
                        default:
                            await client.SendTextMessageAsync(chat, answer[31]);
                            break;

                    }
                    break;
                default:
                    Console.WriteLine(answer[32]);
                    break;

            }

        }

        private IReplyMarkup GetButtons(string name1, string name2, string name3)
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{new KeyboardButton { Text = name1},
                        new KeyboardButton { Text = name2},
                        new KeyboardButton { Text = name3}}
                },
                ResizeKeyboard = true
            };

        }

        private void SendPhoto(Telegram.Bot.Types.Update update, string name, string command)
        {
            var chat = update.Message.Chat.Id;
            var FilePath = Path.Combine(Environment.CurrentDirectory, name);
            using (var stream = System.IO.File.OpenRead(FilePath))
            {
                var r = client.SendPhotoAsync(chat, new Telegram.Bot.Types.InputFiles.InputOnlineFile(stream), replyMarkup: GetButtons(command, "", "")).Result;
            }
        }

        private void SendFile(Telegram.Bot.Types.Update update, string name, string command)
        {
            var chat = update.Message.Chat.Id;
            var FilePath = Path.Combine(Environment.CurrentDirectory, name);
            using (var stream = System.IO.File.OpenRead(FilePath))
            {
                var r = client.SendDocumentAsync(chat, new Telegram.Bot.Types.InputFiles.InputOnlineFile(content: stream, fileName: FilePath), replyMarkup: GetButtons(command, "", "")).Result;
            }
        }

        private void SendAudio(Telegram.Bot.Types.Update update, string name, string command)
        {
            var chat = update.Message.Chat.Id;
            var FilePath = Path.Combine(Environment.CurrentDirectory, name);
            using (var stream = System.IO.File.OpenRead(FilePath))
            {
                var r = client.SendAudioAsync(chat, new Telegram.Bot.Types.InputFiles.InputOnlineFile(content: stream, fileName: FilePath), replyMarkup: GetButtons(command, "", "")).Result;
            }
        }

    }
}
