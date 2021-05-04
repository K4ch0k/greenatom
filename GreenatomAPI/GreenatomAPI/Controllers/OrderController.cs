using GreenatomAPI.Entities;
using GreenatomAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace GreenatomAPI.Controllers
{
    /// <summary>
    /// (Работа с заказами)
    /// </summary>
    public class OrderController : ApiController
    {

        /// <summary>
        /// Получение списка заказов
        /// </summary>
        /// <returns>Строка Json, содержащая информацию о всех заказах из БД</returns>
        [HttpGet, Route("AllOrder")]
        public IHttpActionResult GetOrders()
        {
            try
            {
                return Ok(Core.db.Orders.ToList().ConvertAll(item => new OrderModel(item)));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Errors", ex.InnerException);
                ModelState.AddModelError("Errors", ex.InnerException.InnerException);
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Получение информации о заказе
        /// </summary>
        /// <param name="ID">ID заказа</param>
        /// <returns>Строка Json, содержащая информацию о заказе из БД</returns>
        [HttpGet, Route("DetailsOrder")]
        public IHttpActionResult DetailsOrder(int ID)
        {
            try
            {
                Orders Search = Core.db.Orders.Find(ID);
                if (Search == null)
                {
                    ModelState.AddModelError("Error", "Такого заказа не существует");
                    return BadRequest(ModelState);
                }
                return Ok(new DetailsOrderModel(Search));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Errors", ex.InnerException);
                ModelState.AddModelError("Errors", ex.InnerException.InnerException);
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Добавление нового пустого заказа
        /// </summary>
        /// <param name="NewOrder">Данные для заполнения заказа</param>
        /// <returns>Добавленный экземпляр</returns>
        [HttpPost, Route("CreateOrder")]
        public IHttpActionResult CreateOrder(CreateOrderModel NewOrder)
        {
            try
            {
                var Search = Core.db.Users.Find(NewOrder.UserID);
                if (Search == null)
                    ModelState.AddModelError("Errors", "Пользователя с таким ID не существует");
                if (NewOrder.Amount < 0)
                    ModelState.AddModelError("Errors", "Поле \"Amount\" не может быть отрицательным");
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                Orders CreateOrder = new Orders();
                CreateOrder.UserID = NewOrder.UserID;
                CreateOrder.Amount = NewOrder.Amount;
                CreateOrder.Datetime = DateTime.Now;

                Core.db.Orders.Add(CreateOrder);
                Core.db.SaveChanges();
                return CreatedAtRoute("Default", CreateOrder.ID, new OrderModel(CreateOrder));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Errors", ex.InnerException);
                ModelState.AddModelError("Errors", ex.InnerException.InnerException);
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Удаление заказа
        /// </summary>
        /// <param name="ID">ID заказа</param>
        /// <returns>Удаленный экземпляр</returns>
        [HttpDelete, Route("DeleteOrder")]
        public IHttpActionResult DeleteOrder(int ID)
        {
            try
            {
                Orders DeleteOrder = Core.db.Orders.Find(ID);
                if (DeleteOrder == null)
                    ModelState.AddModelError("Errors", "Заказа с таким ID не существует");
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                List<ProductsInOrder> AllProductsInOrder = Core.db.ProductsInOrder.ToList();
                var Search = AllProductsInOrder.FindAll(item => item.OrderID == ID);

                foreach (var item in Search)
                {
                    Core.db.ProductsInOrder.Remove(item);
                }

                Core.db.Orders.Remove(DeleteOrder);
                Core.db.SaveChanges();
                return Ok(DeleteOrder);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Errors", ex.InnerException);
                ModelState.AddModelError("Errors", ex.InnerException.InnerException);
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Изменение информации о заказе
        /// </summary>
        /// <param name="ID">ID заказа</param>
        /// <param name="Amount">Сумма по заказу, на которую нужно поменять текущее значение суммы</param>
        /// <returns>Измененнная запись о заказе</returns>
        [HttpPut, Route("EditOrder")]
        public IHttpActionResult EditOrder(int ID, float Amount)
        {
            try
            {
                Orders EditOrder = Core.db.Orders.Find(ID);
                if (EditOrder == null)
                    ModelState.AddModelError("Errors", "Заказа с таким ID не существует");
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (Amount <= 0)
                    ModelState.AddModelError("Errors", "Сумма по заказу должна быть больше нуля");
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                EditOrder.Datetime = DateTime.Now;
                EditOrder.Amount = Amount;

                Core.db.SaveChanges();
                return Ok(new OrderModel(EditOrder));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Errors", ex.InnerException);
                ModelState.AddModelError("Errors", ex.InnerException.InnerException);
                return BadRequest(ModelState);
            }
        }


    }
}