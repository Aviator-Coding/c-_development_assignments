   M S B A M L   `   `   `  ����E  ALektion3_2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Lektion3_2
Lektion3_2  5   	       Lektion3_2.Lieferanten   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Lektion3_2     5   	     � Title$  Lieferanten��5   	   $	��450��6   $	��800��6*    � 	Resources 5	         meinTemplate��%�   &	        �� 5
   
   .R�5      ]� .+�5      �� $
��Bold"�66   $
��Left=�6H   $��85��6^   ��6   �� *	vorname+6^   �� 5      $
��Thin"�66   $��90��6H   ��6   �� *
nachname+6H   5      5      5      .��5      �  � ColumnDefinitions 5   
   �� 5      �� 5       � RowDefinitions 5   
   �� 5      �� 5      $��80 �6   .+�5   
   �� -   5        $��lbLieferanten��5      $	��200��6'   $	��300��64   �� 5   
   -   5        $��btnLoeschen��5      $��1��6$   $	��100��61   $��30��6=   .��6c   
Löschen6l   �� 5   
   $��1��6    � DataContext 6%   �� *SelectedItem+ �� ElementName$ 
cbPersonen��.+�5      �� $	��200��6   $
��Bold"�6    $��16(�62   .��6@   Lieferanten hinzufügen6X   �� 5      -   5        $��
cbPersonen��5   )    �� SelectedIndex$ 0��6   $	��200��6;   #��[  6p   �� 5      $��16(�6   $
��Bold"�6"   $	��150��64   .��6@   Firmenname:6M   �� 5       -   5        $��tbFirma��5       $	��150��6%   $��30��61   �� 5!      -   5        $��btnAdd��5!      $	��100��6#   $��30��6/   .��6P   Hinzufügen6\   5"      �� 5#   
   -   5        $��
btnZurueck��5#      $	��100��6#   $��30��6/   $��1��6;   $��1��6H   .��6q   	Zurück6y   5$      5%      