using System.Threading.Tasks;

namespace JakeJones.Home.Core.Managers
{
	public interface INotificationManager
	{
		Task SendNotificationAsync(string subject, string message);
	}
}