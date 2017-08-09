using System.Collections.Generic;

namespace FundTrack.DAL.Entities
{
    public class BankImport
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier merchant.
        /// </summary>
        /// <value>
        /// The identifier merchant.
        /// </value>
        public int IdMerchant { get; set; }

        /// <summary>
        /// Gets or sets the signature.
        /// </summary>
        /// <value>
        /// The signature.
        /// </value>
        public string Signature { get; set; }

        /// <summary>
        /// Gets or sets the credit.
        /// </summary>
        /// <value>
        /// The credit.
        /// </value>
        public decimal Credit { get; set; }

        /// <summary>
        /// Gets or sets the debet.
        /// </summary>
        /// <value>
        /// The debet.
        /// </value>
        public decimal Debet { get; set; }
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