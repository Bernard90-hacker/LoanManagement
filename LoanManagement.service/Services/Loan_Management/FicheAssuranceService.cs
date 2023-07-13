using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace LoanManagement.service.Services.Loan_Management
{
	public class FicheAssuranceService
	{
		public Task<bool> GeneratePdf()
		{
			Test();
			return Task.FromResult(true);
		}

		public void Test()
		{
			QuestPDF.Settings.License = LicenseType.Community;
			var a = Document.Create(container =>
			{
				container.Page(page =>
				{
					page.Size(PageSizes.A3);
					page.Margin(2, Unit.Centimetre);
					page.PageColor(Colors.Blue.Lighten4);

					page.Header()
					.ShowOnce()
					.ComposeHeader();

					page.Content()
						.Column(column =>
						{
							column.Item().PaddingTop(10);
							column.Item().AssurancePart();
							column.Item().CustomerFirstPart();
							column.Item().PaddingTop(10);
							column.Item().BankPart();
							column.Item().PaddingTop(10);
							column.Item().CustomerSecondPart();
							column.Item().CustomerLastPart();
							column.Item().PageBreak();
							column.Item().ContractTitle();
							column.Item().ContractInformation();
						});


					//page.Footer()
					//.Background(Colors.Green.Lighten1)
					//.Height(1, Unit.Inch);

				});
			});
			a.GeneratePdf("Files/test.pdf");
		}
	}
}
