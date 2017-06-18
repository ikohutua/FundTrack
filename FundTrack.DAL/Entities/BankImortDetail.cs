using System;

namespace FundTrack.DAL.Entities
{
    public class BankImportDetail
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the bank import identifier.
        /// </summary>
        /// <value>
        /// The bank import identifier.
        /// </value>
        public int BankImportId { get; set; }

        /// <summary>
        /// Gets or sets the card.
        /// </summary>
        /// <value>
        /// The card.
        /// </value>
        public int Card { get; set; }

        /// <summary>
        /// Gets or sets the trandate.
        /// </summary>
        /// <value>
        /// The trandate.
        /// </value>
        public DateTime Trandate { get; set; }

        /// <summary>
        /// Gets or sets the trantime.
        /// </summary>
        /// <value>
        /// The trantime.
        /// </value>
        public DateTime Trantime { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public string Amount { get; set; }

        /// <summary>
        /// Gets or sets the card amount.
        /// </summary>
        /// <value>
        /// The card amount.
        /// </value>
        public string CardAmount { get; set; }

        /// <summary>
        /// Gets or sets the rest.
        /// </summary>
        /// <value>
        /// The rest.
        /// </value>
        public string Rest { get; set; }

        /// <summary>
        /// Gets or sets the terminal.
        /// </summary>
        /// <value>
        /// The terminal.
        /// </value>
        public string Terminal { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the bank import.
        /// </summary>
        /// <value>
        /// The bank import.
        /// </value>
        public virtual BankImport BankImport { get; set; }
    }
}

//https://api.privatbank.ua/#p24/orders

//Название         Описание
//id              ID мерчанта, зарегистрированного в Приват24
//signature       Сигнатура ответа рассчитывется следующим образом(PHP): 
//                  $sign = sha1(md5($data.$password)); 
//                  $data - содержимое тега<data> ответа, 
//                  $password - личный пароль мерчанта, полученный им при регистрации. 
//                Процедура проверки результатов: 
//                  Сформировать из содержимого подпись. 
//                  Сравнить подпись с той, что находится в самом пакете<signature/>.
//                  Если подпись совпадает – обработать содержимое ответа, если нет – отбросить результаты ответа
//credit          Сумма поступлений
//debet           Сумма отчислений
//card            Номер карты
//trandate        Дата транзакции
//trantime        Время транзакции
//amount          Сумма и валюта транзакции
//cardamount      Движение по карте в валюте карты
//rest            Сумма остатка и валюта после транзакции
//terminal        Канал, через который была произведена операция и его местоположение
//description     Детали (описание транзакции)