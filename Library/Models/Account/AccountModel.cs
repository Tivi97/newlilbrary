using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
//using System.Web.Mvc;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.text.pdf.draw;
using Oracle.ManagedDataAccess.Client;



namespace Library.Models.Account
{
    public class AccountModel : Model
    {
        public string Auth(string login, string password)
        {
            using (Connection)
            {
                if (Session["login"] == null)
                {
                    Connection.Open();
                    var adapter = new OracleDataAdapter
                    {
                        SelectCommand = new OracleCommand
                        {
                            Connection = Connection,
                            CommandText = "select id_user, user_role from users where login = :log and password = :pass"
                        }
                    };
                    adapter.SelectCommand.Parameters.Add("log", OracleDbType.Varchar2).Value = login;
                    var md5 = MD5.Create();
                    adapter.SelectCommand.Parameters.Add("pass", OracleDbType.Varchar2).Value = GetMd5Hash(md5, password);
                    var reader = adapter.SelectCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                         while (reader.Read())
                        {
                            Session["id"] = reader["id_user"].ToString();
                            if (reader["user_role"].ToString() == "1")
                                Session["admin"] = true;
                            if (reader["user_role"].ToString() == "2")
                                Session["coach"] = true;

                        }
                        Session["login"] = true;
                        Connection.Close();
                        return "Здравствуйте, читатель";
                    }
                    else
                    {
                        Connection.Close();
                        return "Неправильный логин или пароль!";
                    }
                }
                else
                {
                    Connection.Close();
                    return "Вы уже авторизованы!";
                }
            }
        }

