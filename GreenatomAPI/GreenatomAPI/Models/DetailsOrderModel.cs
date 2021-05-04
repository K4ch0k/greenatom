using GreenatomAPI.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GreenatomAPI.Models
{
    public class DetailsOrderModel
    {
        /// <summary>
        /// Осуществление преобравзования заказа из БД в приемлимый вид
        /// </summary>
        /// <param name="order">Запись в таблице Orders</param>
        public DetailsOrderModel(Orders order)
        {
            ID = order.ID;
            Datetime = order.Datetime;
            Amount = order.Amount;
            User = new UsersModel(order.Users);
            ProductsInOrder = order.ProductsInOrder.ToList().ConvertAll(item => new ProductsInOrderModel(item));
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

        /// <summary>
        /// Список продуктов в заказе
        /// </summary>
        public List<ProductsInOrderModel> ProductsInOrder { get; set; }

    }
}