   M S B A M L   `   `   `  ���� C  ?11FREAKS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:_11FREAKS.Presentacion_11FREAKS.Presentacion  #    _11FREAKS.Presentacion.Inicio   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6local$clr-namespace:_11FREAKS.Presentacion     -     � Title$  	11 FREAKS��$	��600��$
��1000�� � WindowStartupLocation$ CenterScreen=� � Icon$! /Resources/Logo11Freaks.png�� � 
ResizeMode$ NoResize=� � 	Resources    estiloMarcador �� 
TargetType �� Property �� Value��%�   &	        �� " ��.���  � ��$ 60s Scoreboard)���  � ��
 �   ���  � ��
 �  ����  � ��$ 70(���  � ��$ 8q���  � ��$ Center=���  � ��$ Center=�.��� -    �Name$ miGrid������ 	 �� ImageSource$	 /Resources/bgStadium.jpg��
 j� Stretch$
 UniformToFill=�.+��� -    ��Name$
 logo��$!��/Resources/Logo11Freaks.png��$��726,164,92,264q� � ToolTip _� $<��6Desarrollado Por Miguel Díaz Osa sin fines lucrativos���� -   $ iconoUsuario��$!��/Resources/iconoUsuario.png��$��410,58,420,362q��� -    ��Name$ 
txtUsuario��$��Center=�$��	0,257,0,0q� �� TextWrapping$
 Wrap=�$��RetroSlasher��$	��Top=�$	��230��$��40��$��25(�.�� _� $��Introduzca Nombre Usuario��.��G� -    G�Name$ txtPassword�� G� Password$
 1234��$��Center=�$��	0,337,0,0q�$	��Top=� J� RenderTransformOrigin$ 0.542,-2.225&�$	��230��$��40��$��25(� _� $��Introduzca Su Contraseña���� $��USUARIO��
�������$
��Left=�$��245,257,0,0q�$	��Top=�$	��150��$��40��$��18(�$
��Bold"� _� $>��8El Usuario Tendrá una longitud máxima de 20 caracteres���� $��CONTRASEÑA��
�������$
��Left=�$��240,337,0,0q�$	��Top=�$	��160��$��40��$��18(�$
��Bold"� _� $i��cLongitud Mínima Contraseña será de 7 caracteres, y contendrá al menos 1 Mayúscula y un Número���� -    ��Name$ btnInicioSesion��$��INICIAR SESIÓN��$��325,415,327,0q�$	��Top=�$��44��$��16(�$
��Bold"��� -   $ btnRegistrarse��$��REGÍSTRATE��$
��Left=�$��738,420,0,0q�$	��Top=�$��37��$	��170��$
��Bold"� _� $��Introduzca Su Contraseña���� $��¿NO TIENES CUENTA?��$
��Left=�$��755,389,0,0q�$	��Top=�$	��135��$��28��$��Italic$�
������� _� $8��2Longitud Mínima Contraseña será de 7 caracteres���� -	   $
 foto��$
��Left=�$	��200��$	��200��$��Center=�$��75,0,0,0q��� $.��(*NOTA* ADMINISTRADOR CONTRASEÑA--> 1234��$y�	Collapsed=�$
��Left=�$��
25,168,0,0q�$	��Top=�$��45��$	��350��
�������$��15(��� -
    ��Name$ lblMarcador��$��RMA 2-1 ATM��$��40,28,355,395q�#��[  $y�Hidden=��� -   $ bgSolid��$y�	Collapsed=�$��/Resources/bgSolid.jpg��$
��Left=�$	��603��$��
-18,-6,0,0q�$	��Top=�$
��1063��r� -    r�Name$ video��$��Center=�$	��602�� r� UnloadedBehavior$ Manual=� r� LoadedBehavior$
 Play=�$	��980��$	��Top=� r� Volume$ 1Q� r� Source$% https://i.imgur.com/g6u54yV.mp4?�$��0,10,0,0q��� -   $ 
imgVolumen��$y�	Collapsed=�$
��Left=�$��75��$��60��$	��Top=�$��870,462,0,0q�$��/Resources/volumenAct.png���� -   $ btnSkip��$��       SALTAR��$y�	Collapsed=�$
��Left=�$��
55,462,0,0q�$	��Top=�$��60��$	��200��$
��Bold"�
�������
���   �$��18(�$��Medium&�$
��Left=� _� $��Omitir Intro���� -   $ imgSkip��$y�	Collapsed=�$
��Left=�$��34��$��196,475,0,0q�$	��Top=�$��35��$��/Resources/skip.png���� -   $ lblMute��$��I��$y�	Collapsed=�$	��100(�$��886,417,74,13q�$ 0.5,0.5&�z�V� .~��� �� ��  �� Angle$ 41.641Q�T� 