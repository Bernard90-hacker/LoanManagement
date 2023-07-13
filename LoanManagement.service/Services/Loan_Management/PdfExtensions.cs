using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Fluent;

namespace LoanManagement.service.Services.Loan_Management
{
	public static class PdfExtensions
	{
		public static IContainer AddHeader(this IContainer container)
		{
			byte[] imageData = File.ReadAllBytes("Logo/bia togo.png");
			container.Row(row =>
			{
				row.RelativeItem()
				.Image(imageData);

				row.RelativeItem()
				.Text("COUVERTURE EMPRUNTEUR")
				.Bold()
				.FontSize(10);

				row.Spacing(10);
			});

			return container;
		}

		public static IContainer ComposeHeader(this IContainer container)
		{
			byte[] imageData = File.ReadAllBytes("Logo/bia togo.jpg");
			container.Row(row =>
			{
				row.Spacing(50);
				row.ConstantItem(150).AlignCenter()
				.Image(imageData);

				row.RelativeItem()
				.Column(column =>
				{
					column.Item().Text("").FontColor(Colors.White)
					.Bold()
					.FontSize(20);
					column.Item().Text("").FontColor(Colors.White)
					.Bold()
					.FontSize(20);
					column.Item().Text("COUVERTURE EMPRUNTEUR  N° 001808")
					.Bold()
					.FontSize(20);
					column.Item().Text("                                                                                           Bulletin")
					.FontSize(15);
				});
			});
			//container.Column(column =>
			//{
			//	column.Item().Text(text =>
			//	{
			//		text.AlignCenter();
			//		text.Line("COUVERTURE EMPRUNTEUR").FontSize(10);
			//		text.Element().AlignMiddle();

			//	});
			//})
			return container;
		}


		public static IContainer ComposeContent(this IContainer container)
		{
			container
				.PaddingVertical(40)
				.Height(250)
				.Background(Colors.Grey.Lighten3)
				.AlignCenter()
				.AlignMiddle()
				.Text("Content").FontSize(16);

			return container;

		}

		public static IContainer AssurancePart(this IContainer container)
		{
			container.Row(row =>
			{
				row.RelativeItem()
				.Border(2)
				.BorderColor(Colors.Black)
				.EnsureSpace(10)
				.PaddingTop(10)
				.PaddingBottom(10)
				.PaddingLeft(10)

				.Column(column =>
				{
					column.Item()
					.Text("Numéro de police_____________________________Code Agent____________________________Numéro Convention_________________________________");
				});
			});
			return container;
		}

		public static IContainer CustomerFirstPart(this IContainer container)
		{
			container.Row(row =>
			{
				row.RelativeItem()
				.Border(2)
				.BorderColor(Colors.Black)
				.EnsureSpace(10)
				.PaddingTop(10)
				.PaddingBottom(20)
				.PaddingLeft(10)
				.Column(column =>
				{
					column.Item()
					.Text($"Nom____________________________________________________________Prénoms_______________________________________________________________________\n" +
					$"Numéro de Compte _______________________________________________________ Célibataire :    Marié :   Divorcé(e)  :   Veuf(ve)  :   \n" +
					$"Date de naissance :        Lieu de naissance : _____________________________________________________________________________________________________\n" +
					$"Adresse postale_________________________________Téléphone____________________________________________________________________________________________\n" +
					$"Profession________________________________________________Employeur_____________________________________________________________________________");

				});
			});

			return container;
		}

		public static IContainer BankPart(this IContainer container)
		{
			container.Row(row =>
			{
				row.RelativeItem()
				.Border(2)
				.BorderColor(Colors.Black)
				.EnsureSpace(10)
				.PaddingTop(10)
				.PaddingBottom(20)
				.PaddingLeft(10)
				.Column(column =>
				{
					column.Item()
					.Text("Type de prêt :\n" +
					"Court terme(1 à 2 ans) | |  Court terme (2 à 4 ans) | |  Découvert | |  Crédit Moyen Terme | |   C.D.M.H | |  Autre Prêt Immobilier | |\n" +
					"Montant du prêt ______________________________________________Durée du prêt_________________________________(en mois)   Date de la 1ere échéance______________________\n" +
					"Date de la dernière échéance___________________________________Montant de la prime______________________________Surprime________________Prime Totale__________________\n" +
					"Période de paiement:   Unique | |         Annuelle | |     Mensuelle  | |\n" +
					"En cas de décès de l'assuré, avant le terme du contrat, ............................................................est bénéficiaire du capital garanti et ne saurait être remplacé sans son avis écrit");
				});
			});

			return container;
		}

