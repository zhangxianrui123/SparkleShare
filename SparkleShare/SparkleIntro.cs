//   SparkleShare, an instant update workflow to Git.
//   Copyright (C) 2010  Hylke Bons <hylkebons@gmail.com>
//
//   This program is free software: you can redistribute it and/or modify
//   it under the terms of the GNU General Public License as published by
//   the Free Software Foundation, either version 3 of the License, or
//   (at your option) any later version.
//
//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   GNU General Public License for more details.
//
//   You should have received a copy of the GNU General Public License
//   along with this program.  If not, see <http://www.gnu.org/licenses/>.

using Gtk;
using Mono.Unix;
using SparkleShare;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Timers;

namespace SparkleShare {

	public class SparkleIntro : Window
	{

		// Short alias for the translations
		public static string _ (string s)
		{
			return Catalog.GetString (s);
		}

		public SparkleIntro () : base ("")
		{

			BorderWidth = 0;
			SetSizeRequest (600, 400);
			Resizable = false;
			IconName = "folder-sparkleshare";

			WindowPosition = WindowPosition.Center;

			HBox layout_horizontal = new HBox (false, 6);

				Image side_splash = new Image ("/home/hbons/github/SparkleShare/data/side-splash.png");

			layout_horizontal.PackStart (side_splash, false, false, 0);
			
			VBox wrapper = new VBox (false, 0);
			
			VBox layout_vertical = new VBox (false, 0);
			
				Label introduction = new Label ("<span size='xx-large'><b>Welcome to SparkleShare!</b></span>");
				introduction.UseMarkup = true;
				introduction.Xalign = 0;
				
				Label information = new Label ("Before we can create a SparkleShare folder on this \n" +
				                               "computer, we need a few bits of information from you.");
				information.Xalign = 0;
				

					Entry name_entry = new Entry ("");
					Label name_label = new Label (_("<b>Full Name:</b>"));
					
					UnixUserInfo unix_user_info = new UnixUserInfo (UnixEnvironment.UserName);			

					name_entry.Text = unix_user_info.RealName;
					name_label.UseMarkup = true;
					name_label.Xalign = 0;

				
				Table table = new Table (6, 2, true);
				table.RowSpacing = 6;
				

					Entry email_entry = new Entry ("");
					Label email_label = new Label (_("<b>Email:</b>"));
					email_label.UseMarkup = true;
					email_label.Xalign = 0;

					Entry server_entry = new Entry ("ssh://gitorious.org/sparkleshare");
					Label server_label = new Label (_("<b>Folder Address:</b>"));
					server_label.UseMarkup = true;
					server_label.Xalign = 0;
					server_label.Sensitive = false;
					server_entry.Sensitive = false;
					
					CheckButton check_button = new CheckButton ("I already have an existing folder on a SparkleShare server");
					check_button.Clicked += delegate {
						if (check_button.Active) {
							server_label.Sensitive = true;
							server_entry.Sensitive = true;
							server_entry.HasFocus = true;
						} else {
							server_label.Sensitive = false;
							server_entry.Sensitive = false;					
						}
						ShowAll ();
					};

				table.Attach (name_label, 0, 1, 0, 1);
				table.Attach (name_entry, 1, 2, 0, 1);
				table.Attach (email_label, 0, 1, 1, 2);
				table.Attach (email_entry, 1, 2, 1, 2);
				table.Attach (check_button, 0, 2, 3, 4);
				table.Attach (server_label, 0, 1, 4, 5);
				table.Attach (server_entry, 1, 2, 4, 5);
				
			HButtonBox controls = new HButtonBox ();
			controls.Layout = ButtonBoxStyle.End;
			Button done_button = new Button ("Next");
			controls.Add (done_button);

			layout_vertical.PackStart (introduction, false, false, 0);
			layout_vertical.PackStart (information, false, false, 21);
			layout_vertical.PackStart (new Label (""), false, false, 0);
			layout_vertical.PackStart (table, false, false, 0);

			wrapper.PackStart (layout_vertical, true, true, 0);
			layout_vertical.BorderWidth = 30;
			controls.BorderWidth = 12;
			wrapper.PackStart (controls, false, true, 0);
			layout_horizontal.PackStart (wrapper, true, true, 0);

			Add (layout_horizontal);
			ShowAll ();

		}

	}

}
