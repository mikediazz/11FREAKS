   M S B A M L   `   `   `  ���� C  ?11FREAKS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:_11FREAKS.Presentacion_11FREAKS.Presentacion  %    _11FREAKS.Presentacion.MiEquipo   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6local$clr-namespace:_11FREAKS.Presentacion       � Title$  MiEquipo��$	��700��$
��1200�� � Icon$! /Resources/Logo11Freaks.png�� � WindowState$ 	Maximized=� � 
ResizeMode$ NoResize=� � 	Resources ��%    .��� ����  �� ImageSource$! /Resources/campoFutbol.avif�� j� Stretch$ UniformToFill=�.+��� -    ��Name$ escudoEquipo��$��Right=�$	��200��$	��200��$��
665,50,0,0q�$	��Top=�o� $
��Auto��$��50��
���"�"�$	��Top=�.@�m� -    m�Name$ 
menuInicio��$��INICIO��$��20(�$��50��$��Heavy"�
�������.@�m� -   $ menuVolverPrincipal��$��VOLVER AL MENÚ PRINCIPAL��$��15(�$��25��$��Heavy"�
���   �	 � ToolTip	 _� $!��REGRESAR AL MENÚ PRINCIPAL��m� -   $ menuCerrarSesion��$��CERRAR SESIÓN��$��15(�$��25��$��Heavy"�
���   �	 _� $%��CERRAR SESIÓN Y SALIR AL LOGIN��m� -   $ 	menuSalir��$��SALIR��$��15(�$��25��$��Heavy"�
���   �	 _� $��CERRAR 11FREAKS��m� -   $ menuMiEquipo��$��	MI EQUIPO��$��20(�$��50��$��Heavy"�
�������	 _� $-��'CERRAR SESIÓN Y SALIR A PANTALLA INCIO��.@�m� -   $ menuPlantilla��$��	PLANTILLA��$��15(�$��25��$��Heavy"�
���   �	 _� $@��:CONSULTA TODOS LOS JUGADORES INSCRITOS EN EL PRIMER EQUIPO��m� -   $ menuEstadisticasEquipo��$��ESTADÍSTICAS EQUIPO��$��15(�$��25��$��Heavy"�
���   �	 _� $1��+CONSULTA ESTADÍSTICAS Y DATOS DE TU EQUIPO��m� -	   $ menuLesiones��$��BAJAS REPORTADAS��$��15(�$��25��$��Heavy"�
���   �	 _� $H��BESTOS SON LOS REPORTES DE LAS BAJAS DE ESTA TEMPORADA DE TU EQUIPO��m� -
   $ menuTraspasos��$��	TRASPASOS��$��15(�$��25��$��Heavy"�
���   �	 _� $:��4CONSULTA LOS MOVIMIENTOS EN LOS MERCADOS DE FICHAJES��m� -   $ menuAjustes��$��AJUSTES��$��20(�$��50��$��Heavy"�
�������	 _� $,��&ACCEDE A LA CONFIGURACIÓN DE 11FREAKS��m� -   $ 	menuAyuda��$��AYUDA��$��20(�$��50��$��Heavy"�
�������m� -   $ menuOpcAdmin��$��OPCIONES ADMIN��$��20(�$��50��$��Heavy"�
�������${�False��$y�Hidden=�.@�m� -   $ menuBorrarUsuario��$��Borrar Usuario��$��15(�$��25��$��Heavy"�
���   �m� -   $ menuAñadirAdmin��$��Añadir Admin��$��15(�$��25��$��Heavy"�
���   �m� -   $ menuCrearUsuario��$��Crear Usuario��$��15(�$��25��$��Heavy"�
���   �m� -   $ menuConsultarUsuarios��$��Consultar Usuarios��$��15(�$��25��$��Heavy"�
���   �	 _� $?��9HAGA CONSULTA GENÉRICO O INDIVIDUALIZADA DE LOS USUARIOS���� -   
 ��Name$
 	formacion��$��	FORMACION��$y�Hidden=�$��940,191,10,455q�$��60��$	��280��$��Center=�$��Black"�$��15(�
�������$��10q�
��� d �
������� � ContextMenu �� .@�m� -   $��OCULTAR��	 _� $7��1PUEDE OCULTAR ESTAS VISTAS PULSANDO CLICK DERECHO���� -   $
 goles��$��GOLES��$y�Hidden=�$��75,210,600,342q�$��Black"�$��15(�
�������$��10q�
��� d �
������� �� .@�m� -   $��OCULTAR��	 _� $7��1PUEDE OCULTAR ESTAS VISTAS PULSANDO CLICK DERECHO���� -   $
 racha��$��RACHA��$y�Hidden=�$	��Top=�$��Center=�$��Center=�$��70��$	��370��$��Black"�$��15(�
�������$��10q�
��� d �
�������$��0,55,0,0q� �� .@�m� -   $��OCULTAR��	 _� $7��1PUEDE OCULTAR ESTAS VISTAS PULSANDO CLICK DERECHO���� -    ��Name$ 	miListBox��$y�Hidden=�$��Center=�$��Center=�$	��800��$	��350�� �� .@�m� -   $��OCULTAR��	 _� $r��lPUEDE PULSAR SOBRE CADA ELEMENTO PARA FUNCIONALIDAD EXTRA, O PUEDE OCULTAR ESTA VISTA PULSANDO CLICK DERECHO���� -   $
 foto��$
��Left=�$	��200��$	��200��$��Center=�$��75,0,0,0q