		public static IContainer CustomerSecondPart(this IContainer container)
		{
			container.Row(row =>
			{
				row.RelativeItem()
				.Border(2)
				.BorderColor(Colors.Black)
				.EnsureSpace(10)
				.PaddingTop(10)
				.PaddingBottom(20)
				.PaddingLeft(10)
				.Column(column =>
				{
					column.Item()
					.Text("1. Taille__________________________________                  2. Poids______________________________kg               3. Tension Artérielle_______________________\n" +
					"4. Fumez-vous ?  |_| Oui              |_| Non Si oui, combien de cigarettes par jour?  _______________________\n" +
					"5. Buvez-vous de l'alcool? |_| Pas du tout  |_|   A l'occasion   |_|   Régulièrement     6. Vos distractions________________________\n" +
					"7. Pratiquez-vous un sport?   |_| Oui    |_| Non   Si oui, à quel titre?          |_| Amateur          |_| Professionnel\n" +
					"8. Êtes-vous atteint d'une infirmité? |_| Oui    |_| Non    Si oui, de quelle nature? _______________________________ Date de survenance ______________________");
				});
			});

			return container;
		}


		public static IContainer CustomerLastPart(this IContainer container)
		{
			container.Table(table =>
			{
				table.ColumnsDefinition(column =>
				{
					column.RelativeColumn();
					column.RelativeColumn();
					column.RelativeColumn();
					column.RelativeColumn();
					column.RelativeColumn();
				});

				table.Cell().Row(1).Column(1).Element(Block).Text("Nature");
				table.Cell().Row(1).Column(2).Element(Block).Text("Répondre par Oui ou Non");
				table.Cell().Row(1).Column(3).Element(Block).Text("Si oui, précisez");
				table.Cell().Row(1).Column(4).Element(Block).Text("Période de traitement");
				table.Cell().Row(1).Column(5).Element(Block).Text("Lieu de traitement (CHU CMS ONG CENTRE ...)");
				table.Cell().Row(2).Column(1).Element(Block).Text("Avez-vous été malade lors des 6 derniers mois ?");
				table.Cell().Row(2).Column(2).Element(Block).Text("");
				table.Cell().Row(2).Column(3).Element(Block).Text("");
				table.Cell().Row(2).Column(4).Element(Block).Text("");
				table.Cell().Row(2).Column(5).Element(Block).Text("");
				table.Cell().Row(3).Column(1).Element(Block).Text("Êtes souvent fatigué(e) ?");
				table.Cell().Row(3).Column(2).Element(Block).Text("");
				table.Cell().Row(3).Column(3).Element(Block).Text("");
				table.Cell().Row(3).Column(4).Element(Block).Text("");
				table.Cell().Row(3).Column(5).Element(Block).Text("");
				table.Cell().Row(4).Column(1).Element(Block).Text("Avez-vous maigri les 6 derniers mois ?");
				table.Cell().Row(4).Column(2).Element(Block).Text("");
				table.Cell().Row(4).Column(3).Element(Block).Text("");
				table.Cell().Row(4).Column(4).Element(Block).Text("");
				table.Cell().Row(4).Column(5).Element(Block).Text("");
				table.Cell().Row(5).Column(1).Element(Block).Text("Avez-vous des ganglions, des furoncles, des abcès ou des maladies de la peau ?");
				table.Cell().Row(5).Column(2).Element(Block).Text("");
				table.Cell().Row(5).Column(3).Element(Block).Text("");
				table.Cell().Row(5).Column(4).Element(Block).Text("");
				table.Cell().Row(5).Column(5).Element(Block).Text("");
				table.Cell().Row(6).Column(1).Element(Block).Text("Toussez-vous depuis quelques temps en plus de la fièvre");
				table.Cell().Row(6).Column(2).Element(Block).Text("");
				table.Cell().Row(6).Column(3).Element(Block).Text("");
				table.Cell().Row(6).Column(4).Element(Block).Text("");
				table.Cell().Row(6).Column(5).Element(Block).Text("");
				table.Cell().Row(7).Column(1).Element(Block).Text("Avez-vous des plaies dans la bouche ?");
				table.Cell().Row(7).Column(2).Element(Block).Text("");
				table.Cell().Row(7).Column(3).Element(Block).Text("");
				table.Cell().Row(7).Column(4).Element(Block).Text("");
				table.Cell().Row(7).Column(5).Element(Block).Text("");
				table.Cell().Row(8).Column(1).Element(Block).Text("Faites-vous souvent la diarrhée ?");
				table.Cell().Row(8).Column(2).Element(Block).Text("");
				table.Cell().Row(8).Column(3).Element(Block).Text("");
				table.Cell().Row(8).Column(4).Element(Block).Text("");
				table.Cell().Row(8).Column(5).Element(Block).Text("");
				table.Cell().Row(9).Column(1).Element(Block).Text("Êtes vous souvent ballonné(e) ?");
				table.Cell().Row(9).Column(2).Element(Block).Text("");
				table.Cell().Row(9).Column(3).Element(Block).Text("");
				table.Cell().Row(9).Column(4).Element(Block).Text("");
				table.Cell().Row(9).Column(5).Element(Block).Text("");
				table.Cell().Row(10).Column(1).Element(Block).Text("Avez-vous souvent des OEdèmes de Membres Inférieurs (O.M.I) ?");
				table.Cell().Row(10).Column(2).Element(Block).Text("");
				table.Cell().Row(10).Column(3).Element(Block).Text("");
				table.Cell().Row(10).Column(4).Element(Block).Text("");
				table.Cell().Row(10).Column(5).Element(Block).Text("");
				table.Cell().Row(11).Column(1).Element(Block).Text("Êtes vous souvent essoufflé(e) au moindre effort ?");
				table.Cell().Row(11).Column(2).Element(Block).Text("");
				table.Cell().Row(11).Column(3).Element(Block).Text("");
				table.Cell().Row(11).Column(4).Element(Block).Text("");
				table.Cell().Row(11).Column(5).Element(Block).Text("");
				table.Cell().Row(12).Column(1).Element(Block).Text("Avez-vous déjà reçu une perfusion ?");
				table.Cell().Row(12).Column(2).Element(Block).Text("");
				table.Cell().Row(12).Column(3).Element(Block).Text("");
				table.Cell().Row(12).Column(4).Element(Block).Text("");
				table.Cell().Row(12).Column(5).Element(Block).Text("");
				table.Cell().Row(13).Column(1).Element(Block).Text("Avez-vous déjà reçu une transfusion de sang ?");
				table.Cell().Row(13).Column(2).Element(Block).Text("");
				table.Cell().Row(13).Column(3).Element(Block).Text("");
				table.Cell().Row(13).Column(4).Element(Block).Text("");
				table.Cell().Row(13).Column(5).Element(Block).Text("");
				table.Cell().Row(14).Column(1).Element(Block).Text("Avez-vous déjà subi une opération ?");
				table.Cell().Row(14).Column(2).Element(Block).Text("");
				table.Cell().Row(14).Column(3).Element(Block).Text("");
				table.Cell().Row(14).Column(4).Element(Block).Text("");
				table.Cell().Row(14).Column(5).Element(Block).Text("");
				table.Cell().Row(14).Column(1).Element(Block).Text("Avez-vous des informations complémentaires sur votre état de santé susceptibles de renseigner l'Assureur ?");
				table.Cell().Row(14).Column(2).Element(Block).Text("");
				table.Cell().Row(14).Column(3).Element(Block).Text("");
				table.Cell().Row(14).Column(4).Element(Block).Text("");
				table.Cell().Row(14).Column(5).Element(Block).Text("");
			});

			return container;
		}


