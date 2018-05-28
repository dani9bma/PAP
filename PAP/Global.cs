using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using WMPLib;

namespace PAP
{
	static class Global
	{

		public static TimeSpan Duration(String file)
		{
			// ...
			var player = new WindowsMediaPlayer();
			var clip = player.newMedia(file);
			return TimeSpan.FromSeconds(clip.duration);
		}

		public static Database sql = new Database();
		public static string RootMusic;
		public static List<Musica> playlist = new List<Musica>();
		public static int playlist_index;
		public static int playlist_max_index;
	}
}
