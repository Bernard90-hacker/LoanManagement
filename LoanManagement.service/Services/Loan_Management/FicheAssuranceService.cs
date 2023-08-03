using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Previewer;
using Microsoft.AspNetCore.Hosting;

namespace LoanManagement.service.Services.Loan_Management
{
	public class FicheAssuranceService
	{
		private IUnitOfWork _unitOfWork;
		public FicheAssuranceService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<bool> GeneratePdf(DossierClient dossier)
		{
			await Test(dossier);
			return true;
		}

		public async Task Test(DossierClient dossier)
		{
			var client = await _unitOfWork.Clients.GetByIdAsync(dossier.ClientId);
			Compte? compte = await _unitOfWork.Comptes.GetByClient(client.Id);
			var infos = await _unitOfWork.DossierClients.GetInfoSanteByDossier(dossier.Id);
			StatutMarital? statutMarital = await _unitOfWork.StatutMaritals
				.GetById(dossier.StatutMaritalId);
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
							column.Item().CustomerFirstPart(client, compte, statutMarital.Libelle);
							column.Item().PaddingTop(10);
							column.Item().BankPart();
							column.Item().PaddingTop(10);
							column.Item().CustomerSecondPart(dossier);
							column.Item().CustomerLastPart(infos);
							column.Item().PageBreak();
							column.Item().ContractTitle();
							column.Item().ContractInformation();
						});


					//page.Footer()
					//.Background(Colors.Green.Lighten1)
					//.Height(1, Unit.Inch);

				});
			});
			a.GeneratePdf($"Files/{client.Nom} {client.Prenoms}.pdf");
			a.GeneratePdfAndShow();

        }
	}
}
