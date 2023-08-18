using DemoDAl.Contexts;
using DemoDAL.Models;
using System.Linq;

namespace DmoPL.Helper
{
    public static class SeedData
    {
        public static void SeedAccountAndBusinessLine(MVCContext context)
        {
            if (!context.Accounts.Any())
            {
                context.Accounts.Add(new Account() { AccountName= "TE Data" });
                context.Accounts.Add(new Account() { AccountName= "Telecom Egypt" });
                context.SaveChanges();
            }
            if (!context.BusinessLines.Any())
            {
                context.BusinessLines.Add(new BusinessLine() {Name= "Basic",AccountId=1});
                context.BusinessLines.Add(new BusinessLine() {Name= "Technical Support", AccountId=1});
                context.BusinessLines.Add(new BusinessLine() {Name= "Inbound", AccountId=2});
                context.BusinessLines.Add(new BusinessLine() {Name= "Outbound", AccountId=2});
                context.SaveChanges();
            }
        }
    }
}
