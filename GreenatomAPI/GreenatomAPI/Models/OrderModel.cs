using GreenatomAPI.Entities;

namespace GreenatomAPI.Models
{
    public class OrderModel
    {
        /// <summary>
        /// Осуществление преобравзования заказа из БД в приемлимый вид
        /// </summary>
        /// <param name="order">Запись в таблице Orders</param>
        public OrderModel(Orders order)
        {
            ID = order.ID;
            Datetime = order.Datetime;
            Amount = order.Amount;
            User = new UsersModel(order.Users);
        }

        /// <summary>
        /// ID заказа
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Дата и время совершения заказа
        /// </summary>
        public System.DateTime Datetime { get; set; }

        /// <summary>
        /// Стоимость заказа
        /// </summary>
        public float Amount { get; set; }

        /// <summary>
        /// Пользователь, совершивший заказ
        /// </summary>
        public UsersModel User { get; set; }
    }
}