        public string Register(string fio, string birthday, string passpotr_ser, string passport_num, string certif_code, string e_mail, string userRole, string login, string password)
        {
            using (Connection)
            {
                if (Session["login"] == null)
                {
                    Connection.Open();

                    var adapter = new OracleDataAdapter
                    {
                        SelectCommand = new OracleCommand
                        {
                            Connection = Connection,
                            CommandText = "select id_user from users where login = :log"
                        }
                    };
                    adapter.SelectCommand.Parameters.Add("log", OracleDbType.Varchar2).Value = login;
                    var reader = adapter.SelectCommand.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        adapter = new OracleDataAdapter
                        {
                            InsertCommand = new OracleCommand
                            {
                                Connection = Connection,
                                CommandText = "insert into users (user_fio, data_rogd, passport_series, passport_code, certificate_code, email, user_role, login, password) values (:fio, :birthday, :passpotr_ser, :passport_num, :certif_code, :e_mail, :userRole, :login, :password)"
                            }
                        };
                        adapter.InsertCommand.Parameters.Add("user_fio", OracleDbType.Varchar2).Value = fio;
                        adapter.InsertCommand.Parameters.Add("data_rogd", OracleDbType.Date).Value = DateTime.Parse(birthday);
                        adapter.InsertCommand.Parameters.Add("passport_series", OracleDbType.Int32).Value = passpotr_ser;
                        adapter.InsertCommand.Parameters.Add("passport_code", OracleDbType.Int32).Value = passport_num;
                        adapter.InsertCommand.Parameters.Add("certificate_code", OracleDbType.Varchar2).Value = certif_code;
                        adapter.InsertCommand.Parameters.Add("email", OracleDbType.Varchar2).Value = e_mail;
                        adapter.InsertCommand.Parameters.Add("user_role", OracleDbType.Int32).Value = userRole;
                        adapter.InsertCommand.Parameters.Add("login", OracleDbType.Varchar2).Value = login;
                        var md5 = MD5.Create();
                        adapter.InsertCommand.Parameters.Add("password", OracleDbType.Varchar2).Value = GetMd5Hash(md5, password);
                        adapter.InsertCommand.ExecuteReader();

                        var idReader = "";
                        adapter = new OracleDataAdapter
                        {
                            SelectCommand = new OracleCommand
                            {
                                Connection = Connection,
                                CommandText = "select max(id_user) as id from users"
                            }
                        };
                        reader = adapter.SelectCommand.ExecuteReader();
                        while (reader.Read())
                        {
                            idReader = reader["id"].ToString();
                        }

                        Session["id"] = idReader;
                        Session["login"] = true;
                        Connection.Close();
                        return "Вы зарегистрированы! Удачи в пути!";
                    }
                    else
                    {
                        Connection.Close();
                        return "Этот логин занят!";
                    }
                }
                else
                {
                    Connection.Close();
                    return "Вы уже авторизованы!";
                }
            }
        }
          //---------------------------------------------------------------------------------------------------------------------
        //        public ProfileModel GetProfile(object reader)

        //{
        //    using (Connection)
        //    {
        //        Connection.Open();
        //        var profile = new ProfileModel
        //        {
        //           Reader = new ReaderModel()
        //        };
        //        var user = profile.Tourist;
        //        var adapter = new OracleDataAdapter
        //        {
        //            SelectCommand = new OracleCommand
        //            {
        //                Connection = Connection,
        //                CommandText = "select login, fio, phone, sex, birthday from users where id_user = :id"
        //            }
        //        };
        //        adapter.SelectCommand.Parameters.Add("id", int.Parse(reader.ToString()));
        //        var reader = adapter.SelectCommand.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            user.Birthday = reader["birthday"].ToString().Split(' ')[0];
        //            user.Fio = reader["fio"].ToString();
        //            user.Login = reader["login"].ToString();
        //            user.Phone = reader["phone"].ToString();
        //            user.Sex = reader["sex"].ToString();
        //        }
        //        Connection.Close();
        //        return profile;
        //    }
        //}
        //  ---------------------------------------------------------------------------------------------------------------------

        //      



        //        public byte[] GetPdf()
        //        {
        //            var stream = new MemoryStream();
        //            var doc = new Document(PageSize.A4, 50, 50, 50, 50);

        //            var fg = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIALUNI.TTF");
        //            var fgBaseFont = BaseFont.CreateFont(fg, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        //            var font = new Font(fgBaseFont, 14, Font.NORMAL, BaseColor.BLACK);

        //            PdfWriter.GetInstance(doc, stream);
        //            doc.Open();

        //            using (Connection)
        //            {
        //                Connection.Open();

        //                var fio = "";
        //                var adapter = new OracleDataAdapter
        //                {
        //                    SelectCommand = new OracleCommand
        //                    {
        //                        Connection = Connection,
        //                        CommandText = "select fio from users where id_user = :id_user"
        //                    }
        //                };
        //                adapter.SelectCommand.Parameters.Add("id_user", OracleDbType.Int32).Value = Session["id"];
        //                var reader = adapter.SelectCommand.ExecuteReader();
        //                while (reader.Read())
        //                {
        //                    fio = reader["fio"].ToString();
        //                }

        //                var p = new Paragraph("ФИО туриста: " + fio, font);
        //                var glue = new Chunk(new VerticalPositionMark());
        //                p.Add(new Chunk(glue));
        //                p.Add("Дата распечатки: " + DateTime.Today.ToString("dd.MM.yy"));
        //                doc.Add(p);
        //                p = new Paragraph("Туристский опыт") { Alignment = Element.ALIGN_CENTER, Font = font };
        //                doc.Add(p);

        //                adapter = new OracleDataAdapter
        //                {
        //                    SelectCommand = new OracleCommand
        //                    {
        //                        Connection = Connection,
        //                        CommandText = "select distinct extract(year from date_start) as year from hikes order by year desc"
        //                    }
        //                };
        //                reader = adapter.SelectCommand.ExecuteReader();

        //                while (reader.Read())
        //                {
        //                    var year = reader["year"].ToString();
        //                    p = new Paragraph(year, font);
        //                    doc.Add(p);
        //                    var table = new PdfPTable(3);
        //                    table.AddCell(GetCell("Название похода"));
        //                    table.AddCell(GetCell("Категория сложности"));
        //                    table.AddCell(GetCell("Сроки проведения"));

        //                    var adapter2 = new OracleDataAdapter
        //                    {
        //                        SelectCommand = new OracleCommand
        //                        {
        //                            Connection = Connection,
        //                            CommandText = "select hike_name, category_name, date_start, date_finish from categories c, hikes h, experience ex "
        //                            + "where h.id_category = c.id_category and ex.id_hike = h.id_hike and done = 1 and id_user = :id_user "
        //                            + "and extract(year from date_start) = :year"
        //                        }
        //                    };
        //                    adapter2.SelectCommand.Parameters.Add("id_user", OracleDbType.Int32).Value = int.Parse(Session["id"].ToString());
        //                    adapter2.SelectCommand.Parameters.Add("year", OracleDbType.Int32).Value = int.Parse(year);
        //                    var hikes = adapter2.SelectCommand.ExecuteReader();

        //                    while (hikes.Read())
        //                    {
        //                        table.AddCell(GetCell(hikes["hike_name"].ToString()));
        //                        table.AddCell(GetCell(hikes["category_name"].ToString()));
        //                        table.AddCell(GetCell(hikes["date_start"].ToString().Split(' ')[0] + " - " +
        //                                              hikes["date_finish"].ToString().Split(' ')[0]));
        //                    }

        //                    doc.Add(table);
        //                }

        //                Connection.Close();
        //            }

        //            doc.Close();
        //            return stream.ToArray();
        //        }

        //        private static PdfPCell GetCell(string text)
        //        {
        //            var fg = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIALUNI.TTF");
        //            var fgBaseFont = BaseFont.CreateFont(fg, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        //            var font = new Font(fgBaseFont, 14, Font.NORMAL, BaseColor.BLACK);
        //            var cell = new PdfPCell
        //            {
        //                Padding = 5,
        //                Phrase = new Phrase(text, font),
        //                HorizontalAlignment = 1,
        //            };
        //            return cell;
        //        }

        //        public string AddExperience(ExperienceModel exp)
        //        {
        //            using (Connection)
        //            {
        //                Connection.Open();
        //                var adapter = new OracleDataAdapter
        //                {
        //                    InsertCommand = new OracleCommand
        //                    {
        //                        Connection = Connection,
        //                        CommandText = "insert into experience (id_user, id_hike, is_lead, accept, done) "
        //                        + "values (:id_user, :id_hike, :is_lead, :accept, :done)"
        //                    }
        //                };
        //                adapter.InsertCommand.Parameters.Add("id_user", OracleDbType.Int32).Value = exp.IdUser;
        //                adapter.InsertCommand.Parameters.Add("id_hike", OracleDbType.Int32).Value = exp.IdHike;
        //                adapter.InsertCommand.Parameters.Add("is_lead", OracleDbType.Char).Value = '0';
        //                adapter.InsertCommand.Parameters.Add("accept", OracleDbType.Char).Value = '1';
        //                adapter.InsertCommand.Parameters.Add("done", OracleDbType.Char).Value = "1";
        //                try
        //                {
        //                    adapter.InsertCommand.ExecuteNonQuery();
        //                    return "Пройденный поход добавлен в базу данных!";
        //                }
        //                catch (Exception e)
        //                {
        //                    var ex = e.Message.Substring(11);
        //                    return ex.Remove(ex.IndexOf("ORA", StringComparison.CurrentCulture));
        //                }
        //                finally
        //                {
        //                    Connection.Close();
        //                }
        //            }
        //        }
        //    }
        //}
    }
}