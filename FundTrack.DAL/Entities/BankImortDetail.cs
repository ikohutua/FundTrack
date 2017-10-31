using System;
using Microsoft.EntityFrameworkCore;

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
        /// Gets or sets the card.
        /// </summary>
        /// <value>
        /// The card.
        /// </value>
        public string Card { get; set; }

        /// <summary>
        /// Gets or sets the trandate.
        /// </summary>
        /// <value>
        /// The trandate.
        /// </value>
        public DateTime Trandate { get; set; }

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
        /// Gets or Sets AppCode property
        /// </summary>
        public int? AppCode { get; set; }

        /// <summary>
        /// Gets or Sets FinOpId 
        /// </summary>
        public int? FinOpId { get; set; }

        public bool IsLooked { get; set; }

        public virtual FinOp FinOp { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankImportDetail>(entity =>
            {
                entity.HasOne(e => e.FinOp)
                                    .WithMany(c => c.BankImportDetails)
                                    .HasForeignKey(e => e.FinOpId)
                                    .HasConstraintName("FK_BankImportDetails_FinOp");

                entity.HasKey(bid => bid.Id).HasName("PK_BankImportDetail");

                entity.Property(bid => bid.Card).IsRequired().HasMaxLength(16);

                entity.Property(bid => bid.Trandate).IsRequired().HasColumnType("datetime");

                entity.Property(bid => bid.IsLooked).IsRequired();

                entity.Property(bid => bid.Amount).IsRequired();

                entity.Property(bid => bid.CardAmount).IsRequired();

                entity.Property(bid => bid.Rest).IsRequired();

                entity.Property(bid => bid.Terminal).IsRequired();

                entity.Property(bid => bid.Description).IsRequired();
                entity.Property(bid => bid.AppCode).HasMaxLength(8);

            });
        }
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