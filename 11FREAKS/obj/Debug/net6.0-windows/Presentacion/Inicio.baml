   M S B A M L   `   `   `  ����C  ?11FREAKS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:_11FREAKS.Presentacion_11FREAKS.Presentacion  5   	   #    _11FREAKS.Presentacion.Inicio   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:_11FREAKS.Presentacion     5   	   -   5          � Title$  	11 FREAKS��5	   	   $	��600��6   $
��1000��6(    � WindowStartupLocation$ CenterScreen=�6L    � Icon$! /Resources/Logo11Freaks.png��6q    � 
ResizeMode$ NoResize=�6�    � 	Resources 5         estiloMarcador �� 
TargetType �� Property �� Value��%A  &	        �� 5   
   " ��6'   .�5      ��  � ��$ 60s Scoreboard)�6+   �� 5       � ��
 �   �6+   �� 5       � ��
 �  ��6+   �� 5       � ��$ 70(�6)   �� 5       � ��$ 8q�6(   �� 5       � ��$ Center=�6;   �� 5       � ��$ Center=�69   5      5      .��5      � -   5         �Name$ miGrid��5      ��5   
   �� 5      	 �� ImageSource$	 /Resources/bgStadium.jpg��6   
 j� Stretch$
 UniformToFill=�6@   .+�5   
   �� -   5         ��Name$
 logo��5      $!��/Resources/Logo11Freaks.png��6   $��726,164,92,264q�6C    � ToolTip 5       _� 5!      $<��6Desarrollado Por Miguel Díaz Osa sin fines lucrativos��6   5#      �� 5%   
   -   5        $ iconoUsuario��5%      $!��/Resources/iconoUsuario.png��6&   $��410,58,420,362q�6K   �� 5'   
   -   5         ��Name$ 
txtUsuario��5'      $��Center=�6&   $��	0,257,0,0q�6C    �� TextWrapping$
 Wrap=�6V   $��RetroSlasher��6j   $	��Top=�6~   $	��230��6�   $��40��6�   $��25(�6�   .��6�    5(      _� 5)      $��Introduzca Nombre Usuario��6   .��5*      5+      G� 5,   
   -   5         G�Name$ txtPassword��5,       G� Password$
 1234��6+   $��Center=�6;   $��	0,337,0,0q�6X   $	��Top=�6l    J� RenderTransformOrigin$ 0.542,-2.225&�6�   $	��230��6�   $��40��6�   $��25(�6�    5-      _� 5.      $��Introduzca Su Contraseña��6   50      �� 51   
   $��USUARIO��6   
�������$
��Left=�65   $��245,257,0,0q�6P   $	��Top=�6e   $	��150��6}   $��40��6�   $��18(�6�   $
��Bold"�6�    52      _� 53      $>��8El Usuario Tendrá una longitud máxima de 20 caracteres��6   55      �� 56   
   $��CONTRASEÑA��6   
�������$
��Left=�68   $��240,337,0,0q�6S   $	��Top=�6h   $	��160��6�   $��40��6�   $��18(�6�   $
��Bold"�6�    57      _� 58      $i��cLongitud Mínima Contraseña será de 7 caracteres, y contendrá al menos 1 Mayúscula y un Número��6   5:      �� 5;   
   -   5         ��Name$ btnInicioSesion��5;      $��INICIAR SESIÓN��6*   $��325,415,327,0q�6C   $	��Top=�6Z   $��44��6r   $��16(�6~   $
��Bold"�6�   5=      �� 5?   
   -   5        $ btnRegistrarse��5?      $��REGÍSTRATE��6)   $
��Left=�6>   $��738,420,0,0q�6Y   $	��Top=�6n   $��37��6�   $	��170��6�   $
��Bold"�6�    5@      _� 5A      $��Introduzca Su Contraseña��6   5C      �� 5D   
   $��¿NO TIENES CUENTA?��6   $
��Left=�6-   $��755,389,0,0q�6H   $	��Top=�6]   $	��135��6u   $��28��6�   $��Italic$�6�   
������� 5E      _� 5F      $8��2Longitud Mínima Contraseña será de 7 caracteres��6   5H      �� 5J   
   -	   5        $
 foto��5J      $
��Left=�6   $	��200��6;   $	��200��6H   $��Center=�6T   $��75,0,0,0q�6o   �� 5K   
   $.��(*NOTA* ADMINISTRADOR CONTRASEÑA--> 1234��6   $y�	Collapsed=�6B   $
��Left=�6Y   $��
25,168,0,0q�6t   $	��Top=�6�   $��45��6�   $	��350��6�   
�������$��15(�6�   �� 5L   
   -
   5         ��Name$ lblMarcador��5L      $��RMA 2-1 ATM��6%   $��40,28,355,395q�6;   #��[  6R   $y�Hidden=�6z   �� 5M   
   -   5        $ bgSolid��5M      $y�	Collapsed=�6!   $��/Resources/bgSolid.jpg��68   $
��Left=�6X   $	��603��6s   $��
-18,-6,0,0q�6�   $	��Top=�6�   $
��1063��6�   r� 5N   
   -   5         r�Name$ video��5N      $��Center=�6&   $	��602��6C    r� UnloadedBehavior$ Manual=�6P    r� LoadedBehavior$
 Play=�6j   $	��980��6�   $	��Top=�6�    r� Volume$ 1Q�6�    r� Source$% https://i.imgur.com/g6u54yV.mp4?�6�   $��0,10,0,0q�6�   �� 5P   
   -   5        $ 
imgVolumen��5P      $y�	Collapsed=�6$   $
��Left=�6;   $��75��6W   $��60��6c   $	��Top=�6n   $��870,462,0,0q�6�   $��/Resources/volumenAct.png��6�   6�   �� 5R   
   -   5        $ btnSkip��5R      $��       SALTAR��6"   $y�	Collapsed=�6;   $
��Left=�6R   $��
55,462,0,0q�6m   $	��Top=�6�   $��60��6�   $	��200��6�   $
��Bold"�6�   
�������
���   �$��18(�6  $��Medium&�6  $
��Left=�6%   5S      _� 5T      $��Omitir Intro��6   5V      �� 5X   
   -   5        $ imgSkip��5X      $y�	Collapsed=�6!   $
��Left=�68   $��34��6S   $��196,475,0,0q�6_   $	��Top=�6t   $��35��6�   $��/Resources/skip.png��6�   6�   �� 5Y   
   -   5        $ lblMute��5Y      $��I��6!   $y�	Collapsed=�6-   $	��100(�6D   $��886,417,74,13q�6S   $ 0.5,0.5&�6j   z�5Z      V� 5[      .~�5\      �� �� 5]      �� 5^       �� Angle$ 41.641Q�6&   T� 5_      5`      5b      5e      5f      