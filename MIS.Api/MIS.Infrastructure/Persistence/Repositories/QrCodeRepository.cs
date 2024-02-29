using Microsoft.EntityFrameworkCore;
using MIS.Application.Models.Requests;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using MIS.Domain.Entities;
using MIS.Infrastructure.Persistence.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MIS.Infrastructure.Persistence.Repositories
{
    public class QrCodeRepository : Repository<QrCode>, IQrCodeRepository
    {
        public QrCodeRepository(MISDbContext context) : base(context)
        {
        }

        public static int ExtractNumbers(string value)
        {
            // Use Regex to extract numeric part from the value
            string numericPart = Regex.Replace(value, "[^0-9]", "");

            // Parse the numeric part to an integer
            if (int.TryParse(numericPart, out int number))
            {
                return number;
            }
            else
            {
                // Handle parsing error if needed
                throw new ArgumentException("Invalid numeric part in the value.");
            }
        }
        public async Task<List<QrCodeResponse>> AddListOfQrCodes(PostQrCodesListRequest request)
        {
            var transaction = context.Database.BeginTransaction();
            try
            {
                int count = 0;
                var bureau = await context.Bureaus.FirstOrDefaultAsync(b => b.Id == request.BureauId);
                if (bureau == null)
                    return null;
                var lastqrCode = await context.QrCodes.Where(q => q.DeletedAt == null && q.Designation.Contains(bureau.Abbreviation)).OrderByDescending(q => q.CreatedAt).FirstOrDefaultAsync();
                if (lastqrCode == null)
                    count += 1;
                else
                {
                    string output = string.Concat(lastqrCode.Designation.Where(Char.IsDigit));
                    count = int.Parse(output) + 1;
                }

                List<QrCode> qrCodesList = new();

                for (int i = 0; i < request.QrCodesNumber; i++)
                {
                    qrCodesList.Add(new QrCode()
                    {
                        Designation = bureau.Abbreviation + (count + i).ToString("D3")
                    });
                }

                context.Set<QrCode>().AddRange(qrCodesList);
                if (await context.SaveChangesAsync() > 0)
                {
                    transaction.Commit();
                    return qrCodesList.Select(q => new QrCodeResponse { Value = q.Designation}).ToList();
                }
                else
                {
                    transaction.Rollback();
                    return null;
                }
        }
            catch
            {
                transaction.Rollback();
                return null;
            }



}
    }
}
