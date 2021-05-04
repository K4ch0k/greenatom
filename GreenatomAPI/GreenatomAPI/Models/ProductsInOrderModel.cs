using GreenatomAPI.Entities;
using System;

namespace GreenatomAPI.Models
{
    public class ProductsInOrderModel
    {
        /// <summary>
        /// Осуществление преобравзования продукта из заказа в приемлимый вид
        /// </summary>
        /// <param name="product">Запись в таблице Products</param>
        public ProductsInOrderModel(ProductsInOrder product)
        {
            ID = product.ID;
            Product = new ProductsModel(product.Products);
            Status = product.Status.Name;
            Quantity = product.Quantity;
            Discount = product.Discount;
            Amount = product.Amoount;
        }

        /// <summary>
        /// ID записи
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Продукт
        /// </summary>
        public ProductsModel Product { get; set; }

        /// <summary>
        /// Статус оформления продукта в заказе
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Количество продукта с заданным ID в заказе
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Скидка на продукт
        /// </summary>
        public float? Discount { get; set; }

        /// <summary>
        /// Стоимость продукта в заказе
        /// </summary>
        public float Amount { get; set; }
    }
}