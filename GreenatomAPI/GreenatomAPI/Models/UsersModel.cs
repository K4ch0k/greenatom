using GreenatomAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenatomAPI.Models
{
    public class UsersModel
    {
        /// <summary>
        /// Осуществление преобравзования информации о пользователе из БД в приемлимый вид 
        /// </summary>
        /// <param name="user">Запись в таблице Users</param>
        public UsersModel(Users user)
        {
            ID = user.ID;
            Name = user.Name;
            Surname = user.Surname;
            Lastname = user.Lastname;
            Address = user.Address;
            Phone = user.Phone;
            Email = user.Email;
        }

        /// <summary>
        /// ID пользователя
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string Lastname { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
    }
}