using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;


namespace GaugesWpfDemoApp
{
    public class Node : TreeViewItem
    {

		private List<String> m_searchTags;
        Color m_textColor = Colors.White; 
	
		public Node(String text, Object tag, Node[] children)
		{
			base.Tag = tag;
            base.Foreground = new SolidColorBrush(m_textColor); 

			base.Header = text;

			if (children != null)
			{
				base.ItemsSource = children;
			}


			m_searchTags = new List<String>();	
		}
		
		public Node(String text, Object tag, Node[] children, String searchTags)
		{
			base.Tag = tag;
            base.Foreground = new SolidColorBrush(m_textColor); 

			base.Header = text;

			if (children != null)
			{
				base.ItemsSource = children;
			}


			m_searchTags = new List<String>();
						
			if (String.IsNullOrEmpty(searchTags) == false)
			{
				searchTags = searchTags.ToLower();
				
				m_searchTags.AddRange(searchTags.Split(new Char[] { ';' }, StringSplitOptions.RemoveEmptyEntries));
			}
		}

		public Boolean HasChildren
		{
			get
			{

				return base.Items.Count > 0;
			}
		}


		public String Text
		{
			get
			{
				return base.Header.ToString();
			}

			set
			{
				base.Header = value;
			}
		}

		public System.Collections.IEnumerable Nodes
		{
			get
			{
				return base.ItemsSource;
			}
		}

		
		public String TagListToString()
		{
			StringBuilder sb = new StringBuilder();

			foreach (String st in m_searchTags)
			{
				sb.Append(st);
				sb.Append(';');
			}
			
			if (sb.Length > 0)
			{
				sb.Remove(sb.Length - 1, 1);
			}
			
			return sb.ToString();
		}
		
		public List<String> SearchTags
		{
			get
			{
				return m_searchTags;
			}
		}
	}
}
