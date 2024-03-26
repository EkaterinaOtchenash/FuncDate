using System;
using System.Collections.Generic;
using System.Globalization;

namespace BookingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            //имитация данных в БД
            List<Booking> bookings = new List<Booking>
            {
                new Booking { Date = new DateTime(2023, 03, 10, 12, 00, 00), Guests = 20 },
                new Booking { Date = new DateTime(2023, 03, 11, 14, 30, 00), Guests = 15 },
                new Booking { Date = new DateTime(2023, 03, 12, 10, 00, 00), Guests = 25 },
                new Booking { Date = new DateTime(2023, 03, 13, 16, 30, 00), Guests = 10 },
                new Booking { Date = new DateTime(2023, 03, 14, 11, 00, 00), Guests = 35 }
            };

            //Это создано для проверки данных, будто мы добавляем новую заявку
            Console.Write("Введите дату и время в формате dd.MM.yyyy HH:mm: ");
            string inputDateTime = Console.ReadLine();
            DateTime dateTime = DateTime.ParseExact(inputDateTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);

            Console.Write("Введите количество гостей: ");
            string inputGuests = Console.ReadLine();
            int guests = int.Parse(inputGuests);


            if (CanAddBooking(dateTime, guests, bookings))
            {
                Console.WriteLine("Можно добавить новую запись.");
            }
            else
            {
                Console.WriteLine("Нельзя добавить новую запись.");
            }

            Console.ReadKey();
        }

        static bool CanAddBooking(DateTime dateTime, int guests, List<Booking> bookings)
        {
            // Проверяем, есть ли уже запись на эту дату и время
            var existingBooking = bookings.Find(b => b.Date == dateTime);

            if (existingBooking != null)
            {
                // Если количество гостей превышает 30, то нельзя добавить новую запись
                if (existingBooking.Guests + guests > 30)
                {
                    return false;
                }
                else
                {
                    // Добавляем гостей к существующей записи
                    existingBooking.Guests += guests;
                    return true;
                }
            }
            else
            {
                // Проверяем, есть ли записи на эту дату и время с разницей в час
                var oneHourBefore = dateTime.AddHours(-1);
                var oneHourAfter = dateTime.AddHours(1);
                var nearbyBookings = bookings.FindAll(b => b.Date >= oneHourBefore && b.Date <= oneHourAfter);

                if (nearbyBookings.Count > 0)
                {
                    // Если есть записи на эту дату и время с разницей в час, то добавляем новую запись
                    bookings.Add(new Booking { Date = dateTime, Guests = guests });
                    return true;
                }
                else
                {
                    // Если нет записей на эту дату и время, то добавляем новую запись
                    bookings.Add(new Booking { Date = dateTime, Guests = guests });
                    return true;
                }
            }
        }
    }

    //опять-таки класс для имитации объекта в БД
    class Booking
    {
        public DateTime Date { get; set; }
        public int Guests { get; set; }
    }
}
