   M S B A M L   `   `   `  ����D  @Launcher4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Launcher4	Launcher4  5   	       Launcher4.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Launcher4     5   	   -   5          �Name$
  Win1��5   	    � Title$ 
MainWindow��5   	   $	��501��6   $	��851��6)    � 
ResizeMode$ NoResize=�65    � WindowStartupLocation$ CenterScreen=�6K    � AllowsTransparency .  � WindowStyle$
 None=�6�   ��6�   Z� .��5	      � .+�5
   
   �� -   5         ��Name$ formBG��5
      $
��Left=�6    $	��501��6;   $	��Top=�6H   $	��851��6`   $��Resources/launcherv4.png��6l   6�   � 5   
   -   5         �Name$ pbarDownload��5      $
��Left=�6,   $��10��6G   $	��Top=�6S   $	��497��6k   $��182,423,0,0q�6w   $��0q�5      ���	#FF2AFF08��5   �   Z� ��5   A   Z� �� 5   
   -   5        $ btn_play��5      $
��Left=�6"   $��72��6=   $��360,346.607,0,0q�6I   $	��Top=�6b   $	��166��6z    � Cursor$
 Hand��6�   $��Resources/btn_play_c.png��6�   6�   �� 5   
   $
��Left=�6   $	��104��6+   $��56,364.667,0,0q�68   $	��Top=�6P   $	��142��6h   $��Resources/leaf_left.png��6t   �� 5   
   $
��Left=�6   $	��104��6+   $��666.665,364.667,0,0q�68   $	��Top=�6U   $	��142��6m   $��Resources/leaf_right.png��6y   �� 5   
   -   5        $ btnclose��5      $
��Left=�6"   $��21��6=   $��674.995,134.162,0,0q�6I   $	��Top=�6f   $��21��6~   $
 Hand��6�   $��Resources/btn_close.png��6�   6�   �� 5   
   -   5        	 ��Name$	 labelServerStatus��5      $��Checking server status..��6+   $��Center=�6N   $��Center=�6l   $��102,47,598,430q�6�   $��Center=�6�   $��Cambria)�6�   $
��Bold"�6�   
�������$	��151��6�   �� 5   
   -   5        $	 labelRealmlist��5      $	��...��6(   $��Center=�66   $��Center=�6S   $��88,66,588,409q�6w   $��Center=�6�   ���	#FFEEEEEE$
��Bold"�6�   $��Cambria)�6�   $	��175��6�   $��14(�6�   �� 5   
   -   5        $	 labelDownloaded��5      $�� ��6)   $��Center=�64   $��Center=�6Q   $��Center=�6u   $��24��6�   $��182,433,508,44q�6�   $��Center=�6�   $	��161��6�   $��Cambria)�6�   ���	#FF555580$
��Bold"�6  $��14(�6   �� 5   
   -	   5        $	 
labelSpeed��5      $�� ��6$   $��Center=�6/   $��Center=�6L   $��Center=�6p   $��24��6�   $��348,433,286,44q�6�   $��Center=�6�   $	��217��6�   $��Cambria)�6�   ���	#FF555580$
��Bold"�6	  $��14(�6  �� 5   
   -
   5        $	 	labelPerc��5      $�� ��6#   $��Center=�6.   $��Center=�6K   $��Center=�6o   $��24��6�   $��570,433,172,44q�6�   $��Center=�6�   $	��109��6�   $��Cambria)�6�   ���	#FF555580$
��Bold"�6  $��14(�6  5      5      