﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace SummerSchool
{

    //public class Inf
    //{
    //    public string[] Commands = new string[] {
    //    "Спасибо за информацию",
    //    "Продолжить",
    //    "Прoдолжить",
    //    "Прoдoлжить",
    //    "История",
    //    "Современность",
    //    "Готово",
    //    "Пpoдoлжить",
    //    "Ознакомился",
    //    "Спасибо"
    //    };
    //    public string[] Answer = new string[] { 
    //        "Добры дзень! Вас приветствует Летняя школа белорусского языка и культуры", 
    //        "Давайте познакомимся! Наша школа осуществляет уже третий набор слушателей. Занятия проходят на базе Научной библиотеки Гродненского государственного университета имени Янки Купалыв городе Гродно в Республике Беларусь. Руководитель Летней школы белорусского языка и культуры–директор Научной библиотеки Николай Гринько.Программа Летней школы предусматривает:\n\n 1)лекции и практические занятия по белорусскому языку; \n 2)тематические лекции по истории национальной литературы и культуры Беларуси; \n 3)мастер-классы по белорусской кухне, народному ткачеству.",
    //        "В нашей школе могут обучаться люди из разных стран и любых возрастов! Например, в прошлом году у нас прошли обучение:\n\n1)студенты из Германии; \n2)преподаватели, аспиранты, студенты из Российской Федерации.",
    //        "Наша программа направлена на тех, кто только начинает изучать белорусистику, а также желает усовершенствовать свои знания и навыки в области белорусского языка и культуры Беларуси. Мы приглашаем Вас в нашу школу, где Вас ждут:\n\n1)креативные занятия по белорусскому языку,\n2)открытие Беларуси, ее традиций и красоты,\n3)знакомство с традиционной белорусской культурой: мастер-классы по народным ремеслам,\n4)путешествия по знаковым местам города Гродно и его региона,\n5)активный отдых.",
    //        "Ниже Вы можете  увидеть фото с  прошлых лет обучения.",
    //        "Предлагаем вам ознакомиться с буклетом.",
    //        "Буклет на белорусском:",
    //        "Буклет на английском:",
    //        "Предлагаем Вам познакомится с городом Гродно и нашим университетом, а позже мы расскажем Вам о программе школы.",
    //        "Гродно – это уютный и живописный город, который приветливо встречает гостей и предлагает множество интересных мест для посещения. В городе есть много достопримечательностей (https://www.tripadvisor.ru/Attractions-g661665-Activities-a_allAttractions.true-Grodno_Grodno_Region.html). В Гродно  можно посетить музеи(https://34travel.me/gotobelarus/post/8-neobychnykh-muzeev-grodno?ysclid=lu5ik6d4mr16954256), галереи (https://artschool.grsu.by/ru/uslugi1/180) и выставки (https://grodno.in/afisha/exhibition/?ysclid=lu5iu8aon8918394690), чтобы познакомиться с историей и культурой города. Он также славится своими кулинарными традициями, включая национальные белорусские блюда, которые можно попробовать в местных ресторанах и кафе (https://citymix.by/grodno/gastromix/catalog/f:belarusian?ysclid=lu5iy57m9z890242806).",
    //        "ГрГУ имени Янки Купалы (Гродненский государственный университет имени Янки Купалы) – это один из ведущих университетов в Гродно, Беларусь. Университет был основан в 1940 году и назван в честь выдающегося белорусского поэта Янки Купалы (https://biographe.ru/uchenie/yanka-kupala/?ysclid=lu5jaf0pvj484053990).Университет является важным центром образования и культуры и привлекает студентов со всей страны и за её пределами. Познакомиться подробнее можно на официальном сайте: https://www.grsu.by/ \n\n Предлагаем Вам посмотреть видеоролик о университете:https://www.youtube.com/watch?v=MnmZEaRC5HE&t=42s",
    //        "Сейчас мы хотим Вас погрузить мир нашей страны, Беларуси! \n Белорусский язык –это красивый и мелодичный славянский язык, который звучит очень утонченно и приятно на слух.Он обладает богатой историей(https://pikabu.ru/story/belorusskiy_yazyik_istoriya_i_razvitie_6707989?ysclid=lu5jrqv5l4919358313) и культурным наследием, что делает его еще более привлекательным для изучения. Погрузиться в мир белорусского языка – значит открыть для себя новые возможности для общения, познания культуры и расширения своих горизонтов. Попробуйте послушать белорусский язык, он зачарует Вас своей красотой и уникальностью.",
    //        "В 2020 году вышел фильм, который знакомит зрителей с судьбой народного поэта Беларуси Янки Купалы. Вы можете посмотреть трейлер фильма «Купала», ещё раз послушать звучание белорусского языка, а позже посмотреть фильм целиком. https://www.youtube.com/watch?v=-erawIF0390.",
    //        "Мы предлагаем Вам пройти викторину «Беларусь». Язык – часть культуры, страны. Знание о стране, язык которой Вы изучаете, может быть полезен для более глубокого понимания языка и его культурных особенностей, традиций и ценностей: https://kvizzi.ru/viktorina-respublika-belarus/?ysclid=ltx6tfynwv718028033 \n\nНажмите «Продолжить» после прохождения теста.",
    //        "Мы надеемся, вы стравились с викториной , теперь предлагаем Вам познакомиться с прекрасной и богатой белорусской культурой. По окончанию ознакомления напишите в чат «готово» ",
    //        "Беларусь - страна с богатой историей, которая оставила свой след в мировой истории. Великая Отечественная война (https://www.belta.by/society/view/dose-belarus-v-gody-velikoj-otechestvennoj-vojny-tsifry-i-fakty-446805-2021/?ysclid=lu5s1ogn40464644499) оставила неизгладимый след в сердцах белорусского народа, который дал бой фашистским захватчикам на своей земле. Чернобыльская катастрофа (https://chernobyl.mchs.gov.by/informatsionnyy-tsentr/posledstviya-chernobylskoy-katastrofy-dlya-belarusi/) также не оставила равнодушными жителей Беларуси, оставивших свои дома и земли из-за радиационного загрязнения. Но, несмотря на все испытания, белорусский народ продолжает жить и творить.",
    //        "К историческому наследию Беларуси можно отнести: \n1)народные праздники (https://problr.by/tradiczii-i-prazdniki-belarusi.html?ysclid=lu5t9f6ou0800480824), \n2)кухня((https://probelarus.by/belarus/kitchen/bel-kitchen.html?ysclid=lu5izmwxop501842663), \n3)литература (https://planetabelarus.by/publications/top-10-izvestnykh-pisateley-rodivshikhsya-na-territorii-nyneshney-belarusi/?ysclid=lu5x6ibfvu14225779), \n4)наука(https://adukar.com/by/news/abiturientu/izvestnye-uchyonye-belarusi),",
    //        "5)живопись",
    //        "6)достопримечательности",
    //        "7)национальная одежда",
    //        "8)ремёсла",
    //        "9)музыка",
    //        "Современная Беларусь характеризуется культурным разнообразием, активным участием в международных отношениях, стремлением к инновациям и технологическому прогрессу. В стране развиваются: \n\n1)образование (https://adukar.com/by/catalog-vuz?page=1&), \n2)наука (https://nashilyudi.by/list/uchenye/?ysclid=lu5t2zwmbp174720442), \n3)живопись (https://dzen.ru/a/Y1t-KSc4Ihz82W8u), \n4)спорт (https://president.gov.by/ru/belarus/social/sport), \n5)Промышленность(смотреть видео)(https://www.youtube.com/watch?v=zpau-ALHhFo&t=13s), \n 6)театры (https://ru.belarus.travel/news/main-belarus-theater?ysclid=lu5unyhoy8239635525), \n7)одежда(https://dzen.ru/a/X-XKtA0Md1msGxIU), \n8)литература",
    //        "Современные Праздники (https://president.gov.by/ru/gosudarstvo/prazdniki)сочетаются вместе с народными, и каждый год белорусы отмечают множество праздников. Города Беларуси(https://wikiway.com/belarus/photo/gorodov/) сочетают в себе древнюю архитектуру и современные достижения. Природа Беларуси удивляет своим разнообразием и красотой.",
    //        "Мы рады, что Вы погрузились  в удивительный мир белорусской культуры. Давайте проверим, как хорошо вы знаете белорусский язык. Перейдите, пожалуйста, по ссылкеи попробуйте пройти тест:https://trikky.ru/kak-horosho-ty-znaesh-belorusskiy-yazyk-524362.html",
    //        "Настроились на обучение белорусского языка? Переходите по ссылке на онлайн-регистрацию https://belmova.grsu.by/by/anlajn-registratsyya.",
    //        "Если у Вас есть вопросы, Вы можете позвонить организаторам и узнать всё, что Вас интересует. Мы с радостью ждём Вас и Ваших друзей в нашей школе. Помимо изучения языка вы обретёте новые знакомства, множество положительных и незабываемых эмоций. Всю информацию вы можете посмотреть у нас на  сайте https://belmova.grsu.by/by/.",
    //        "Теперь давайте ознакомимся с программой нашей школы.",
    //        "Стоимость участия в Летней школе белорусского языка и культуры 2023 г. составляет 135 евро / 150 долларов.\n Программа выходного дня(приблизительная стоимость одной экскурсии – 30 евро \n Стоимость проживания в студенческом общежитии ГрГУ имени Янки Купалы в номере гостиничного типа от 8 до 27 евро за 1 сутки.",
    //        "Мы надеемся, Вам понравилась «экскурсия» по Беларуси! Будем рады видеть Вас в качестве участника Летней школы белорусского языка и культуры! \n КОНТАКТЫ:\n1)Республика Беларусь, г. Гродно, ул. Ожешко, 22, ауд. 116 \n2)Тел.: + (375) 152 731924 \n3)e-mail: library@grsu.by; libgrgu@gmail.com",
    //        "До свидания!",
    //        "Данное сообщение не обрабатывается",
    //        "Ошибка в обработке данного сообщения"};
    //}

    class Program
    {
        static void Main(string[] args)
        {
            //сделать json файл а в нём ссылки
            //разобраться с пдф файлами
            //Переделать ссылку на слово трейлер, а не на фильм
            var information = JsonConvert.DeserializeObject<Information>(System.IO.File.ReadAllText("Information.json"));
            try
            {
                Bot bot = new Bot();
                bot.GetUpdates(information.Answer, information.Commands);
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}
