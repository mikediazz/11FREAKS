using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace _11FREAKS.Datos
{
    internal class Correo
    {


        public Correo() { } //CONSTRUCTOR VACÍO


        public void CorreoBienvenida(string email)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                using (MailMessage correo = new MailMessage())
                {
                    correo.To.Add(email);      //Correo Destino

                    correo.Subject = "11FREAKS";                    //Asunto

                    correo.Body = "<html>\r\n  <head>\r\n    <title>11FREAKS</title>\r\n    <style>\r\n   h1 {text-align: center;}    h2 {text-align: center;}      h4 {  max-width: 500px;   margin: 0 auto;   text-align: center;  text-align: justify;  }         img { display: block;   margin: 0 auto;   width: 300px;     height: 300px;    }     </style>  </head> <body>   <h1>BIENVENIDO A 11FREAKS</h1>\r\n  <h2>¡Ya puedes empezar la temporada!</h2>    <h4>¡Bienvenido/a a 11FREAKS! Estamos encantados de tenerte a bordo y esperamos que disfrutes de la experiencia en nuestro emocionante juego de fútbol.\r\n\r\nNos complace informarte que tu cuenta ha sido creada con éxito. A partir de ahora, podrás acceder a todas las funciones y servicios de 11FREAKS y comenzar a explorar todo lo que puede ofrecer. Prepárate para disfrutar de partidos emocionantes y vivir la pasión del fútbol como nunca antes lo habías hecho.\r\n\r\nEn 11FREAKS, podrás crear y personalizar tu propio equipo, participar en ligas y partidos amistosos, y competir con jugadores de todo el mundo. Además, nuestro equipo de desarrollo trabaja constantemente para añadir nuevas funciones y mejoras al juego, para que siempre tengas nuevas emociones y retos que superar.\r\n\r\nSi tienes alguna pregunta o problema, no dudes en ponerte en contacto con nuestro equipo de soporte al cliente. Estamos aquí para ayudarte en cualquier momento.\r\n\r\n¡Gracias por elegir 11FREAKS y bienvenido/a de nuevo a la familia!\r\n\r\n   </h4> <h4><br/>Atentamente,\r\nEl Equipo de 11FREAKS <br/><br/><br/> </h4>     <img src=\"https://i.imgur.com/n6RlabH.jpg\" alt=\"Imagen de ejemplo\">        </body>   </html>"; //Mensaje del correo
                    correo.IsBodyHtml = true;           //CUERPO CORREO EN FORMATO HTML

                    correo.From = new MailAddress("11freaks.retrodevs@gmail.com", "11FREAKS", System.Text.Encoding.UTF8);   //Correo Salida

                    using (SmtpClient cliente = new SmtpClient())
                    {
                        cliente.UseDefaultCredentials = false;
                        cliente.Credentials = new System.Net.NetworkCredential("11freaks.retrodevs@gmail.com", "axboqzfzjqdvfnpy"); //Cuenta de Correo
                        cliente.Port = 587;
                        cliente.EnableSsl = true;

                        cliente.Host = "smtp.gmail.com";
                        cliente.Send(correo);
                    }
                }

            }));


        }







        public void CorreoContraseña(string email)
        {
            using (MailMessage correo = new MailMessage())
            {
                correo.To.Add(email);      //Correo Destino

                correo.Subject = "CONTRASEÑA 11FREAKS";                    //Asunto

                correo.Body = "<html>\r\n  <head>\r\n    <title>11FREAKS</title>\r\n    <style>\r\n   h1 {text-align: center;}    h2 {text-align: center;}      h4 {  max-width: 500px;   margin: 0 auto;   text-align: center;  text-align: justify;  }         img { display: block;   margin: 0 auto;   width: 300px;     height: 300px;    }     </style>  </head> <body>   <h1>CONTRASEÑA 11FREAKS</h1>\r\n  <h2>SU ANTERIOR CONTRASEÑA HA SIDO RESTABLECIDA CON ÉXITO</h2>    <h4>Si tienes alguna pregunta, problema o crees han accedido a tu cuenta, no dudes en ponerte en contacto con nuestro equipo de soporte al cliente. Estamos aquí para ayudarte en cualquier momento.\r\n\r\n¡Gracias por elegir 11FREAKS!\r\n\r\n   </h4> <h4><br/>Atentamente,\r\nEl Equipo de 11FREAKS <br/><br/><br/> </h4>     <img src=\"https://i.imgur.com/n6RlabH.jpg\" alt=\"Imagen de ejemplo\">        </body>   </html>"; //Mensaje del correo
                correo.IsBodyHtml = true;           //CUERPO CORREO EN FORMATO HTML

                correo.From = new MailAddress("11freaks.retrodevs@gmail.com", "11FREAKS", System.Text.Encoding.UTF8);   //Correo Salida

                using (SmtpClient cliente = new SmtpClient())
                {
                    cliente.UseDefaultCredentials = false;
                    cliente.Credentials = new System.Net.NetworkCredential("11freaks.retrodevs@gmail.com", "axboqzfzjqdvfnpy"); //Cuenta de Correo
                    cliente.Port = 587;
                    cliente.EnableSsl = true;

                    cliente.Host = "smtp.gmail.com";
                    cliente.Send(correo);
                }

            }
        }






        public void CorreoCambioEmail(string email)
        {
            using (MailMessage correo = new MailMessage())
            {
                correo.To.Add(email);      //Correo Destino

                correo.Subject = "EMAIL CAMBIADO";                    //Asunto

                correo.Body = "<html>\r\n  <head>\r\n    <title>11FREAKS</title>\r\n    <style>\r\n   h1 {text-align: center;}    h2 {text-align: center;}      h4 {  max-width: 500px;   margin: 0 auto;   text-align: center;  text-align: justify;  }         img { display: block;   margin: 0 auto;   width: 300px;     height: 300px;    }     </style>  </head> <body>   <h1>EMAIL 11FREAKS</h1>\r\n  <h2>SU CORREO HA SIDO RESTABLECIDA CON ÉXITO</h2>    <h4>Si tienes alguna pregunta, problema o crees han accedido a tu cuenta, no dudes en ponerte en contacto con nuestro equipo de soporte al cliente. Estamos aquí para ayudarte en cualquier momento.\r\n\r\n¡Gracias por elegir 11FREAKS!\r\n\r\n   </h4> <h4><br/>Atentamente,\r\nEl Equipo de 11FREAKS <br/><br/><br/> </h4>     <img src=\"https://i.imgur.com/n6RlabH.jpg\" alt=\"Imagen de ejemplo\">        </body>   </html>"; //Mensaje del correo
                correo.IsBodyHtml = true;           //CUERPO CORREO EN FORMATO HTML

                correo.From = new MailAddress("11freaks.retrodevs@gmail.com", "11FREAKS", System.Text.Encoding.UTF8);   //Correo Salida

                using (SmtpClient cliente = new SmtpClient())
                {
                    cliente.UseDefaultCredentials = false;
                    cliente.Credentials = new System.Net.NetworkCredential("11freaks.retrodevs@gmail.com", "axboqzfzjqdvfnpy"); //Cuenta de Correo
                    cliente.Port = 587;
                    cliente.EnableSsl = true;

                    cliente.Host = "smtp.gmail.com";
                    cliente.Send(correo);
                }

            }
        }







        public void CorreoCuentaEliminada(string email)
        {
            using (MailMessage correo = new MailMessage())
            {
                correo.To.Add(email);      //Correo Destino

                correo.Subject = "CUENTA 11FREAKS ELIMINADA";                    //Asunto

                correo.Body = "<html>\r\n  <head>\r\n    <title>11FREAKS</title>\r\n    <style>\r\n   h1 {text-align: center;}    h2 {text-align: center;}      h4 {  max-width: 500px;   margin: 0 auto;   text-align: center;  text-align: justify;  }         img { display: block;   margin: 0 auto;   width: 300px;     height: 300px;    }     </style>  </head> <body>   <h1>CUENTA 11FREAKS ELIMINADA</h1>\r\n  <h2>SU CUENTA DE 11FREAKS HA SIDO ELIMINADA CON ÉXITO</h2>    <h4>Esperamos volver a verte pronto. Si tienes alguna pregunta, alguna sugerencia, problema o crees han accedido a tu cuenta, no dudes en ponerte en contacto con nuestro equipo de soporte al cliente. Estamos aquí para ayudarte en cualquier momento.\r\n\r\n¡Gracias por elegir 11FREAKS!\r\n\r\n   </h4> <h4><br/>Atentamente,\r\nEl Equipo de 11FREAKS <br/><br/><br/> </h4>     <img src=\"https://i.imgur.com/n6RlabH.jpg\" alt=\"Imagen de ejemplo\">        </body>   </html>"; //Mensaje del correo
                correo.IsBodyHtml = true;           //CUERPO CORREO EN FORMATO HTML

                correo.From = new MailAddress("11freaks.retrodevs@gmail.com", "11FREAKS", System.Text.Encoding.UTF8);   //Correo Salida

                using (SmtpClient cliente = new SmtpClient())
                {
                    cliente.UseDefaultCredentials = false;
                    cliente.Credentials = new System.Net.NetworkCredential("11freaks.retrodevs@gmail.com", "axboqzfzjqdvfnpy"); //Cuenta de Correo
                    cliente.Port = 587;
                    cliente.EnableSsl = true;

                    cliente.Host = "smtp.gmail.com";
                    cliente.Send(correo);
                }

            }
        }







        public void CorreoBaneo(string email)
        {
            using (MailMessage correo = new MailMessage())
            {
                correo.To.Add(email);      //Correo Destino

                correo.Subject = "CUENTA 11FREAKS BANEADA";                    //Asunto

                correo.Body = "<html>\r\n  <head>\r\n    <title>11FREAKS</title>\r\n    <style>\r\n   h1 {text-align: center;}    h2 {text-align: center;}      h4 {  max-width: 500px;   margin: 0 auto;   text-align: center;  text-align: justify;  }         img { display: block;   margin: 0 auto;   width: 300px;     height: 300px;    }     </style>  </head> <body>   <h1>CUENTA 11FREAKS ELIMINADA</h1>\r\n  <h2>SU CUENTA DE 11FREAKS HA SIDO ELIMINADA POR UNO DE LOS ADMINISTRADORES</h2>    <h4>Uno de los administradores de 11FREAKS ha procedido a eliminar su cuenta de forma permanente. Si tienes alguna pregunta, alguna reclamación, problema o crees han accedido a tu cuenta, no dudes en ponerte en contacto con nuestro equipo de soporte al cliente. Estamos aquí para ayudarte en cualquier momento.\r\n\r  </h4> <h4><br/>Atentamente,\r\nEl Equipo de 11FREAKS <br/><br/><br/> </h4>     <img src=\"https://i.imgur.com/n6RlabH.jpg\" alt=\"Imagen de ejemplo\">        </body>   </html>"; //Mensaje del correo
                correo.IsBodyHtml = true;           //CUERPO CORREO EN FORMATO HTML

                correo.From = new MailAddress("11freaks.retrodevs@gmail.com", "11FREAKS", System.Text.Encoding.UTF8);   //Correo Salida

                using (SmtpClient cliente = new SmtpClient())
                {
                    cliente.UseDefaultCredentials = false;
                    cliente.Credentials = new System.Net.NetworkCredential("11freaks.retrodevs@gmail.com", "axboqzfzjqdvfnpy"); //Cuenta de Correo
                    cliente.Port = 587;
                    cliente.EnableSsl = true;

                    cliente.Host = "smtp.gmail.com";
                    cliente.Send(correo);
                }

            }
        }





    }
    
}
