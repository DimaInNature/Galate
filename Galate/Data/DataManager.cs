using Galate.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Galate.Data
{
    public sealed class DataManager
    {
        public sealed class User
        {
            public static UserModel GetByLoginAndPassword(string login, string password)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM GalateUsers WHERE Login = @Login AND Password = @Password", db.getConnection());
                    cmd.Parameters.Add("@Login", MySqlDbType.VarChar).Value = login;
                    cmd.Parameters.Add("@Password", MySqlDbType.VarChar).Value = password;

                    db.openConnection();

                    using (MySqlDataReader read = cmd.ExecuteReader())
                    {
                        if (!read.Read()) { }

                        UserModel outData = new UserModel()
                        {
                            Id = (int)read["Id"],
                            Name = (string)read["Name"],
                            Surname = (string)read["Surname"],
                            Email = (string)read["Email"],
                            Login = (string)read["Login"],
                            Password = (string)read["Password"],
                            IsAdmin = (bool)read["IsAdmin"],
                        };
                        db.closeConnection();
                        return outData;
                    }
                }
            }

            public static bool Create(UserModel user)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    MySqlCommand command = new MySqlCommand("INSERT INTO GalateUsers (Name, " +
                                                        $"Surname, Email, Login, Password, IsAdmin) " +
                                                        $"VALUES (N'{user.Name}', N'{user.Surname}', " +
                                                        $"N'{user.Email}', N'{user.Login}', N'{user.Password}', " +
                                                        "@IsAdmin)", db.getConnection());

                    command.Parameters.Add("@IsAdmin", MySqlDbType.Byte).Value = user.IsAdmin;

                    db.openConnection();

                    if (command.ExecuteNonQuery() == 1)
                    {
                        db.closeConnection();
                        return true;
                    }
                    else
                    {
                        db.closeConnection();
                        return false;
                    }
                }
            }
        }

        public sealed class Tour
        {
            public static IEnumerable<TourModel> GetAll()
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    MySqlCommand command = new MySqlCommand("SELECT * FROM GalateTours", db.getConnection());

                    db.openConnection();

                    using (MySqlDataReader read = command.ExecuteReader())
                    {
                        while (true)
                        {
                            if (!read.Read()) break;

                            TourModel outData = new TourModel()
                            {
                                Id = (int)read["Id"],
                                TourName = (string)read["Name"],
                                TourDate = (DateTime)read["Date"],
                                Price = (double)read["Price"]
                            };

                            outData.FormatPrice = $"{outData.Price} руб.";
                            outData.StringTourDate = outData.TourDate.ToString("dd.MM.yyyy");

                            yield return outData;
                        }
                    }

                    db.closeConnection();
                }
            }

            public static TourModel GetById(int id)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    MySqlCommand cmd = new MySqlCommand($"SELECT * FROM GalateTours WHERE Id = {id}", db.getConnection());

                    db.openConnection();

                    using (MySqlDataReader read = cmd.ExecuteReader())
                    {
                        if (!read.Read()) { }

                        TourModel outData = new TourModel()
                        {
                            Id = (int)read["Id"],
                            TourName = (string)read["Name"],
                            TourDate = (DateTime)read["Date"],
                            Price = (double)read["Price"]
                        };

                        outData.FormatPrice = $"{outData.Price} руб.";
                      
                        db.closeConnection();

                        return outData;
                    }
                }
            }

            public static bool Create(TourModel tour)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    MySqlCommand command = new MySqlCommand("INSERT INTO GalateTours (Name, Date, Price) " +
                                                        $"VALUES (N'{tour.TourName}', @Date, {tour.Price})", db.getConnection());

                    command.Parameters.Add("@Date", MySqlDbType.Date).Value = tour.TourDate;

                    db.openConnection();

                    if (command.ExecuteNonQuery() == 1)
                    {
                        db.closeConnection();
                        return true;
                    }
                    else
                    {
                        db.closeConnection();
                        return false;
                    }
                }
            }

            public static bool Delete(int id)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    MySqlCommand command = new MySqlCommand($"DELETE FROM GalateTours WHERE Id = {id}", db.getConnection());

                    db.openConnection();

                    if (command.ExecuteNonQuery() == 1)
                    {
                        db.closeConnection();
                        return true;
                    }
                    else
                    {
                        db.closeConnection();
                        return false;
                    }
                }
            }
        }

        public sealed class Client 
        {
            public static bool Create(ClientModel client)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    MySqlCommand command = new MySqlCommand("INSERT INTO GalateClients (Name, " +
                                                        $"Surname, Phone) VALUES (N'{client.Name}', N'{client.Surname}'," +
                                                        $" N'{client.Phone}')", db.getConnection());

                    db.openConnection();

                    if (command.ExecuteNonQuery() == 1)
                    {
                        db.closeConnection();
                        return true;
                    }
                    else
                    {
                        db.closeConnection();
                        return false;
                    }
                }
            }

            public static IEnumerable<ClientModel> GetAll()
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    MySqlCommand command = new MySqlCommand("SELECT * FROM GalateClients", db.getConnection());

                    db.openConnection();

                    using (MySqlDataReader read = command.ExecuteReader())
                    {
                        while (true)
                        {
                            if (!read.Read()) break;

                            ClientModel outData = new ClientModel()
                            {
                                Id = (int)read["Id"],
                                Name = (string)read["Name"],
                                Surname = (string)read["Surname"],
                                Phone = (string)read["Phone"]
                            };
                            outData.FullName = $"{outData.Name} {outData.Surname}";

                            yield return outData;
                        }
                    }

                    db.closeConnection();
                }
            }

            public static ClientModel GetById(int id)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    MySqlCommand cmd = new MySqlCommand($"SELECT * FROM GalateClients WHERE Id = {id}", db.getConnection());

                    db.openConnection();

                    using (MySqlDataReader read = cmd.ExecuteReader())
                    {
                        if (!read.Read()) { }

                        ClientModel outData = new ClientModel()
                        {
                            Id = (int)read["Id"],
                            Name = (string)read["Name"],
                            Surname = (string)read["Surname"],
                            Phone = (string)read["Phone"]
                        };
                        outData.FullName = $"{outData.Name} {outData.Surname}";

                        db.closeConnection();

                        return outData;
                    }
                }
            }

            public static bool Delete(int id)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    MySqlCommand command = new MySqlCommand($"DELETE FROM GalateClients WHERE Id = {id}", db.getConnection());

                    db.openConnection();

                    if (command.ExecuteNonQuery() == 1)
                    {
                        db.closeConnection();
                        return true;
                    }
                    else
                    {
                        db.closeConnection();
                        return false;
                    }
                }
            }
        }

        public sealed class AirTravel
        {
            public static bool Create(AirTravelModel travel)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    MySqlCommand command = new MySqlCommand("INSERT INTO GalateAirTravels (TourId, " +
                                                        $"ClientId) VALUES (N'{travel.TourId}', N'{travel.ClientId}')", db.getConnection());

                    db.openConnection();

                    if (command.ExecuteNonQuery() == 1)
                    {
                        db.closeConnection();
                        return true;
                    }
                    else
                    {
                        db.closeConnection();
                        return false;
                    }
                }
            }

            public static IEnumerable<AirTravelModel> GetAll()
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    MySqlCommand command = new MySqlCommand("SELECT * FROM GalateAirTravels", db.getConnection());

                    db.openConnection();

                    using (MySqlDataReader read = command.ExecuteReader())
                    {
                        while (true)
                        {
                            if (!read.Read()) break;

                            AirTravelModel outData = new AirTravelModel()
                            {
                                Id = (int)read["Id"],
                                ClientId = (int)read["ClientId"],
                                TourId = (int)read["TourId"],
                            };

                            outData.Client = Client.GetById(outData.ClientId);
                            outData.Tour = Tour.GetById(outData.TourId);

                            yield return outData;
                        }
                    }

                    db.closeConnection();
                }
            }

            public static bool Delete(int id)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    MySqlCommand command = new MySqlCommand($"DELETE FROM GalateAirTravels WHERE Id = {id}", db.getConnection());

                    db.openConnection();

                    if (command.ExecuteNonQuery() == 1)
                    {
                        db.closeConnection();
                        return true;
                    }
                    else
                    {
                        db.closeConnection();
                        return false;
                    }
                }
            }

            public static bool DeleteByClientId(int id)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    MySqlCommand command = new MySqlCommand($"DELETE FROM GalateAirTravels WHERE ClientId = {id}", db.getConnection());

                    db.openConnection();

                    if (command.ExecuteNonQuery() == 1)
                    {
                        db.closeConnection();
                        return true;
                    }
                    else
                    {
                        db.closeConnection();
                        return false;
                    }
                }
            }

            public static bool DeleteByTourId(int id)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    MySqlCommand command = new MySqlCommand($"DELETE FROM GalateAirTravels WHERE TourId = {id}", db.getConnection());

                    db.openConnection();

                    if (command.ExecuteNonQuery() == 1)
                    {
                        db.closeConnection();
                        return true;
                    }
                    else
                    {
                        db.closeConnection();
                        return false;
                    }
                }
            }
        }
    }
}