		public static IContainer ContractTitle(this IContainer container)
		{
			string title = "EXTRAITS DES CONDITIONS GENERALES";
			container.Table(table =>
			{
				table.ColumnsDefinition(column =>
				{
					column.RelativeColumn(2);
					column.RelativeColumn(2);
				});
				table.Cell().ColumnSpan(2).Element(Block1).Text(title).Bold().FontSize(30);
			});

			return container;
		}

		public static IContainer ContractInformation(this IContainer container)
		{
			string title = "EXTRAITS DES CONDITIONS GENERALES";
			container.Table(table =>
			{
				table.ColumnsDefinition(column =>
				{
					column.RelativeColumn(2);
					column.RelativeColumn(2);
				});
				table.Cell().Column(1).Element(Block1).Text(text =>
				{
					text.Line("");
					text.Line("DETERMINATION DES PERSONNES ASSUREES").Bold().Underline();
					text.Line("Est admissible à l'assurance, toute personne physique sollicitant " +
						"un prêt auprès d'une institution financière, âgée d'au moins 18 ans et d'au plus 64 ans");
					text.Line("");
					text.Line("GARANTIES\n").Bold().Underline();
					text.Line("Paiement à l'institution financière de l'encours du prêt (dans la limite" +
						" des montants indiqués au tableau d'amortisssement) de l'adhérent au cas de décès, toute cause ou" +
						" d'invalidité permanente ou totale.\n");
					text.AlignCenter();
					text.Line("EXCLUSIONS\n").Bold().Underline();
					text.Line("BENEFICIAL Togo garantit tous les cas de décès sauf les cas d'exclusions légales et/ou contractuelles\n");
					text.Line("EXCLUSIONS COMMUNES A L'ENSEMBLE DES GARANTIES\n").Bold().Underline();
					text.Line("Ne sont pas couverts, les sinistres consécutifs aux événements ci-après énumérés : \n");
					text.Line("1. Suicide de l'assuré. La garantie en cas de suicide de l'assuré n'est acquise que s'il se produit " +
						"plus de deux ans après l'adhésion de la personne assurée;");
					text.Line("2. Guerre civile et guerre étrangère : Emeutes, mouvements populaires ;");
					text.Line("3. Actes de terrorisme ou de sabotage auxquels l'assuré aurait participé; Participation volontaire de " +
						"l'assuré à un rixe ;");
					text.Line("4. Epidémies et autres catastrophes reconnues comme telles par les autorités publiques ;");
					text.Line("5. Désintégration du noyau atomique, radiation, explosion, dégagement de chaleur, d'origine nucléaire ;\n");

					text.Line("CAPITAL GARANTI\n").Bold().Underline();
					text.Line("Le capital garanti correspond à l'encours du prêt conformément au tableau d'amortissement.\n");
					text.Line("CLAUSE BENEFICIAIRE\n").Bold().Underline();
					text.Line("En cas de décès de l'assuré avant le terme du contrat, le capital garanti est versé à l'institution financière.\n");
					text.Line("L'institution financière est le bénéficiaire désigné et ne peut être remplacé sans son avis écrit.\n");
					text.Line("DUREE DU CONTRAT").Bold().Underline();
					text.Line("Les garanties couvrent toute la durée du remboursement du prêt et sont effectives dès que la demande d'adhésion à" +
						"la convention a été acceptée, et au plus à la date de mise en place du prêt.");
				});

				table.Cell().Column(2).Element(Block1).Text(text =>
				{
					text.AlignCenter();
					text.Line("FIN DU CONTRAT\n").Bold().Underline();
					text.Line("Les garanties cessent pour l'adhérent: ");
					text.Line("1. Au terme initial de la couverture d'assurance ou dès que l'adhérent finit de rembourser le prêt faisant" +
						"l'objet d'assurance");
					text.Line("2. Au décès ou en cas d'invalidité permanente et total de l'adhérent");
					text.Line("3. Au plus tard le 31 Décembre de l'année au cours de laquelle l'adhérent atteint son 65ème anniversaire\n");
					text.Line("PIECES A FOURNIR\n").Bold().Underline();
					text.Line("EN CAS DE DECES TOUTES CAUSES\n").Bold().Underline();
					text.Line("Le bulletin individuel d'adhésion\n");
					text.Line("Un extrait d'acte de naissance ou une pièce d'identité de l'Assuré\n");
					text.Line("L'acte de décès de l'assuré\n");
					text.Line("Certificat médical de genre de mort\n");
					text.Line("Le procès verbal de police ou de gendarmerie si le décès est consécutif à un accident\n");
					text.Line("Le tableau d'amortissement\n");
					text.Line("EN CAS D'INVALIDITE PERMANENTE ET TOTALE\n").Bold().Underline();
					text.Line("1. Un certificat médical fourni par BENEFICIAL Togo dûment renseigné;\n");
					text.Line("2. Le bulletin individuel d'adhésion;\n");
					text.Line("3. L'extrait d'acte de naissance ou une pièce d'identité de l'assuré;\n");
					text.Line("4. Les documents et certificats médicaux attestant de l'invalidité et précisant la cause;\n");
					text.Line("5. Rapport médical complet du médécin traitant;\n");
					text.Line("6. Le procès-verbal de police ou de gendarmerie si l'invalidité est consécutif à un accident;\n");
					text.Line("7. Le tableau d'amortissement;\n");
				});
			});

			return container;
		}



		static IContainer Block(IContainer container)
		{
			return container
				.Border(1)
				.Background(Colors.Blue.Lighten4)
				.ShowOnce()
				.MinWidth(50)
				.MinHeight(50)
				.AlignCenter()
				.AlignMiddle();
		}

		static IContainer Block1(IContainer container)
		{
			return container
				.Background(Colors.Blue.Lighten4)
				.ShowOnce()
				.MinWidth(50)
				.MinHeight(50)
				.AlignCenter()
				.AlignMiddle();
		}
	}
}
