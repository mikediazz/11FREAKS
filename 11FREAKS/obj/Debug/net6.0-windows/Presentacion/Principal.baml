   M S B A M L   `   `   `  ����C  ?11FREAKS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:_11FREAKS.Presentacion_11FREAKS.Presentacion  5   	   U QSystem.Runtime, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a7,clr-namespace:System;assembly=System.RuntimeSystem 5   	   R NSystem.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089>.clr-namespace:System.Data;assembly=System.DataSystem.Data 5   	   ?$clr-namespace:_11FREAKS.Presentacion_11FREAKS.Presentacion  5	   	   &     _11FREAKS.Presentacion.Principal   5   	   +uc$clr-namespace:_11FREAKS.Presentacion  5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ?System,clr-namespace:System;assembly=System.Runtime     5   	   E
SystemData.clr-namespace:System.Data;assembly=System.Data     5   	   6local$clr-namespace:_11FREAKS.Presentacion     5	   	   -   5          � Title$  	Principal��5   	   $	��700��6   $
��1200��6(    � WindowState$ 	Maximized=�6L    � 
ResizeMode$ NoResize=�6{    � WindowStartupLocation$ CenterScreen=�6�    � Icon$! /Resources/Logo11Freaks.png��6�    � 	Resources 5         Ctr1 �� Text  F1�� 5   
   %_   &	        &	 %     �� 5      $ Texto Ayuda��6+   �� 5      $ Another Text��6)   5       J� InputBindings 5      �� 5   
   
 �� Key$ F1��6   	 �� 	Modifiers$
	 Ctrlf�6   
 �� Command#
 [  6/   �� 5   
   $ F2��6    J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command# [  5      69     5   
   -   5        # [ 5      67   .��5       � ��5"   
   �� 5#       �� ImageSource$ /Resources/fondoMain.jpg��6    j� Stretch$ UniformToFill=�6@   .+�5&   
   �� -   5         ��Name$
 logo��5&      $!��/Resources/Logo11Freaks.png��6    J� Opacity$
 0.85Q�6C   $��606,50,-6,184q�6R   $	��200��6i   $	��200��6u   $	��Top=�6�   $��Right=�6�    � ToolTip 5'      _� 5(      $<��6Desarrollado Por Miguel Díaz Osa sin fines lucrativos��6   5*      &   System.Windows.Controls.DataGrid  5-   
   -   5          Name$ tablaOjeadores��5-      $y�	Collapsed=�6+   $	��450��6B   $	��600��6O      AutoGenerateColumns$ False��6[      Columns 5.      0  *System.Windows.Controls.DataGridTextColumn  5/      ,  &System.Windows.Controls.DataGridColumn   Header$ Nombre��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *Nombre+  50      $ Equipo��6%    65   �� *Equipo+  51      $ 	Amarillas��6%    68   �� *Estadistica+  52      $ Partidos Jugados��6%    6?   �� *PartidosJugados+54      o� 59   
   $
��Auto��6   $��50��6   
���"�"�$	��Top=�6A   .@�5:      m� -   5         m�Name$ 
menuInicio��5:      $��INICIO��6+   $��20(�6;   $��50��6I   $��Heavy"�6U   
�������.@�5;      m� -   5        $ menuCerrarSesion��5;      $��CERRAR SESIÓN��65   $��15(�6L   $��25��6Z   $��Heavy"�6f   
���   � 5<      _� 5=      $-��'CERRAR SESIÓN Y SALIR A PANTALLA INCIO��6"   5?      m� 5@      -   5        $ 	menuSalir��5@      $��SALIR��6.   $��15(�6=   $��25��6K   $��Heavy"�6W   
���   � 5A      _� 5B      $��SALIR DE 11FREAKS��6"   5D      5E      m� 5G      -	   5        $ 
menuMiLiga��5G      $��MI LIGA��6+   $��20(�6<   $��50��6J   $��Heavy"�6V   
�������.@�5H      m� -
   5        $ menuMiClasificacion��5H      $��CLASIFICACIÓN��68   $��15(�6O   $��25��6]   $��Heavy"�6i   
���   � 5I      _� 5J      $��VER CLASIFICACIÓN��6"   5L      m� 5M      -   5        $ menuVerEquipos��5M      $��VER EQUIPOS��63   $��15(�6H   $��25��6V   $��Heavy"�6b   
���   � 5N      _� 5O      $%��CONSULTA LOS EQUIPOS DE LA LIGA��6"   5Q      m� 5R      -   5        $ menuJugarPartido��5R      $��JUGAR PARTIDO��65   $��15(�6L   $��25��6Z   $��Heavy"�6f   
���   � 5S      _� 5T      $��DISPUTA PARTIDO��6"   5V      m� 5W      -   5        $ menuMercadoTraspasos��5W      $��MERCADO TRASPASOS��69   $��15(�6T   $��25��6b   $��Heavy"�6n   
���   � 5X      _� 5Y      $��IR A MERCADO TRASPASOS��6"   5[      5\      m� 5_      -   5        $ menuMiEquipo��5_      $��	MI EQUIPO��6-   $��20(�6@   $��50��6N   $��Heavy"�6Z   
������� 5`      _� 5a      $��IR A MI EQUIPO��6   5c      m� 5f      -   5        $ menuOjeadores��5f      $��	OJEADORES��6.   $��20(�6A   $��50��6O   $��Heavy"�6[   
�������.@�5g      m� -   5        $ menuVerClasificacion��5g      $��VER CLASIFICACIÓN��69   $��15(�6T   $��25��6b   $��Heavy"�6n   
���   � 5h      _� 5i      $>��8ECHA UN VISTAZO A COMO LALIGA ACTUALMENTE EN LA REALIDAD��6"   5k      m� 5l      -   5        $ menuMaximosGoleadores��5l      $��TOP GOLEADORES��6:   $��15(�6R   $��25��6`   $��Heavy"�6l   
���   � 5m      _� 5n      $7��1ESTOS SON LOS GOLEADORES DE LALIGA EN LA REALIDAD��6"   5p      m� 5q      -   5        $ menuMaximosAsistentes��5q      $��TOP ASISTENTES��6:   $��15(�6R   $��25��6`   $��Heavy"�6l   
���   � 5r      _� 5s      $1��+ESTOS SON LOS MAYORES ASISTENTES DE LA LIGA��6"   5u      m� 5v      -   5        $ menuMaximosRojas��5v      $��TOP EXPULSADOS��65   $��15(�6M   $��25��6[   $��Heavy"�6g   
���   � 5w      _� 5x      $8��2ESTOS SON LOS JUGADOERS MÁS EXPULSADOS DE LA LIGA��6"   5z      m� 5{      -   5        $ menuMaximosAmarillas��5{      $��TOP AMARILLAS��69   $��15(�6P   $��25��6^   $��Heavy"�6j   
���   � 5|      _� 5}      $I��CESTOS SON LOS JUGADOERS CON MÁS AMARILLAS DE LALIGA EN LA REALIDAD��6"   5       5�      _� 5�      $]��WECHA UN VISTAZO AL BOLETIN DE OJEADORES PARA VER COMO LALIGA ACTUALMENTE EN LA REALIDAD��6   5�      m� 5�      -   5        $ menuMiCuenta��5�      $��	MI CUENTA��6-   $��20(�6@   $��50��6N   $��Heavy"�6Z   
�������.@�5�      m� -   5        $ menuCambiarContraseña��5�      $��CAMBIAR CONTRASEÑA��6:   $��15(�6V   $��25��6d   $��Heavy"�6p   
���   � 5�      _� 5�      $&�� CAMBIAR CONTRASEÑA DE LA CUENTA��6"   5�      m� 5�      -   5        $ menuCambiarCorreo��5�      $��RESTABLECER CORREO��66   $��15(�6R   $��25��6`   $��Heavy"�6l   
���   � 5�      _� 5�      $,��&CAMBIAR DIRECCIÓN CORREO DE SU CUENTA��6"   5�      m� 5�      -   5        $ menuBorrarCuenta��5�      $��BORRAR CUENTA��65   $��15(�6L   $��25��6Z   $��Heavy"�6f   
���   � 5�      _� 5�      $'��!BORRAR CUENTA DE FORMA DEFINITIVA��6"   5�      5�      m� 5�      -   5        $ menuAjustes��5�      $��AJUSTES��6,   $��20(�6=   $��50��6K   $��Heavy"�6W   
������� 5�      _� 5�      $,��&ACCEDE A LA CONFIGURACIÓN DE 11FREAKS��6   .@�5�      m� -   5        $ menuOcultarLogo��5�      $��OCULTAR LOGO 11FREAKS��64   $��15(�6S   $��25��6a   $��Heavy"�6m   
���   � 5�      _� 5�      $ ��OCULTE EL LOGO DE 11FREAKS��6"   5�      m� 5�      -   5        $ menuAjustarVolumen��5�      $��AJUSTAR VOLUMEN��67   $��15(�6P   $��25��6^   $��Heavy"�6j   
���   � 5�      _� 5�      $ ��OCULTE EL LOGO DE 11FREAKS��6"   5�      5�      m� 5�      -   5        $ 	menuAyuda��5�      $��AYUDA��6*   $��20(�69   $��50��6G   $��Heavy"�6S   
������� 5�      _� 5�      $4��.AQUÍ PODRÁS ENCONTRAR LA AYUDA QUE NECESITAS��6   .@�5�      m� -   5        $ menuDocumentacion��5�      $��DOCUMENTACIÓN��66   $��15(�6M   $��25��6[   $��Heavy"�6g   
���   � 5�      _� 5�      $)��#DOCUMENTACIÓN TÉCNICA DE 11FREAKS��6"   5�      m� 5�      -   5        $ menuAyudaUsuario��5�      $��AYUDA USUARIO��65   $��15(�6L   $��25��6Z   $��Heavy"�6f   
���   � 5�      _� 5�      $��GUÍA USUARIO DE 11FREAKS��6"   5�      5�      m� 5�      -   5        $ menuOpcAdmin��5�      $��OPCIONES ADMIN��6.   $��20(�6G   $��50��6U   $��Heavy"�6a   
�������${�False��6�   $y�Hidden=�6�   .@�5�      m� -    5        $ menuAñadirAdmin��5�      $��AÑADIR ADMIN��64   $��15(�6J   $��25��6X   $��Heavy"�6d   
���   � 5�      _� 5�      $4��.OTORGUE PERMISOS DE ADMINISTRADOR A UN USUARIO��6"   5�      m� 5�      -!   5        $ menuCrearUsuario��5�      $��CREAR USUARIO��65   $��15(�6L   $��25��6Z   $��Heavy"�6f   
���   � 5�      _� 5�      $��CREE UN USUARIO��6"   5�      m� 5�      -"   5        $ menuConsultarUsuarios��5�      $��CONSULTAR USUARIOS��6:   $��15(�6V   $��25��6d   $��Heavy"�6p   
���   � 5�      _� 5�      $?��9HAGA CONSULTA GENÉRICA O INDIVIDUALIZADA DE LOS USUARIOS��6"   5�      5�      5�      9� 5�   
   -#   5         9�Name$ expanderUsers��5�       9� ExpandDirection$
 Left=�6*   $��Right=�6A   $��Bottom=�6]   $��0��6x   
�������$y�Hidden=�6�   ��5�      �� 5�      $��Búsqueda Usuarios��6   $��Center=�65   $��12(�6P   $
��Bold"�6^    � LayoutTransform 5�      V� 5�      .~�5�      �� �� 5�      �� 5�       �� Angle$ 90Q�6.   T� 5�      5�      5�   3   .��5�      -   '_11FREAKS.Presentacion.BusquedaUsuarios  -$   5          Name$ control��5�   "   5�      �� 5�   
   -%   5         ��Name$ indicadorLbox��5�      $�� ��6'   $y�Visible=�62   $��300,132,700,514q�6H   $��38��6a   $	��500��6m   $��Black"�6y   $��20(�6�   
�������6�   �� 5�   
   -&   5         ��Name$ 	miListBox��5�      $y�Hidden=�6%   $��Center=�69   $��Center=�6T   $	��800��6q   $	��350��6}    � ContextMenu 5�      �� 5�      .@�5�      m� -'   5        $��OCULTAR��5�      60   5�       5�      _� 5�      $a��[PUEDE OCULTAR ESTA VISTA PULSANDO CLICK DERECHO, O BUSCAR INFORMACIÓN DESDE CLASIFICACIÓN��6   5�      �� 5�   
   -(   5        $ jugadoresListBox��5�      $	��600��6,   $	��450��69   $ ESTA ES TU PLANTILLA��6E   $��Center=�6�   $��	0,71,0,13q�6�   ��5�      �� 5�      .R�5�      �   � ColumnDefinitions  5�      �� 5�      $	��150 �6/   �� 5�      $	��250 �6/   .+�5�      �� $	��100��68   $	��100��6D   $��10q�6Q   $
}�True��6]   ��6    �� *Foto+! J� Clip! 5�      @� 5�   "   " @� RadiusX$" 50Q�62   # @� RadiusY$# 50Q�6?   $ @� Center$$ 50,50&�6L   5�      �� 5      $��1��6%   $��10q�65   .+�5     �� $
��Bold"�6@   $��23(�6R   ��6(   �� *Nombre+6R   �� 5     $��Italic$�6B   $��14(�6U   ��6(   �� *
Posicion+�� 5  "   $�    #��6&    �� 5  "   �6&   �� *Dorsal+5     �� 5     ��6(   �� *Equipo+�� 5     $��14(�6(   $
��Bold"�66   �� 5  "   �6&   �� *Valor+ �� 5	  "   $	�€��6&   5
     5     �� 5     $��50��6:   $��50��6E   $��2��6Q   $��Right=�6a   $��30,50q�6}   ��6    �� *Escudo+6}   5     5      5     % �� 
TargetType& �� Property' �� Value( �� 
TargetType) �� CornerRadius��%�  '��       �� 5     "% ��6   .�5     �� & � ��
' �   �64   �� 5     & � ��$' 5q�68   �� 5     & � ��' �#E8E8E863   �� 5     & � ��' 5     �� 5     "( ��6.   .R�5  "   �� #��z��6)   #��z��5  !   #��z��5  !   $) 25��5  !   ._�5  &   �� $
��Auto=�63   $��Hidden=�5  +   .��5  *   �� 5  '   5   #   5!     5#     5$     5&     5'     �� 5-  
   -)   5        $ equiposLigaListBox��5-  '   $y�Visible=�6   $	��600��6C   $	��450��6P   $ ESTA ES TU PLANTILLA��6\   $��Center=�6|   $��	0,71,0,13q�6�   ��5.     �� 5/     .R�50     �   51     �� 52     $��80 �6/   �� 53     $	��100 �6/   �� 54     $	��250 �6/   .+�57     �� $��0��6%   $��10q�65   .+�58     �� $
��Bold"�6(   $��23(�6:   �� 59  "   �6&   �� *
Posicion+ �� 5:  "   $�º��6&   5;     5<     �� 5>     $��1��6%   $��10q�65   .+�5?     �� $
��Bold"�6(   $��23(�6:   �� 5@  "   �6&   �� *Puntos+ �� 5A  "   $	�pts��6&   5B     5C     �� 5E     $��2��6%   $��10q�65   .+�5F     �� $
��Bold"�6(   $��20(�6:   ��6H   �� *Nombre+�� 5G     $��Italic$�6(   ��6;   �� *Abreviatura+5H     5I     5K      5M     ��%�  '��       �� 5N     "% ��6   .�5O     �� & � ��
' �   �64   �� 5P     & � ��$' 5q�68   �� 5Q     & � ��' �#E8E8E863   �� 5R     & � ��' 5S     �� 5T     "( ��6.   .R�5U  "   �� #��z��6)   #��z��5V  !   #��z��5W  !   $) 25��5X  !   ._�5Y  &   �� $
��Auto=�63   $��Hidden=�5Z  +   .��5[  *   �� 5\  '   5]  #   5^     5`     5a     5c     5d     5~     5     