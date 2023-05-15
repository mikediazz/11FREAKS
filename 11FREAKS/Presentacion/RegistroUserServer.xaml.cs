﻿using _11FREAKS.Datos;
using _11FREAKS.Presentacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _11FREAKS.Presentacion
{
    /// <summary>
    /// Lógica de interacción para RegistroUserServer.xaml
    /// </summary>
    public partial class RegistroUserServer : Window
    {
        private Inicio inicio;
        public RegistroUserServer(Inicio pInicio)
        {
            InitializeComponent();
            this.inicio = pInicio;
            Datos.BDOnline miBaseDatos;
        }


        private void txtEquipo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }



        private void btnRegistrarse_Click(object sender, RoutedEventArgs e)
        {
            Datos.BDOnline miBaseDatos = new Datos.BDOnline();

            if (miBaseDatos.CompruebaPassword(txtUsuario.Text, txtPassword.Password))
            {
                if (miBaseDatos.ConectarServer(txtUsuario.Text, txtPassword.Password) == true)
                {
                    var mensajeTemporal = AutoClosingMessageBox.Show(
                    text: "Whoops! YA EXISTE ESTE USUARIO",
                    caption: "EQUIPO DE 11FREAKS",
                    timeout: 3000,
                    buttons: MessageBoxButtons.OK);
                }
                else
                {
                    if (CompruebaCampos() > 0)
                    {
                        var mensajeTemporal = AutoClosingMessageBox.Show(
                        text: "**DEBE RELLENAR TODOS LOS CAMPOS PARA COMPLETAR EL REGISTRO**",
                        caption: "EQUIPO DE 11FREAKS",
                        timeout: 1500,
                        buttons: MessageBoxButtons.OK);
                    }
                    else
                    {
                        miBaseDatos.CrearUsuario(txtUsuario.Text, txtPassword.Password, txtEquipo.Text.ToUpper(), txtEmail.Text);          //CREAMOS USUARIO

                        var mensajeTemporal = AutoClosingMessageBox.Show(
                        text: "**USUARIO CREADO**    ID EQUIPO",
                        caption: "EQUIPO DE 11FREAKS",
                        timeout: 1500,
                        buttons: MessageBoxButtons.OK);

                        Correo mailBienvenida = new Correo();                                                 //ENVIAMOS CORREO DE BIENVENIDA AL USUARIO
                                                                                                              // mailBienvenida.CorreoBienvenida(txtEmail.Text);

                        /*Thread volumenThread = new Thread(new ThreadStart(mailBienvenida.CorreoBienvenida(txtEmail.)));          //CREAMOS HILO AL QUE LE PASAMOS EL MÉTODO PARA INICIAR INTRO                                                                        
                        volumenThread.Start();*/

                        /*  Thread hilo = new Thread(new ParameterizedThreadStart(mailBienvenida.CorreoBienvenida);
                          hilo.Start(txtEmail.Text);*/
                        try
                        {
                            Thread thMailBienvenida = new Thread(() => mailBienvenida.CorreoBienvenida(txtEmail.Text));
                            thMailBienvenida.Start();
                        }
                        catch (Exception ex)
                        {
                            var mensajeCorreo = AutoClosingMessageBox.Show(
                            text: "NO SE PUDO ENVIAR CORREO " + ex.Message,
                            caption: "EQUIPO DE 11FREAKS",
                            timeout: 1500,
                            buttons: MessageBoxButtons.OK);
                        }


                        Principal principal = new Principal(inicio, miBaseDatos, txtUsuario.Text);
                        this.Hide();
                        principal.ShowDialog();
                    }

                }

            }
            else
            {                                                               //CONTRASEÑA NO VÁLIDA
                var mensajeTemporal = AutoClosingMessageBox.Show(
                text: "Whoops! LA CONTRASEÑA NO CUMPLE LOS REQUISITOS",
                caption: "EQUIPO DE 11FREAKS",
                timeout: 3000,
                buttons: MessageBoxButtons.OK);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }








        private int CompruebaCampos()
        {
            int comprobador = 0;          //VARIABLE COMPROBACIÓN REQUISITOS

            if (txtUsuario == null || txtUsuario.Text == string.Empty)
            {
                comprobador += 1;
            }
            if (txtPassword == null || txtPassword.Password == string.Empty)
            {
                comprobador += 1;
            }
            if (txtEmail == null || txtEmail.Text == string.Empty)
            {
                comprobador += 1;
            }
            if (txtEquipo == null || txtEquipo.Text == string.Empty)
            {
                comprobador += 1;
            }

            return comprobador;
        }











    }
}