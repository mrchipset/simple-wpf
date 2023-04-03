using BindingCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicBindingLabel
{
	class ContentModel : NotifyObject
	{
		private string _content;

		public string Content
		{
			get { return _content; }
			set
			{
				_content = value;
				OnPropertyChanged();
			}
		}
		public ContentModel()
		{
			Content = string.Empty;
		}


	}
}
