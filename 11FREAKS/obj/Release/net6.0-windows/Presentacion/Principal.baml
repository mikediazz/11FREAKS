   M S B A M L   `   `   `  ���� C  ?11FREAKS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:_11FREAKS.Presentacion_11FREAKS.Presentacion  ?$clr-namespace:_11FREAKS.Presentacion_11FREAKS.Presentacion  &     _11FREAKS.Presentacion.Principal   +uc$clr-namespace:_11FREAKS.Presentacion  W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6local$clr-namespace:_11FREAKS.Presentacion     -     � Title$  	Principal��$	��700��$
��1200�� � WindowState$ 	Maximized=� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � Icon$! /Resources/Logo11Freaks.png�� � 	Resources    Ctr1 �� Text  F1�� %C   &	        &	      �� $ Texto Ayuda���� $ Another Text�� J� InputBindings �� 
 �� Key$ F1��	 �� 	Modifiers$
	 Ctrlf�
 �� Command#
 [  �� $ F2�� J� CommandBindings )  #System.Windows.Input.CommandBinding  -      Command# [    -   # [ .��� ����  �� ImageSource$ /Resources/fondoMain.jpg�� j� Stretch$ UniformToFill=�.+��� -    ��Name$
 logo��$!��/Resources/Logo11Freaks.png�� J� Opacity$
 0.85Q�$��606,50,-6,184q�$	��200��$	��200��$	��Top=�$��Right=� � ToolTip _� $<��6Desarrollado Por Miguel Díaz Osa sin fines lucrativos��o� $
��Auto��$��50��
���"�"�$	��Top=�.@�m� -    m�Name$ 
menuInicio��$��INICIO��$��20(�$��50��$��Heavy"�
�������.@�m� -   $ menuCerrarSesion��$��CERRAR SESIÓN��$��15(�$��25��$��Heavy"�
���   � _� $-��'CERRAR SESIÓN Y SALIR A PANTALLA INCIO��m� -   $ 	menuSalir��$��SALIR��$��15(�$��25��$��Heavy"�
���   � _� $��SALIR DE 11FREAKS��m� -   $ 
menuMiLiga��$��MI LIGA��$��20(�$��50��$��Heavy"�
�������.@�m� -	   $ menuMiClasificacion��$��CLASIFICACIÓN��$��15(�$��25��$��Heavy"�
���   � _� $��VER CLASIFICACIÓN��m� -
   $ menuVerEquipos��$��VER EQUIPOS��$��15(�$��25��$��Heavy"�
���   � _� $%��CONSULTA LOS EQUIPOS DE LA LIGA��m� -   $ menuMiEquipo��$��	MI EQUIPO��$��20(�$��50��$��Heavy"�
�������.@�m� -   $ menuCambiarMiEquipo��$��CAMBIAR MI EQUIPO��$��15(�$��25��$��Heavy"�
���   � _� $��CAMBIA TU EQUIPO FAVORITO��m� -   $ menuVerEquipo��$��IR A MI EQUIPO��$��15(�$��25��$��Heavy"�
���   � _� $:��4CONSULTA ESTADÍSTICAS Y DATOS DE TU EQUIPO FAVORITO��m� -   $ menuOjeadores��$��	OJEADORES��$��20(�$��50��$��Heavy"�
�������.@�m� -   $ menuVerClasificacion��$��VER CLASIFICACIÓN��$��15(�$��25��$��Heavy"�
���   � _� $>��8ECHA UN VISTAZO A COMO LALIGA ACTUALMENTE EN LA REALIDAD��m� -   $ menuMaximosGoleadores��$��TOP GOLEADORES��$��15(�$��25��$��Heavy"�
���   � _� $7��1ESTOS SON LOS GOLEADORES DE LALIGA EN LA REALIDAD��m� -   $ menuMaximosAsistentes��$��TOP ASISTENTES��$��15(�$��25��$��Heavy"�
���   � _� $1��+ESTOS SON LOS MAYORES ASISTENTES DE LA LIGA��m� -   $ menuMaximosRojas��$��TOP EXPULSADOS��$��15(�$��25��$��Heavy"�
���   � _� $8��2ESTOS SON LOS JUGADOERS MÁS EXPULSADOS DE LA LIGA��m� -   $ menuMaximosAmarillas��$��TOP AMARILLAS��$��15(�$��25��$��Heavy"�
���   � _� $I��CESTOS SON LOS JUGADOERS CON MÁS AMARILLAS DE LALIGA EN LA REALIDAD�� _� $]��WECHA UN VISTAZO AL BOLETIN DE OJEADORES PARA VER COMO LALIGA ACTUALMENTE EN LA REALIDAD��m� -   $ menuMiCuenta��$��	MI CUENTA��$��20(�$��50��$��Heavy"�
�������.@�m� -   $ menuCambiarContraseña��$��CAMBIAR CONTRASEÑA��$��15(�$��25��$��Heavy"�
���   � _� $&�� CAMBIAR CONTRASEÑA DE LA CUENTA��m� -   $ menuCambiarCorreo��$��RESTABLECER CORREO��$��15(�$��25��$��Heavy"�
���   � _� $,��&CAMBIAR DIRECCIÓN CORREO DE SU CUENTA��m� -   $ menuBorrarCuenta��$��BORRAR CUENTA��$��15(�$��25��$��Heavy"�
���   � _� $'��!BORRAR CUENTA DE FORMA DEFINITIVA��m� -   $ menuAjustes��$��AJUSTES��$��20(�$��50��$��Heavy"�
������� _� $,��&ACCEDE A LA CONFIGURACIÓN DE 11FREAKS��.@�m� -   $ menuOcultarLogo��$��OCULTAR LOGO 11FREAKS��$��15(�$��25��$��Heavy"�
���   � _� $ ��OCULTE EL LOGO DE 11FREAKS��m� -   $ menuAjustarVolumen��$��AJUSTAR VOLUMEN��$��15(�$��25��$��Heavy"�
���   � _� $ ��OCULTE EL LOGO DE 11FREAKS��m� -   $ 	menuAyuda��$��AYUDA��$��20(�$��50��$��Heavy"�
������� _� $4��.AQUÍ PODRÁS ENCONTRAR LA AYUDA QUE NECESITAS��m� -   $ menuOpcAdmin��$��OPCIONES ADMIN��$��20(�$��50��$��Heavy"�
�������${�False��$y�Hidden=�.@�m� -   $ menuAñadirAdmin��$��AÑADIR ADMIN��$��15(�$��25��$��Heavy"�
���   � _� $4��.OTORGUE PERMISOS DE ADMINISTRADOR A UN USUARIO��m� -   $ menuCrearUsuario��$��CREAR USUARIO��$��15(�$��25��$��Heavy"�
���   � _� $��CREE UN USUARIO��m� -   $ menuConsultarUsuarios��$��CONSULTAR USUARIOS��$��15(�$��25��$��Heavy"�
���   � _� $?��9HAGA CONSULTA GENÉRICA O INDIVIDUALIZADA DE LOS USUARIOS��9� -     9�Name$ expanderUsers�� 9� ExpandDirection$
 Left=�$��Right=�$��Bottom=�$��0��
�������$y�Hidden=����� $��Búsqueda Usuarios��$��Center=�$��12(�$
��Bold"� � LayoutTransform V� .~��� �� ��  �� Angle$ 90Q�T� .��-   '_11FREAKS.Presentacion.BusquedaUsuarios  -!     Name$ control���� -"    ��Name$ indicadorLbox��$�� ��$y�Visible=�$��300,132,700,514q�$��38��$	��500��$��Black"�$��20(�
��������� -#    ��Name$ 	miListBox��$y�Hidden=�$��Center=�$��Center=�$	��800��$	��350�� � ContextMenu �� .@�m� -$   $��OCULTAR�� _� $a��[PUEDE OCULTAR ESTA VISTA PULSANDO CLICK DERECHO, O BUSCAR INFORMACIÓN DESDE CLASIFICACIÓN���� -%   $ lblJugadores��$��	JUGADORES��$
��Left=�$��
128,78,0,0q�$	��Top=�$	��483��$	��815��
�������