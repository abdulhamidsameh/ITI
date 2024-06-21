using ITI.BLL.Interfaces;
using ITI.BLL.Repositories;
using ITI.DAL.Data;

namespace ITI.PL.Extenshions
{
	public static class ApplicationServicesExtenshion
	{
		public static IServiceCollection ApplicationServices(this IServiceCollection services)
		{
			services.AddControllersWithViews();

			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

			services.AddScoped(typeof(IStudentRepository), typeof(StudentRepository));

			return services;
		}
	}
}
