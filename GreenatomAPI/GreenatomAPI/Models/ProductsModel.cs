using GreenatomAPI.Entities;

namespace GreenatomAPI.Models
{
    public class ProductsModel
    {
        /// <summary>
        /// Осуществление преобравзования продукта из БД в приемлимый вид
        /// </summary>
        /// <param name="product">Запись в таблице Products</param>
        public ProductsModel(Products product)
        {
            ID = product.ID;
            Name = product.Name;
            Price = product.Price;
            Description = product.Description;
        }

        /// <summary>
        /// ID товара
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Наименование товара
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Стоимость товара
        /// </summary>
        public float Price { get; set; }

        /// <summary>
        /// Описание товара
        /// </summary>
        public string Description { get; set; }
    }
}