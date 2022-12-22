using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace LevelUpCSharp.Production
{
    public class JobCenter
    {
        private readonly string _village;

        public JobCenter(string village)
        {
            _village = village;
        }

        public IEmployee Hire()
        {
            var villageName = Directory.GetFiles(_village).FirstOrDefault(file => new FileInfo(file).Extension == ".dll");
            
            var missingPluginAssembly = villageName == null;
            if (missingPluginAssembly)
            {
                return BackupEmployee();
            }

            Assembly village;
            try
            {
                village = Assembly.LoadFrom(villageName);
            }
            catch
            {
                return BackupEmployee();
            }

            Type[] candidates;

            try
            {
                candidates = village.GetTypes().Where(type => IsCompetent(type)).ToArray();
            }
            catch(Exception ex)
            {
                return BackupEmployee();
            }

            var lackOfCompetencies = !candidates.Any();
            if (lackOfCompetencies)
            {
                return BackupEmployee();
            }

            IEmployee employee = null;
            foreach (var candidate in candidates)
            {
                try
                {
                    employee = (IEmployee)Activator.CreateInstance(candidate);
                }
                catch
                {
                }
            }

            return employee ?? BackupEmployee();
        }

        private bool IsCompetent(Type type)
        {
            return type.GetInterfaces().Contains(typeof(IEmployee)) && type.IsAbstract == false;
        }

        private static SandwichMaster BackupEmployee()
        {
            return new SandwichMaster();
        }
    }
}