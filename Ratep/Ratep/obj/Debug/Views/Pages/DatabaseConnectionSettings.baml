   M S B A M L   `   `   `  ����@  <Ratep, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Ratep.Views.PagesRatep.Views.Pages  5      2    ,Ratep.Views.Pages.DatabaseConnectionSettings   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QScottPlot.WPF, Version=4.1.37.0, Culture=neutral, PublicKeyToken=e53b06131e34a3aaW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:Ratep.Views.Pages     5        N� Title$   DatabaseConnectionSettings��5	       N� FontSize$ 14(�5
      .��5      �� $��Center=�6   $��Center=�6*   ���#F9F9F9$��1q�5      ���#E8E8E8 �� CornerRadius$ 15��67   ._�5   
   � $��15q�6    � RowDefinitions 5      �� 5      $��55 �6    �� 5      $��35 �6    �� 5      $��15 �6    �� 5      $��35 �6    �� 5      $��15 �6    �� 5      $��35 �6    .+�5      ��    
HeaderText#���   6   $6��0Настройка соединения с БД:��69   � 5      $��1��6   .+�5      �� $��Center=�6   ���#333333$��
5, 0, 0, 0q�6H   $
��Left=�6\   .��6w   Имя сервера:6�   �� 5      ���#E8E8E8$��1q�6.   ���#E0E0E0$ 15��6X   $��Right=�5      ._�5      � $	��155��6   .+�5       �� -   5        $��
ServerName��5    "   
������ $
��auto��6M   $��Center=�6Z   $��0q�6u   $��5q�6�   5!      5"      5#      � 5%      $��3��6   .+�5&      �� $��Center=�6   ���#333333$��
5, 0, 0, 0q�6H   $
��Left=�6\   .��6w   Имя БД:6�   �� 5'      ���#E8E8E8$��1q�6.   ���#E0E0E0$ 15��6X   $��Right=�5(      ._�5)      � $	��155��6   .+�5*      �� -   5         ��Name$ DBName��5*   "   
������ $
��auto��6K   $��Center=�6X   $��0q�6s   $��5q�6�   5+      5,      5-      � 5/      $��5��6   .+�50      �� -   5          
MainButton#���  50      $	��120��6>   $��30��6J   $
��Left=�6n   .��6�   
Назад6�   �� 51      -   5        #���  51      $	��120��6>   $��30��6J   $��Right=�6q   .��6�   Присоединиться6�   52      53      54      55      