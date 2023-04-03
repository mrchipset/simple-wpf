using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BindingCommon
{
	public class NotifyObject : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		// Create the OnPropertyChanged method to raise the event
		// The calling member's name will be used as the parameter.
		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}