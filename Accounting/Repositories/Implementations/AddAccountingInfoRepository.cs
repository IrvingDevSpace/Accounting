using Accounting.Models;
using Accounting.Repositories.Interfaces;
using CSV;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Repositories.Implementations
{
    internal class AddAccountingInfoRepository : IAddAccountingInfoRepository
    {
        public List<AddAccountingInfo> GetAddAccountingInfos(SearchDate searchDate)
        {
            String fileServerPath = "C:\\Users\\IRVING\\Program Course\\Code\\Accounting\\FileServer\\";
            if (searchDate.StartTime > searchDate.EndTime)
                return null;
            int diffDays = (searchDate.EndTime - searchDate.StartTime).Days + 1;
            List<String> directories = Directory.GetDirectories(fileServerPath).ToList();
            List<AddAccountingInfo> addAccountingInfos = new List<AddAccountingInfo>();
            for (int i = 0; i < diffDays; i++)
            {
                String directoryName = $"{fileServerPath}{searchDate.StartTime.AddDays(i).ToString("yyyy-MM-dd")}";
                foreach (var directory in directories)
                {
                    if (directory == directoryName)
                    {
                        addAccountingInfos.AddRange(CSVHelper.Read<AddAccountingInfo>($"{directoryName}\\Data.csv"));
                        break;
                    }
                }
            }
            return addAccountingInfos;
        }
    }
}
